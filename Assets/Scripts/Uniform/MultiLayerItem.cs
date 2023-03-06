using System;
using UnityEngine;
using UnityEngine.UI;

namespace Uniform
{
    public abstract class MultiLayerItem : MonoBehaviour
    {
        [SerializeField] private Image[] _imageLayers;
        [SerializeField] private Color[] _colorLayers;

        public event Action<Color[]> OnLayerColorChanged;

        public Color[] Colors => _colorLayers;
    
        private void Start()
        {
            SetInitialColors();
        }

        public void SetColor(Color color, int layerIndex)
        {
            _colorLayers[layerIndex] = color;
            _imageLayers[layerIndex].color = color;
            OnLayerColorChanged?.Invoke(_colorLayers);
        }

        private void SetInitialColors()
        {
            if (_imageLayers.Length != _colorLayers.Length)
            {
                Debug.LogError("Target GameObject with exception. (Click to select)");
                throw new Exception($"UniformView error: imageLayers count not the same as colorLayers.");
            }
        
            for (var i = 0; i < _imageLayers.Length; i++)
            {
                _imageLayers[i].color = _colorLayers[i];
            }
        }
    }
}