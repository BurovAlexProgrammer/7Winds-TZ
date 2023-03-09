using System;
using System.Collections.Generic;
using System.Linq;
using Tabs;
using UnityEngine;

namespace Uniform
{
    public class MultiLayerViewer : MonoBehaviour
    {
        [SerializeField] private MultiLayerItem[] _viewPrefabs;
        [SerializeField] private ColorTabController _tabController;
        [SerializeField] private PaletteController _palette;

        private LinkedList<MultiLayerItem> _uniformViews;
        private MultiLayerItem _currentView;

        public event Action<Color[]> OnLayerColorChanged;
        public event Action<int, MultiLayerItem> OnModelIndexChanged;

        public MultiLayerItem CurrentView => _currentView;

        private void Awake()
        {
            _palette.OnColorPicked += OnPaletteColorPicked;

            if (_viewPrefabs.Length == 0) throw new Exception("UniformViewer is empty.");

            _uniformViews = new LinkedList<MultiLayerItem>();

            foreach (var uniformPrefab in _viewPrefabs)
            {
                _uniformViews.AddLast(Instantiate(uniformPrefab, transform));
            }
        
            foreach (var uniformView in _uniformViews)
            {
                //uniformView.OnLayerColorChanged += OnUniformLayerColorChanged;
                _tabController.OnTabColorChanged += OnTabColorChanged;
            }
        
            _currentView = _uniformViews.ElementAt(MainContext.Instance.UniformData.ModelIndex);
            OnModelIndexChanged?.Invoke(GetModelIndex(_currentView), _currentView);
            OnLayerColorChanged?.Invoke(_currentView.Colors);
        }

        private void OnTabColorChanged(Color[] colors)
        {
            foreach (var uniformView in _uniformViews)
            {
                for (var i = 0; i < uniformView.Colors.Length; i++)
                {
                    uniformView.SetColor(colors[i], i);
                }
            }
        }

        private void OnUniformLayerColorChanged(Color[] colors)
        {
            OnLayerColorChanged?.Invoke(colors);
        }

        private void OnDestroy()
        {
            _palette.OnColorPicked -= OnPaletteColorPicked;
        
            foreach (var uniformView in _uniformViews)
            {
                uniformView.OnLayerColorChanged -= OnUniformLayerColorChanged;
            }
        }

        private void OnPaletteColorPicked(Color newColor)
        {
            Debug.Log("Picked");
            var tabIndex = _tabController.GetSelectedTabIndex();
            CurrentView.SetColor(newColor, tabIndex);
        }

        private void Start()
        {
            foreach (var uniformView in _uniformViews)
            {
                uniformView.gameObject.SetActive(false);
            }

            CurrentView.gameObject.SetActive(true);
        }

        public void ShowNext()
        {
            var nextItem = _uniformViews.Find(CurrentView).Next ?? _uniformViews.First;
            var nextView = nextItem.Value;
            SwitchView(nextView);
            OnModelIndexChanged?.Invoke(GetModelIndex(nextView), nextView);
        }

        public void ShowPrev()
        {
            var prevItem = _uniformViews.Find(CurrentView).Previous ?? _uniformViews.Last;
            var prevView = prevItem.Value;
            SwitchView(prevView);
            OnModelIndexChanged?.Invoke(GetModelIndex(prevView), prevView);
        }

        private void SwitchView(MultiLayerItem newView)
        {
            CurrentView.gameObject.SetActive(false);
            _currentView = newView;
            SetCurrentColorsToUniform(_currentView);
            CurrentView.gameObject.SetActive(true);
        }

        private int GetModelIndex(MultiLayerItem uniformView)
        {
            return _uniformViews
                .Select((x, i) => new { uniform = x, index = i })
                .First(x => x.uniform == uniformView)
                .index;
        }

        private void SetCurrentColorsToUniform(MultiLayerItem uniformView)
        {
            var colors = _tabController.GetColors();
        
            for (var i = 0; i < _currentView.Colors.Length; i++)
            {
                _currentView.SetColor(colors[i], i);
            }
        }
    }
}