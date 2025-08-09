using UnityEngine;
using ColorAttributes;

/// <summary>
/// F‚É‰‚¶‚½‰Šúİ’è
/// </summary>
[CreateAssetMenu(fileName = "ColorStatus", menuName = "Scriptable Objects/ColorStatus")]
public class ColorStatus : ScriptableObject
{
    [SerializeField] ColorAttribute _colorAttribute;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _minSpeed;

    public ColorAttribute ColorAttribute { get { return _colorAttribute; } }
    public float MaxSpeed { get { return _maxSpeed; } }
    public float MinSpeed { get { return _minSpeed; } }
}
