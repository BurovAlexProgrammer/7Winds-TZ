using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PageViews
{
    public class PageViewer : MonoBehaviour
    {
        [SerializeField] private PageViewBase[] _pageViews;
        [SerializeField] private Button[] _nextPageButtons;

        private PageViewBase _currentPage;
        private CancellationToken _cancellationToken;
        private LinkedList<PageViewBase> _pageViewList;

        private const float ChangePageDuration = 0.5f;

        private void Awake()
        {
            _cancellationToken = gameObject.GetCancellationTokenOnDestroy();
            _pageViewList = new LinkedList<PageViewBase>(_pageViews);
            _currentPage = _pageViews[0];
            
            for (var i = 1; i < _pageViews.Length; i++)
            {
                _pageViews[i].gameObject.SetActive(false);
            }

            foreach (var nextPageButton in _nextPageButtons)
            {
                nextPageButton.onClick.AddListener(ShowNextPage);
            }
        }

        private void OnDestroy()
        {
            foreach (var nextPageButton in _nextPageButtons)
            {
                nextPageButton.onClick.RemoveListener(ShowNextPage);
            }
        }

        private void ShowNextPage()
        {
            ExecuteShowNextPage().Forget();
            
        }

        private async UniTaskVoid ExecuteShowNextPage()
        {
            var nextPage = GetNextPage();
            _currentPage.SetInteractable(false);
            nextPage.SetInteractable(false);
            nextPage.gameObject.SetActive(true);
            
            var sequence = DOTween.Sequence()
                .Append(_currentPage.RectTransform
                    .DOAnchorPosX(-Screen.width, ChangePageDuration)
                    .SetEase(Ease.InOutQuart))
                .Join(nextPage.RectTransform
                    .DOAnchorPosX(0f, ChangePageDuration)
                    .From(Vector2.right * Screen.width)
                    .SetEase(Ease.InOutQuart))
                .OnComplete(CompleteSequence);
            
            await sequence.ToUniTask(TweenCancelBehaviour.Complete, _cancellationToken);

            void CompleteSequence()
            {
                _currentPage.gameObject.SetActive(false);
                _currentPage = nextPage;
                _currentPage.SetInteractable(true);
            }

            PageViewBase GetNextPage()
            {
                var nextPageNode = _pageViewList.Find(_currentPage).Next ?? _pageViewList.First;
                return nextPageNode.Value;
            }
        }
        
        
    }
}