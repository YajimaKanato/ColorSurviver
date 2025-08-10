using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] int _maxTargetCount = 20;
    [SerializeField] float _countDown = 10;
    [SerializeField] Text _countDownText;

    ObjectPoolAndSpawn[] _objectPool;
    Coroutine _coroutine;
    int _currentTargetCount = 0;
    int _count;

    private void Start()
    {
        _objectPool = FindObjectsByType<ObjectPoolAndSpawn>(FindObjectsSortMode.None);
        _countDownText.text = "";
    }

    private void Update()
    {
        GetTargetCount();

        if (_currentTargetCount >= _maxTargetCount)
        {
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(CountDownCoroutine());
            }
        }
        else
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
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
}
