using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorPickButton : ButtonBase
{
    [SerializeField] private Image _imageColorRef;
        
    public event Action<Color> OnPickColor;

    public Color GetColor() => _imageColorRef.color;

    public override void OnPointerClick(PointerEventData eventData)
    {
        OnPickColor?.Invoke(_imageColorRef.color);
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        OnPickColor?.Invoke(_imageColorRef.color);
    }
}