using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tabs
{
    [RequireComponent(typeof(Button))]
    public abstract class TabView : MonoBehaviour
    {
        [SerializeField] private GameObject _selectFrame;

        
        public event Action<TabView> OnClick;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClickButton);
            Deselect();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClickButton);
        }

        public void Select()
        {
            _selectFrame.SetActive(true);
        }

        public void Deselect()
        {
            _selectFrame.SetActive(false);
        }

        private void OnClickButton()
        {
            OnClick?.Invoke(this);
        }
    }
}
