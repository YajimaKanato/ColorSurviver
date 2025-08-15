using UnityEngine;

public class SlimeorBat : MonoBehaviour
{
    static SlimeorBat _instance;

    static bool _slime = false;
    public static bool Slime { get { return _slime; } set { _slime = value; } }
    static bool _bat = false;
    public static bool Bat { get { return _bat; } set { _bat = value; } }

    private void Start()
    {
        _slime = false;
        _bat = false;
    }

    public void GoSlime()
    {
        _slime = true;
        Debug.Log(_slime);
        Debug.Log(_bat);
    }

    public void GoBat()
    {
        _bat = true;
        Debug.Log(_slime);
        Debug.Log(_bat);
    }
}
