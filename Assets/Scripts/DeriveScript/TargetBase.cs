using UnityEngine;
using ColorAttributes;

/// <summary>
/// 白地のオブジェクトにアタッチ
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class TargetBase : MonoBehaviour
{
    [SerializeField] ColorPallete _colorPalette;
    [SerializeField] ColorStatus _colorStatus;
    [SerializeField] ColorAttribute _reverseColor;
    [SerializeField] Vector3 _catchOffset;
    [SerializeField] float _originalSpeed = 1f;
    [SerializeField] int _score = 100;
    [SerializeField] int _colorlessValue = 4;

    public int Score { get { return _score; } }

    Rigidbody2D _rb2d;
    CircleCollider2D _cc2d;
    ObjectPoolAndSpawn _objectPool;
    public ColorStatus ColorStatus { get { return _colorStatus; } }

    /// <summary>
    /// オブジェクトプールを設定するプロパティ
    /// </summary>
    public ObjectPoolAndSpawn OPAS { get; set; }

    protected Vector3 _direction;
    protected float _theta;
    protected float _speed;
    bool _isCatched = false;
    public bool IsCatched { get { return _isCatched; } set { _isCatched = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_colorStatus)
        {
            MoveSetting();

            //諸々のセッティング
            _rb2d = GetComponent<Rigidbody2D>();
            _rb2d.gravityScale = 0;
            _rb2d.freezeRotation = true;
            _cc2d = GetComponent<CircleCollider2D>();
            if (_colorPalette)
            {
                ColorSetting();
            }
            else
            {
                Debug.LogWarning("ScriptableObject/ColorPaletteが設定されていません");
            }
        }
        else
        {
            Debug.LogWarning("ScriptableObject/ColorStatusが設定されていません");
        }

        var reversePool = FindObjectsByType<ObjectPoolAndSpawn>(FindObjectsSortMode.None);
        foreach (var pool in reversePool)
        {
            if (pool.ColorAttribute == _reverseColor)
            {
                _objectPool = pool;
                Debug.Log(_colorStatus.ColorAttribute.ToString() + ":reverse=>" + pool.ColorAttribute);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCatched)
        {
            gameObject.transform.position = gameObject.transform.parent.position + _catchOffset;
        }

        if (_objectPool.SpawnCount >= _colorlessValue)
        {
            ColorChange(true);
        }
        else
        {
            ColorChange(false);
        }
    }

    private void FixedUpdate()
    {
        if (!_isCatched)
        {
            _rb2d.linearVelocity = _direction * _speed * _originalSpeed;
        }
        else
        {
            _rb2d.linearVelocity = Vector3.zero;
        }
    }

    protected void MoveSetting()
    {
        //動く速度と方向決め
        _theta = Random.Range(0, 2 * Mathf.PI);
        _direction = new Vector3(Mathf.Cos(_theta), Mathf.Sin(_theta));
        _speed = Random.Range(_colorStatus.MinSpeed, _colorStatus.MaxSpeed);
    }

    /// <summary>
    /// 色を変える関数
    /// </summary>
    void ColorSetting()
    {
        foreach (var c in _colorPalette.ColorDataList)
        {
            if (c.ColorAttribute == _colorStatus.ColorAttribute)
            {
                gameObject.GetComponent<SpriteRenderer>().color = c.Color;
                return;
            }
        }
    }

    public void ColorChange(bool over)
    {
        if (over)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
        }
        else
        {
            ColorSetting();
        }
    }

    public void SuccessClassification()
    {

        ReleaseToPool();
    }

    /// <summary>
    /// オブジェクトプールに返す関数
    /// オブジェクトが消える時に呼び出す
    /// </summary>
    protected void ReleaseToPool()
    {
        OPAS.ReleaseToPool(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Destroy")
        {
            ReleaseToPool();
        }
    }
}
