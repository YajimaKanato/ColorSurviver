using UnityEngine;

public class CreditText : MonoBehaviour
{
    [SerializeField] GameObject _text;

    Vector3 _textPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _textPos = gameObject.transform.position;
        _textPos.z = -10;
        Debug.Log(Camera.main.WorldToScreenPoint(_textPos));
    }

    // Update is called once per frame
    void Update()
    {
        _textPos = gameObject.transform.position;
        _textPos.z = -10;
        _text.transform.position = Camera.main.WorldToScreenPoint(_textPos);
        _text.transform.rotation = gameObject.transform.rotation;
    }
}
