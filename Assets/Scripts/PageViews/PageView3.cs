using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PageViews
{
    public class PageView3 : PageViewBase
    {
        [SerializeField] private RectTransform _uniformViewContainer;
        [SerializeField] private RectTransform _logoViewContainer;
        [SerializeField] private TMP_InputField _teamNameField;
        [SerializeField] private TMP_Text _enterTeamNameLabel;
        [SerializeField] private Button _buttonNext;
        
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private Color _originEnterTeamNameLabelColor;

        public override RectTransform RectTransform => _rectTransform;

        private void Awake()
        {
            _teamNameField.onValueChanged.AddListener(ValidateTeamNameInput);
            _originEnterTeamNameLabelColor = _enterTeamNameLabel.color;
        }

        private void ValidateTeamNameInput(string text)
        {
            _buttonNext.interactable = text.Length <= 10;
            _enterTeamNameLabel.color =
                text.Length <= 10 ? _originEnterTeamNameLabelColor : Common.Colors.ErrorTextColor;
        }

        private void Start()
        {
            SetPreview();
        }
        
        private void OnDestroy()
        {
        }

        public override void SetInteractable(bool state)
        {
            _canvasGroup.interactable = state;
        }

        public override void Init()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetPreview()
        {
            RemoveChildren(_logoViewContainer);
            RemoveChildren(_uniformViewContainer);

            Instantiate(MainContext.Instance.CurrentUniformView, _uniformViewContainer);
            Instantiate(MainContext.Instance.CurrentLogoView, _logoViewContainer);
        }

        private void RemoveChildren(Transform transformContainer)
        {
            foreach (Transform child in transformContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}