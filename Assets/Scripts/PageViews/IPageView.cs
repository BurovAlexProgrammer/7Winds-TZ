using UnityEngine;

namespace PageViews
{
    public interface IPageView
    {
        RectTransform RectTransform { get; }

        void SetInteractable(bool state);
    }
}