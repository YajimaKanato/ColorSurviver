using UnityEngine;

/// <summary>
/// オブジェクトにアタッチし、別のスクリプトから呼び出す
/// </summary>
public class ObjectActionForPool : MonoBehaviour
{
    /// <summary>
    /// オブジェクトプールを設定するプロパティ
    /// </summary>
    public ObjectPoolAndSpawn OPAS { get; set; }

    /// <summary>
    /// オブジェクトプールに返す関数
    /// オブジェクトが消える時に呼び出す
    /// </summary>
    public void ReleaseToPool()
    {
        OPAS.ReleaseToPool(gameObject);
    }
}
