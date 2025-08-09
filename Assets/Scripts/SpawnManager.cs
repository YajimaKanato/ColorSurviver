using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プールに生成を指示
/// </summary>
public class SpawnManager : MonoBehaviour
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

    private void Start()
    {

        _spawnInterval = Random.Range(_minInterval, _maxInterval);
    }

    void Update()
    {
        _delta += Time.deltaTime;
        if (_delta >= _spawnInterval)
        {
            _delta = 0;
            _spawnInterval = Random.Range(_minInterval, _maxInterval);
            Spawn();
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
}
