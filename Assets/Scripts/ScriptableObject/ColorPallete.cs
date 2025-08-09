using UnityEngine;
using ColorAttributes;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ColorPallete", menuName = "Scriptable Objects/ColorPallete")]
public class ColorPallete : ScriptableObject
{
    [SerializeField] List<ColorData> _colorDataList;
    public List<ColorData> ColorDataList { get { return _colorDataList; } }
}

[System.Serializable]
public class ColorData
{
    [SerializeField] ColorAttribute _colorAttribute;
    [SerializeField] Color _color;

    public ColorAttribute ColorAttribute { get { return _colorAttribute; } }
    public Color Color { get { return _color; } }
}

namespace ColorAttributes
{
    public enum ColorAttribute
    {
        [InspectorName("赤")] Red,
        [InspectorName("オレンジ")] Orange,
        [InspectorName("黄色")] Yellow,
        [InspectorName("緑")] Green,
        [InspectorName("水色")] RightBlue,
        [InspectorName("青")] Blue,
        [InspectorName("紫")] Purple,
        [InspectorName("白")] White,
        [InspectorName("黒")] Black,
        [InspectorName("無色")] Colorless
    }
}
