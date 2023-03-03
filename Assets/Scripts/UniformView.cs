using UnityEngine;
using UnityEngine.UI;

public class UniformView : MonoBehaviour
{
    [SerializeField] private Image _imageLayer1;
    [SerializeField] private Image _imageLayer2;
    [SerializeField] private Image _imageLayer3;
    [SerializeField] private Color _colorLayer1;
    [SerializeField] private Color _colorLayer2;
    [SerializeField] private Color _colorLayer3;
    
    void Start()
    {
        SetColors();
    }

    void Update()
    {
        
    }

    private void SetColors()
    {
        _imageLayer1.color = _colorLayer1;
        _imageLayer2.color = _colorLayer2;
        _imageLayer3.color = _colorLayer3;
    }
}
