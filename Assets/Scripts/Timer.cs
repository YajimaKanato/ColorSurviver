using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour, IPause, IGameControl
{
    [SerializeField] float _timer = 60;
    [SerializeField] Text _text;

    float _delta = 0;
    bool _isPause = false;
    bool _isGameClear = false;
    bool _isGameOver = false;
    bool _isGameStart = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _delta = _timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameStart && !_isPause && !_isGameClear && !_isGameOver)
        {
            if (_delta <= 0)
            {
                _delta = 0;
                _text.text = _delta.ToString("00.00");
                GameClear();
                gameObject.SetActive(false);
            }
            else
            {
                _delta -= Time.deltaTime;
                _text.text = _delta.ToString("00.00");
            }
        }
    }

    void GameClear()
    {
        //Object���p������GameObject���w�肷�邱�Ƃł��ׂẴI�u�W�F�N�g���擾���邱�Ƃ��ł���
        var pause = FindObjectsByType(typeof(GameObject), FindObjectsSortMode.None);
        foreach (var obj in pause)
        {
            obj.GetComponent<IGameControl>()?.GameClear();
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

    void IGameControl.GameClear()
    {

    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void GameStart()
    {
        _isGameStart = true;
    }
}
