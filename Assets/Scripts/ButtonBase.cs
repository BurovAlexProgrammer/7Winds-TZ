using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ButtonBase : Selectable, IPointerClickHandler, ISubmitHandler
{
    public abstract void OnPointerClick(PointerEventData eventData);

    public abstract void OnSubmit(BaseEventData eventData);
}