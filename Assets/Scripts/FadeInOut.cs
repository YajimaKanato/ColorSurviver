using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] bool _fadeIn;
    [SerializeField] bool _fadeOut;

    Image _image;

    float _delta = 1;
    float _fadeTime = 1;
    float _fadeNum = 8;

    void Start()
    {
        if (_fadeIn)
        {
            StartCoroutine(FadeIn());
        }
    }

    public void StartFadeOut(string name)
    {
        if(_fadeOut)
        {
            StartCoroutine(FadeOut(name));
        }
    }

    IEnumerator FadeIn()
    {
        _delta = 1;
        while (true)
        {
            _delta -= Time.deltaTime;
            if (_delta <= 0)
            {
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

    IEnumerator FadeOut(string name)
    {
        _delta = 0;
        while (true)
        {
            _delta += Time.deltaTime;
            if (_delta >= 1.0f)
            {
                SceneChange.ChangeScene(name);
                yield break;
            }
            else
            {
                if (_delta >= _fadeTime)
                {
                    _fadeTime += 1 / _fadeNum;
                    _image.color = new Color(0, 0, 0, _fadeTime);
                }
                yield return null;
            }
        }
    }
}
