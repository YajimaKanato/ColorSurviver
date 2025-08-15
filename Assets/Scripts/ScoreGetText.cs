using UnityEngine;
using UnityEngine.UI;

public class ScoreGetText : MonoBehaviour, IPause
{
    Text _text;

    int _score;
    public int Score { get { return _score; } set { _score = value; } }
    float _delta;
    bool _isPause = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = _score.ToString();
        if (!_isPause)
        {
            _delta += Time.deltaTime;
            _text.color = new Color(1, 1, 1, 1 - _delta);
            gameObject.transform.position += Vector3.up;
            if (_delta >= 1)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Pause()
    {
        _isPause = true;
    }

    public void Resume()
    {
        _isPause = false;
    }
}
