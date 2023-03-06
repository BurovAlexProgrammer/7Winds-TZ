using System;
using Tabs;
using Uniform;
using UnityEngine;

public class MainContext : MonoBehaviour
{
    [SerializeField] private UniformData _uniformData;
    [SerializeField] private ColorTabController _tabController;
    [SerializeField] private MultiLayerViewer _uniformViewer;

    private static MainContext _instance;
    public static MainContext Instance => _instance ? _instance : throw new NullReferenceException();
    public UniformData UniformData => _uniformData;
    public MultiLayerItem CurrentUniformView => _uniformViewer.CurrentView;

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
            ModelColors = new []
            {
                ColorUtility.TryParseHtmlString("#40A5DA", out var color1) ? color1 : Color.white, 
                ColorUtility.TryParseHtmlString("#050076", out var color2) ? color2 : Color.gray, 
                ColorUtility.TryParseHtmlString("#CCCDCC", out var color3) ? color3 : Color.black, 
            }
        }; //TODO Load from file
        
        _tabController.OnTabColorChanged += OnUniformTabColorChanged;
        _uniformViewer.OnModelIndexChanged += OnModelIndexChanged;
    }

    private void Start()
    {
        _tabController.Init(_uniformData.ModelColors);
    }

    private void OnUniformTabColorChanged(Color[] colors)
    {
        _uniformData.ModelColors = colors;
    }

    private void OnDestroy()
    {
        _tabController.OnTabColorChanged -= OnUniformTabColorChanged;
        _uniformViewer.OnModelIndexChanged -= OnModelIndexChanged;
    }

    private void OnModelIndexChanged(int index, MultiLayerItem view)
    {
        Debug.Log("Index changed");
        _uniformData.ModelIndex = index;
        _uniformData.ModelColors = view.Colors;
    }
}