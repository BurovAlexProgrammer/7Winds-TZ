using UnityEngine;
using UnityEngine.UI;

namespace PageViews
{
    public class PageView1 : PageView
    {
        [SerializeField] private UniformViewer _uniformViewer;
        [SerializeField] private Button _buttonNextUniform;
        [SerializeField] private Button _buttonPrevUniform;
        [SerializeField] private Button _buttonNextPage;

        private void Awake()
        {
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