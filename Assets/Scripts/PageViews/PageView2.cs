using Uniform;
using UnityEngine;
using UnityEngine.UI;

namespace PageViews
{
    public class PageView2 : PageViewBase
    {
        [SerializeField] private RectTransform _uniformViewContainer;
        [SerializeField] private MultiLayerViewer _logoViewer;
        [SerializeField] private Button _buttonNextView;
        [SerializeField] private Button _buttonPrevView;

        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        public override RectTransform RectTransform => _rectTransform;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
            _buttonNextView.onClick.AddListener(OnClickButtonNextUniform);
            _buttonPrevView.onClick.AddListener(OnClickButtonPrevUniform);
        }

        private void OnDestroy()
        {
            _buttonNextView.onClick.RemoveListener(OnClickButtonNextUniform);
            _buttonPrevView.onClick.RemoveListener(OnClickButtonPrevUniform);
        }

        public override void SetInteractable(bool state)
        {
            _canvasGroup.interactable = state;
        }

        public override void Init()
        {
            SetUniformView(MainContext.Instance.CurrentUniformView);
        }

        public void SetUniformView(MultiLayerItem item)
        {
            RemoveChildren();

            var uniformViewCopy = Instantiate(item, transform);
        }

        private void RemoveChildren()
        {
            foreach (Transform child in transform)
            {
                Destroy(child);
            }
        }

        private void OnClickButtonNextUniform()
        {
            _logoViewer.ShowNext();
        }

        private void OnClickButtonPrevUniform()
        {
            _logoViewer.ShowPrev();
        }
    }
}