using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Tabs
{
    public class ColorTabController : MonoBehaviour, ITabController
    {
        [SerializeField] private List<ColorTabView> _tabs;
        [SerializeField] private PaletteController _palette;
        [SerializeField] private Button _buttonRandomize;
        
        public event Action<int> OnTabSwitched;
        public event Action<Color[]> OnTabColorChanged;
        
        private ColorTabView _selectedTab;
        
        private int SelectedTabIndex => _tabs
            .Select((x, i) => new { tab = x, index = i })
            .Single(x => x.tab == _selectedTab)
            .index;

        private void Awake()
        {
            SelectTab(_tabs[0]);
            
            foreach (var tabView in _tabs)
            {
                tabView.OnClick += SelectTab;
                tabView.OnColorChanged += TabColorChanged;
            }
            
            _palette.OnColorPicked += OnPaletteColorPicked;
            _buttonRandomize.onClick.AddListener(RandomizeColors);
        }

        private void TabColorChanged(Color newColor)
        {
            OnTabColorChanged?.Invoke(_tabs.Select(x => x.Color).ToArray());
        }

        private void OnDestroy()
        {
            foreach (var tabView in _tabs)
            {
                tabView.OnClick -= SelectTab;
            }
            
            _palette.OnColorPicked -= OnPaletteColorPicked;
            _buttonRandomize.onClick.RemoveListener(RandomizeColors);
        }

        public void Init(Color[] colors)
        {
            for (var i = 0; i < _tabs.Count; i++)
            {
                var tabView = _tabs[i];
                tabView.SetColor(colors[i]);
            }
        }

        public Color[] GetColors()
        {
            return _tabs.Select(x => x.Color).ToArray();
        }

        public void SelectTab(ITabView tabView)
        {
            DeselectAllTabs();
            tabView.Select();
            _selectedTab = tabView as ColorTabView;
        }

        public int GetSelectedTabIndex()
        {
            return _tabs.FindIndex(x => x == _selectedTab);
        }
        
        private void DeselectAllTabs()
        {
            foreach (var tabView in _tabs)
            {
                tabView.Deselect();
            }
        }

        private void RandomizeColors()
        {
            for (var i = 0; i < _tabs.Count; i++)
            {
                _tabs[i].SetColor(_palette.GetRandomColor());
            }
        }
        
        private void OnPaletteColorPicked(Color color)
        {
            _selectedTab.SetColor(color);
        }
    }
}