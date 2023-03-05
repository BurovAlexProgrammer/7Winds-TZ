using System;
using System.ComponentModel;
using UnityEngine;

[Serializable]
public class UniformData
{
    public int ModelIndex = -1;
    public Color[] ModelColors;
    public int LogoIndex = -1;
    public Color[] LogoColors;
    public string TeamName;
}