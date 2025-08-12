using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour, IPause, IGameControl
{
    [SerializeField] int _maxTargetCount = 20;
    [SerializeField] float _countDown = 10;
    [SerializeField] Text _countDownText;

    ObjectPoolAndSpawn[] _objectPool;
    IEnumerator _coroutine;
    int _currentTargetCount = 0;
    int _count;

    bool _isGameClear = false;
    bool _isGameStart = false;

    private void Start()
    {
        _objectPool = FindObjectsByType<ObjectPoolAndSpawn>(FindObjectsSortMode.None);
        _countDownText.text = "";
    }

    private void Update()
    {
        if (_isGameStart && !_isGameClear)
        {
            GetTargetCount();

            if (_currentTargetCount >= _maxTargetCount)
            {
                if (_coroutine == null)
                {
                    _coroutine = CountDownCoroutine();
                    StartCoroutine(_coroutine);
                }
            }
            else
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                    _coroutine = null;
                    _countDownText.text = "";
                }
            }
        }
    }

    void GetTargetCount()
    {
        _currentTargetCount = 0;
        foreach (var obj in _objectPool)
        {
            _currentTargetCount += obj.SpawnCount;
        }
    }

    IEnumerator CountDownCoroutine()
    {
        _count = (int)_countDown;
        var wait = new WaitForSeconds(1);
        while (true)
        {
            if (_count <= 0)
            {
                _countDownText.text = "Game Over";
                GameOver();
                yield break;
            }
            else
            {
                _countDownText.text = "" + _count;
                yield return wait;
                _count--;
            }
        }
    }

    void GameOver()
    {
        //Objectを継承したGameObjectを指定することですべてのオブジェクトを取得することができる
        var pause = FindObjectsByType(typeof(GameObject), FindObjectsSortMode.None);
        foreach (var obj in pause)
        {
            var script = obj.GetComponents<IGameControl>();
            foreach (var g in script)
            {
                g.GameOver();
            }
        }
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

    public void GameClear()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    void IGameControl.GameOver()
    {

    }

    public void GameStart()
    {
        _isGameStart = true;
    }
}
