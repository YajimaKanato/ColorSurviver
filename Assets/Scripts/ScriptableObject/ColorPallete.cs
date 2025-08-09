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
        [InspectorName("��")] Red,
        [InspectorName("�I�����W")] Orange,
        [InspectorName("���F")] Yellow,
        [InspectorName("��")] Green,
        [InspectorName("���F")] RightBlue,
        [InspectorName("��")] Blue,
        [InspectorName("��")] Purple,
        [InspectorName("��")] White,
        [InspectorName("��")] Black,
        [InspectorName("���F")] Colorless
    }
}
