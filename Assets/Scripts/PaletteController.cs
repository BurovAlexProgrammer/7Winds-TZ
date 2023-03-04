using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PaletteController : MonoBehaviour
{
    [SerializeField] private ColorPickButton[] _colorPickButtons;

    public event Action<Color> OnColorPicked;

    private void Awake()
    {
        foreach (var colorPickButton in _colorPickButtons)
        {
            colorPickButton.OnPickColor += ColorPicked;
        }
    }

    private void OnDestroy()
    {
        foreach (var colorPickButton in _colorPickButtons)
        {
            colorPickButton.OnPickColor -= ColorPicked;
        }
    }

    public Color GetRandomColor()
    {
        return _colorPickButtons[Random.Range(0, _colorPickButtons.Length)].GetColor();
    }

    private void ColorPicked(Color color)
    {
        OnColorPicked?.Invoke(color);
    }
}