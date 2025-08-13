using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartCount : MonoBehaviour, IPause
{
    Text _text;

    Color _color;
    Color _defColor;

    IEnumerator _coroutine;

    int _count = 4;
    float _delta = 0;
    int _size;
    int _defSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = "";
        _size = _text.fontSize;
        _defSize = _text.fontSize;
        _color = _text.color;
        _defColor = _text.color;
    }

    public void CoroutineStart()
    {
        _coroutine = CountDownCoroutine();
        StartCoroutine(_coroutine);
    }

    IEnumerator CountDownCoroutine()
    {
        _text.text = "" + (_count - 1);
        while (true)
        {
            if (_count <= 0)
            {
                GameStart();
                yield break;
            }
            else
            {
                _text.color = _color;
                _text.fontSize = _size;
                _delta += Time.deltaTime;
                _color.a -= Time.deltaTime;
                _size -= 1;
                if (_count <= 1)
                {
                    _text.text = "Let's Clean!";
                }
                else
                {
                    _text.text = "" + (_count - 1);
                }

                if (_delta >= 1)
                {
                    SEManager.SEPlay("CountDown");
                    _delta = 0;
                    _count--;
                    _color.a = _defColor.a;
                    _size = _defSize;
                }

                yield return null;
            }
        }
    }

    void GameStart()
    {
        var start = FindObjectsByType(typeof(GameObject), FindObjectsSortMode.None);
        foreach (var obj in start)
        {
            var script = obj.GetComponents<IGameControl>();
            if (script != null)
            {
                foreach (var c in script)
                {
                    c.GameStart();
                }
            }
        }
        gameObject.SetActive(false);
    }

    public void Pause()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    public void Resume()
    {
        if (_coroutine != null)
        {
            StartCoroutine(_coroutine);
        }
    }
}
