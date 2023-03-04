using System;

namespace Tabs
{
    public interface ITabView
    {
        public event Action<ITabView> OnClick;

        public void Select();
        public void Deselect();
    }
}