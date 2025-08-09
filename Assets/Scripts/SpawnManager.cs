using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v�[���ɐ������w��
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
    /// ��������Q�[���I�u�W�F�N�g���擾����֐�
    /// </summary>
    /// <returns> GameObject�^���Ԃ��Ă���</returns>
    void Spawn()
    {
        //�d�݂̑��a�̌v�Z
        float maxValue = 0;
        foreach (var value in _poolList)
        {
            maxValue += value.Wieght;
        }

        //�d�ݕt���m���ɂ��]��
        float rand = Random.Range(0, maxValue);
        float nowValue = 0;
        foreach (var value in _poolList)
        {//�d�݂̉��Z
            nowValue += value.Wieght;
            //���݂̏d�݂������l�ȏ�ɂȂ�����Q�[���I�u�W�F�N�g�𐶐�
            if (nowValue >= rand)
            {
                value.ObjectPoolAndSpawn.Spawn();
                return;
            }
        }
    }
}
