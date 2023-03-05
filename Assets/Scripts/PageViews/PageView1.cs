using UnityEngine;
using UnityEngine.UI;

namespace PageViews
{
    public class PageView1 : PageViewBase
    {
        [SerializeField] private UniformViewer _uniformViewer;
        [SerializeField] private Button _buttonNextUniform;
        [SerializeField] private Button _buttonPrevUniform;

        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        public override RectTransform RectTransform => _rectTransform;

        public override void SetInteractable(bool state)
        {
            _canvasGroup.interactable = state;
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _buttonNextUniform.onClick.AddListener(OnClickButtonNextUniform);
            _buttonPrevUniform.onClick.AddListener(OnClickButtonPrevUniform);
        }

        private void OnDestroy()
        {
            _buttonNextUniform.onClick.RemoveListener(OnClickButtonNextUniform);
            _buttonPrevUniform.onClick.RemoveListener(OnClickButtonPrevUniform);
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