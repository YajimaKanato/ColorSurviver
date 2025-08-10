using System.Collections.Generic;
using UnityEngine;
using ColorAttributes;

/// <summary>
/// 各プールにアタッチ
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

    /// <summary> 非アクティブのゲームオブジェクトを保管するためのキュー </summary>
    Queue<GameObject> _poolQueue;

    int _spawnCount = 0;
    public int SpawnCount {  get { return _spawnCount; } }

    private void Start()
    {
        //サイズを指定してオブジェクトプールを作成
        _poolQueue = new Queue<GameObject>(_maxPoolSize);
    }

    /// <summary>
    /// ゲームオブジェクトを取得する関数
    /// </summary>
    /// <param name="pos"> オブジェクトの生成時座標</param>
    /// <param name="rot"> オブジェクトの生成時回転</param>
    /// <returns> オブジェクトを返す</returns>
    public GameObject GetGameObject(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        if (_poolQueue == null || _poolQueue.Count == 0)
        {
            Debug.Log("新しく生成");
            //非アクティブなオブジェクトがなかったら新しく生成
            return Instantiate(prefab, pos, rot);
        }
        else
        {
            Debug.Log("プールから生成");
            //非アクティブなオブジェクトを取得し再利用
            var pool = _poolQueue.Dequeue();
            pool.transform.position = pos;
            pool.transform.rotation = rot;
            pool.SetActive(true);
            return pool;
        }
    }

    /// <summary>
    /// オブジェクトプールに返す関数
    /// </summary>
    /// <param name="obj"> プールに返すオブジェクト</param>
    public void ReleaseToPool(GameObject obj)
    {
        _spawnCount--;
        //プールがいっぱいかどうか
        if (_poolQueue.Count >= _maxPoolSize)
        {
            Debug.Log("破棄");
            Destroy(obj);
        }
        else
        {
            Debug.Log("プールに返す");
            obj.SetActive(false);
            _poolQueue.Enqueue(obj);
        }
    }

    /// <summary>
    /// オブジェクトを生成する関数
    /// </summary>
    public void Spawn()
    {
        _spawnCount++;
        //オブジェクトを生成
        var spawn = GetGameObject(_prefab, _position, Quaternion.Euler(_rotation));
        spawn.transform.SetParent(this.transform);
        //生成したオブジェクトからオブジェクトプールを取得
        var oc = spawn?.GetComponent<TargetBase>();
        //オブジェクトにオブジェクトプールが設定されていなければ設定
        if (!oc.OPAS)
        {
            oc.OPAS = this;
        }
    }
}
