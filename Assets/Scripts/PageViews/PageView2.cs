using UnityEngine;
using UnityEngine.UI;

namespace PageViews
{
    public class PageView2 : PageViewBase
    {
        [SerializeField] private UniformViewer _uniformViewer;
        [SerializeField] private Button _buttonNextUniform;
        [SerializeField] private Button _buttonPrevUniform;
        [SerializeField] private Button _buttonNextPage;

        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

            public override RectTransform RectTransform => _rectTransform;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
            _buttonNextUniform.onClick.AddListener(OnClickButtonNextUniform);
            _buttonPrevUniform.onClick.AddListener(OnClickButtonPrevUniform);
        }

        private void OnDestroy()
        {
            _buttonNextUniform.onClick.RemoveListener(OnClickButtonNextUniform);
            _buttonPrevUniform.onClick.RemoveListener(OnClickButtonPrevUniform);
        }
        
        public override void SetInteractable(bool state)
        {
            _canvasGroup.interactable = state;
        }

        private void OnClickButtonNextUniform()
        {
            _uniformViewer.ShowNext();
        }

        private void OnClickButtonPrevUniform()
        {
            _uniformViewer.ShowPrev();
        }

    }
}