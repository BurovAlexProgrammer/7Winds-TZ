using System;
using System.Collections.Generic;

namespace Tabs
{
    public interface ITabController
    {
        public event Action<int> OnTabSwitched;

        public void SelectTab(ITabView tabView);
        public int GetSelectedTabIndex();
    }
}