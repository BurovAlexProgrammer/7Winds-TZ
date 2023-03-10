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

        private const int TeamNameMaxLength = 10;
        
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private Color _originEnterTeamNameLabelColor;

        public override RectTransform RectTransform => _rectTransform;

        private void Awake()
        {
            _teamNameField.SetTextWithoutNotify(MainContext.Instance.UniformData.TeamName);
            _teamNameField.onValueChanged.AddListener(ValidateSaveTeamName);
            _originEnterTeamNameLabelColor = _enterTeamNameLabel.color;
        }

        private void OnEnable()
        {
            SetPreview();
        }

        private void OnDestroy()
        {
            _teamNameField.onValueChanged.RemoveListener(ValidateSaveTeamName);
        }

        private void ValidateSaveTeamName(string text)
        {
            var isValid = text.Length <= TeamNameMaxLength;
            _buttonNext.interactable = isValid;
            _enterTeamNameLabel.color = isValid ? _originEnterTeamNameLabelColor : Common.Colors.ErrorTextColor;

            if (isValid)
            {
                MainContext.Instance.UniformData.TeamName = _teamNameField.text;
            }
        }
        
        public override void Init()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
        }
        
        public override void SetInteractable(bool state)
        {
            _canvasGroup.interactable = state;
        }
        
        public void SetPreview()
        {
            RemoveChildren(_logoViewContainer);
            RemoveChildren(_uniformViewContainer);

            if (MainContext.Instance.CurrentUniformView != null)
            {
                Instantiate(MainContext.Instance.CurrentUniformView, _uniformViewContainer);
            }

            if (MainContext.Instance.CurrentLogoView != null)
            {
                Instantiate(MainContext.Instance.CurrentLogoView, _logoViewContainer);
            }
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