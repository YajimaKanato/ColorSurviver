using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameClearOrOverS : MonoBehaviour, IGameControl
{
    Image _image;

    float _delta = 0;
    float _fadeTime = 0;
    float _fadeNum = 8;

    public void GameClear()
    {
        StartCoroutine(FadeS("GameClearS"));
    }

    public void GameOver()
    {
        StartCoroutine(FadeS("GameOverS"));
    }

    public void GameStart()
    {

    }

    IEnumerator FadeS(string name)
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _image = GetComponent<Image>();
    }
}
