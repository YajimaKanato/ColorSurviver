using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プールに生成を指示
/// </summary>
public class SpawnManager : MonoBehaviour, IPause, IGameControl
{
    [System.Serializable]
    class PoolData
    {
        [Header("PoolData")]
        [SerializeField] ObjectPoolAndSpawn _objectPool;
        [SerializeField] float _wieght;

        public ObjectPoolAndSpawn ObjectPoolAndSpawn { get { return _objectPool; } }
        public float Wieght { get { return _wieght; } }
    }

    [SerializeField] List<PoolData> _poolList;
    [SerializeField] float _maxInterval;
    [SerializeField] float _minInterval;

    float _delta;
    float _spawnInterval;

    bool _isPause = false;
    bool _isGameClear = false;
    bool _isGameOver = false;
    bool _isGameStart = false;

    private void Start()
    {

        _spawnInterval = Random.Range(_minInterval, _maxInterval);
    }

    void Update()
    {
        if (_isGameStart && !_isGameClear && !_isGameOver)
        {
            if (!_isPause)
            {
                _delta += Time.deltaTime;
                if (_delta >= _spawnInterval)
                {
                    _delta = 0;
                    _spawnInterval = Random.Range(_minInterval, _maxInterval);
                    Spawn();
                }
            }
        }
    }

    /// <summary>
    /// 生成するゲームオブジェクトを取得する関数
    /// </summary>
    /// <returns> GameObject型が返ってくる</returns>
    void Spawn()
    {
        //重みの総和の計算
        float maxValue = 0;
        foreach (var value in _poolList)
        {
            maxValue += value.Wieght;
        }

        //重み付き確率による評価
        float rand = Random.Range(0, maxValue);
        float nowValue = 0;
        foreach (var value in _poolList)
        {//重みの加算
            nowValue += value.Wieght;
            //現在の重みが乱数値以上になったらゲームオブジェクトを生成
            if (nowValue >= rand)
            {
                value.ObjectPoolAndSpawn.Spawn();
                return;
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

    public void GameClear()
    {
        _isGameClear = true;
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
