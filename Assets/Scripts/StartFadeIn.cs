using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartFadeIn : MonoBehaviour
{
    [SerializeField] GameObject _obj;

    Image _image;

    float _delta = 1;
    float _fadeTime = 1;
    float _fadeNum = 8;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _image = GetComponent<Image>();
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        _delta = 1;
        while (true)
        {
            _delta -= Time.deltaTime;
            if (_delta <= 0)
            {
                if (_obj)
                {
                    _obj.GetComponent<StartCount>()?.CoroutineStart();
                }
                gameObject.SetActive(false);
                yield break;
            }
            else
            {
                if (_delta <= _fadeTime)
                {
                    _fadeTime -= 1 / _fadeNum;
                    _image.color = new Color(0, 0, 0, _fadeTime);
                }
                yield return null;
            }
        }
    }
}
