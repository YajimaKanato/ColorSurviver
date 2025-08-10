using System.Collections.Generic;
using UnityEngine;
using ColorAttributes;

/// <summary>
/// �e�v�[���ɃA�^�b�`
/// </summary>
public class ObjectPoolAndSpawn : MonoBehaviour
{
    [SerializeField] int _maxPoolSize;

    [Header("Instantiate")]
    [SerializeField] ColorAttribute _colorAttribute;
    [SerializeField] GameObject _prefab;
    [SerializeField] Vector3 _position;
    [SerializeField] Vector3 _rotation;

    public ColorAttribute ColorAttribute { get { return _colorAttribute; } }

    /// <summary> ��A�N�e�B�u�̃Q�[���I�u�W�F�N�g��ۊǂ��邽�߂̃L���[ </summary>
    Queue<GameObject> _poolQueue;

    int _spawnCount = 0;
    public int SpawnCount {  get { return _spawnCount; } }

    private void Start()
    {
        //�T�C�Y���w�肵�ăI�u�W�F�N�g�v�[�����쐬
        _poolQueue = new Queue<GameObject>(_maxPoolSize);
    }

    /// <summary>
    /// �Q�[���I�u�W�F�N�g���擾����֐�
    /// </summary>
    /// <param name="pos"> �I�u�W�F�N�g�̐��������W</param>
    /// <param name="rot"> �I�u�W�F�N�g�̐�������]</param>
    /// <returns> �I�u�W�F�N�g��Ԃ�</returns>
    public GameObject GetGameObject(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        if (_poolQueue == null || _poolQueue.Count == 0)
        {
            Debug.Log("�V��������");
            //��A�N�e�B�u�ȃI�u�W�F�N�g���Ȃ�������V��������
            return Instantiate(prefab, pos, rot);
        }
        else
        {
            Debug.Log("�v�[�����琶��");
            //��A�N�e�B�u�ȃI�u�W�F�N�g���擾���ė��p
            var pool = _poolQueue.Dequeue();
            pool.transform.position = pos;
            pool.transform.rotation = rot;
            pool.SetActive(true);
            return pool;
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g�v�[���ɕԂ��֐�
    /// </summary>
    /// <param name="obj"> �v�[���ɕԂ��I�u�W�F�N�g</param>
    public void ReleaseToPool(GameObject obj)
    {
        _spawnCount--;
        //�v�[���������ς����ǂ���
        if (_poolQueue.Count >= _maxPoolSize)
        {
            Debug.Log("�j��");
            Destroy(obj);
        }
        else
        {
            Debug.Log("�v�[���ɕԂ�");
            obj.SetActive(false);
            _poolQueue.Enqueue(obj);
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g�𐶐�����֐�
    /// </summary>
    public void Spawn()
    {
        _spawnCount++;
        //�I�u�W�F�N�g�𐶐�
        var spawn = GetGameObject(_prefab, _position, Quaternion.Euler(_rotation));
        spawn.transform.SetParent(this.transform);
        //���������I�u�W�F�N�g����I�u�W�F�N�g�v�[�����擾
        var oc = spawn?.GetComponent<TargetBase>();
        //�I�u�W�F�N�g�ɃI�u�W�F�N�g�v�[�����ݒ肳��Ă��Ȃ���ΐݒ�
        if (!oc.OPAS)
        {
            oc.OPAS = this;
        }
    }
}
