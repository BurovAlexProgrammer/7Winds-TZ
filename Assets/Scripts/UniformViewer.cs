using System;
using System.Collections.Generic;
using UnityEngine;

public class UniformViewer : MonoBehaviour
{
    [SerializeField] private UniformView[] _uniformViewPrefabs;

    private LinkedList<UniformView> _uniformViews;
    private UniformView _currentView;

    private void Awake()
    {
        if (_uniformViewPrefabs.Length == 0) throw new Exception("UniformViewer is empty.");

        _uniformViews = new LinkedList<UniformView>();

        foreach (var uniformPrefab in _uniformViewPrefabs)
        {
            _uniformViews.AddLast(Instantiate(uniformPrefab, transform));
        }

        _currentView = _uniformViews.First.Value;
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
        SwitchView(_uniformViews.Find(_currentView).Next.Value);
    }

    public void ShowPrev()
    {
        SwitchView(_uniformViews.Find(_currentView).Previous.Value);
    }

    private void SwitchView(UniformView newView)
    {
        _currentView.gameObject.SetActive(false);
        _currentView = newView;
        _currentView.gameObject.SetActive(true);
    }
}