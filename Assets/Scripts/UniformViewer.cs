using System;
using System.Collections.Generic;
using Tabs;
using UnityEngine;

public class UniformViewer : MonoBehaviour
{
    [SerializeField] private UniformView[] _uniformViewPrefabs;
    [SerializeField] private ColorTabController _tabController;
    [SerializeField] private PaletteController _palette;
    
    private LinkedList<UniformView> _uniformViews;
    private UniformView _currentView;

    private void Awake()
    {
        _palette.OnColorPicked += OnPaletteColorPicked;
        
        if (_uniformViewPrefabs.Length == 0) throw new Exception("UniformViewer is empty.");

        _uniformViews = new LinkedList<UniformView>();

        foreach (var uniformPrefab in _uniformViewPrefabs)
        {
            _uniformViews.AddLast(Instantiate(uniformPrefab, transform));
        }

        _currentView = _uniformViews.First.Value;
    }

    private void OnDestroy()
    {
        _palette.OnColorPicked -= OnPaletteColorPicked;
    }

    private void OnPaletteColorPicked(Color newColor)
    {
        Debug.Log("Picked");
        var tabIndex = _tabController.GetSelectedTabIndex();
        _currentView.SetColor(newColor, tabIndex);
        
    }

    private void Start()
    {
        foreach (var uniformView in _uniformViews)
        {
            uniformView.gameObject.SetActive(false);
        }
        
        _currentView.gameObject.SetActive(true);
    }

    public void ShowNext()
    {
        var nextItem = _uniformViews.Find(_currentView).Next ?? _uniformViews.First;
        SwitchView(nextItem.Value);
    }

    public void ShowPrev()
    {
        var prevItem = _uniformViews.Find(_currentView).Previous ?? _uniformViews.Last;
        SwitchView(prevItem.Value);
    }

    private void SwitchView(UniformView newView)
    {
        _currentView.gameObject.SetActive(false);
        _currentView = newView;
        _currentView.gameObject.SetActive(true);
    }
}