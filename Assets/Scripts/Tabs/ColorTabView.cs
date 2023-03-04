using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tabs
{
    public class ColorTabView : TabView
    {
        [SerializeField] private Image _tabColorImage;

        public event Action<Color> OnColorChanged; 
        
        public void SetColor(Color color)
        {
            _tabColorImage.color = color;
            OnColorChanged?.Invoke(color);
        }
    }
}