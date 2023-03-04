using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tabs
{
    public class ColorTabView : MonoBehaviour, ITabView
    {
        [SerializeField] private Image _tabColorImage;

        [SerializeField] private GameObject _selectFrame;
        
        public event Action<ITabView> OnClick;

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
            OnClick?.Invoke(this as ITabView);
        }
        public event Action<Color> OnColorChanged; 
        
        public void SetColor(Color color)
        {
            _tabColorImage.color = color;
            OnColorChanged?.Invoke(color);
        }
    }
}