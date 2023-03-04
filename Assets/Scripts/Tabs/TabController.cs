using UnityEngine;

namespace Tabs
{
    public class TabController : MonoBehaviour
    {
        [SerializeField] private TabView[] _tabs;

        private TabView _selectedTab;

        private void Awake()
        {
            foreach (var tabView in _tabs)
            {
                tabView.OnClick += SelectTabView;
            }
        }

        private void OnDestroy()
        {
            foreach (var tabView in _tabs)
            {
                tabView.OnClick -= SelectTabView;
            }
        }
        

        private void SelectTabView(TabView tabView)
        {
            DeselectAllTabs();
            tabView.Select();
            _selectedTab = tabView;
        }

        private void DeselectAllTabs()
        {
            foreach (var tabView in _tabs)
            {
                tabView.Deselect();
            }
        }
    }
}