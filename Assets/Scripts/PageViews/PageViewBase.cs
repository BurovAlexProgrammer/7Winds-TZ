using UnityEngine;

namespace PageViews
{
    [RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
    public abstract class PageViewBase : MonoBehaviour, IPageView
    {
        public abstract RectTransform RectTransform { get; }
        public abstract void SetInteractable(bool state);
        public abstract void Init();
    }
}
