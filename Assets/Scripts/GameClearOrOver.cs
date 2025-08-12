using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameClearOrOver : MonoBehaviour, IGameControl
{
    Image _image;

    float _delta = 0;
    float _fadeTime = 0;
    float _fadeNum = 8;

    public void GameClear()
    {
        StartCoroutine(Fade("GameClear"));
    }

    public void GameOver()
    {
        StartCoroutine(Fade("GameOver"));
    }

    public void GameStart()
    {

    }

    IEnumerator Fade(string name)
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
