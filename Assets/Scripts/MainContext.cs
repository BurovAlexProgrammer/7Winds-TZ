using System;
using Tabs;
using Uniform;
using UnityEngine;
using UnityEngine.Serialization;

public class MainContext : MonoBehaviour
{
    [SerializeField] private UniformData _uniformData;
    [SerializeField] private ColorTabController _uniformTabController;
    [SerializeField] private MultiLayerViewer _uniformViewer;
    [SerializeField] private ColorTabController _logoTabController;
    [SerializeField] private MultiLayerViewer _logoViewer;

    private static MainContext _instance;
    public static MainContext Instance => _instance ? _instance : throw new NullReferenceException();
    public UniformData UniformData => _uniformData;
    public MultiLayerItem CurrentUniformView => _uniformViewer.CurrentView;
    public MultiLayerItem CurrentLogoView => _logoViewer.CurrentView;

    public MainContext()
    {
        if (_instance != null) throw new Exception("MainContext singleton duplicate.");

        _instance = this;
    }

    private void Awake()
    {
        _uniformData = new UniformData()
        {
            ModelIndex = 1,
            ModelColors = new[]
            {
                ColorUtility.TryParseHtmlString("#40A5DA", out var color1) ? color1 : Color.white,
                ColorUtility.TryParseHtmlString("#050076", out var color2) ? color2 : Color.gray,
                ColorUtility.TryParseHtmlString("#CCCDCC", out var color3) ? color3 : Color.black,
            },
            LogoIndex = 0,
            LogoColors = new[]
            {
                ColorUtility.TryParseHtmlString("#40A5DA", out var color4) ? color1 : Color.white,
                ColorUtility.TryParseHtmlString("#050076", out var color5) ? color2 : Color.gray,
                ColorUtility.TryParseHtmlString("#CCCDCC", out var color6) ? color3 : Color.black,
            }
        }; //TODO Load from file

        _uniformTabController.OnTabColorChanged += OnUniformTabColorChanged;
        _uniformViewer.OnModelIndexChanged += OnModelIndexChanged;
        _logoTabController.OnTabColorChanged += OnLogoTabColorChanged;
        _logoViewer.OnModelIndexChanged += OnLogoIndexChanged;
    }

    private void Start()
    {
        _uniformTabController.Init(_uniformData.ModelColors);
        _logoTabController.Init(_uniformData.LogoColors);
    }

    private void OnUniformTabColorChanged(Color[] colors)
    {
        _uniformData.ModelColors = colors;
    }
    
    private void OnLogoTabColorChanged(Color[] colors)
    {
        _uniformData.LogoColors = colors;
    }
    
    private void OnDestroy()
    {
        _uniformTabController.OnTabColorChanged -= OnUniformTabColorChanged;
        _uniformViewer.OnModelIndexChanged -= OnModelIndexChanged;
        _logoTabController.OnTabColorChanged -= OnLogoTabColorChanged;
        _logoViewer.OnModelIndexChanged -= OnLogoIndexChanged;
    }

    private void OnModelIndexChanged(int index, MultiLayerItem view)
    {
        Debug.Log("Index changed");
        _uniformData.ModelIndex = index;
        _uniformData.ModelColors = view.Colors;
    }

    private void OnLogoIndexChanged(int index, MultiLayerItem view)
    {
        Debug.Log("Logo Index changed");
        _uniformData.LogoIndex = index;
        _uniformData.LogoColors = view.Colors;
    }
}