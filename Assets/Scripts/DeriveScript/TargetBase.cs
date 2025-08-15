using UnityEngine;
using ColorAttributes;
using System.Collections;

/// <summary>
/// 白地のオブジェクトにアタッチ
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public abstract class TargetBase : MonoBehaviour, IPause, IGameControl
{
    [SerializeField] ColorPallete _colorPalette;
    [SerializeField] protected ColorStatus _colorStatus;
    [SerializeField] ColorAttribute _reverseColor;
    [SerializeField] Vector3 _catchOffset;
    [SerializeField] float _originalSpeed = 1f;
    [SerializeField] int _score = 100;
    [SerializeField] int _colorlessValue = 4;
    [SerializeField] float _maxMoveInterval = 2;
    [SerializeField] float _minMoveInterval = 1;
    [SerializeField] GameObject _text;

    public int Score { get { return _score; } }

    Rigidbody2D _rb2d;
    ObjectPoolAndSpawn _objectPool;
    Animator _animator;
    Animator _childAnim;
    public ColorStatus ColorStatus { get { return _colorStatus; } }

    /// <summary>
    /// オブジェクトプールを設定するプロパティ
    /// </summary>
    public ObjectPoolAndSpawn OPAS { get; set; }

    IEnumerator _coroutine;
    protected Vector3 _direction;
    protected float _theta;
    protected float _speed;
    float _sqrt2;
    float _delta = 1;
    bool _isCatched = false;
    public bool IsCatched { get { return _isCatched; } set { _isCatched = value; } }

    bool _isPause = false;
    bool _isGameClear = false;
    bool _isGameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_colorStatus)
        {
            //諸々のセッティング
            _rb2d = GetComponent<Rigidbody2D>();
            _rb2d.gravityScale = 0;
            _rb2d.freezeRotation = true;
            _animator = GetComponent<Animator>();
            _childAnim = transform.GetChild(0).GetComponent<Animator>();
            _coroutine = MoveChange();
            StartCoroutine(_coroutine);
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
        if (_isPause || _isGameClear || _isGameOver)
        {
            _rb2d.linearVelocity = Vector3.zero;
        }
        else
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
    }

    protected abstract void MoveSetting();

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
        Vector3 instPos = Camera.main.WorldToScreenPoint(transform.position);
        instPos.z = -10;
        var text = Instantiate(_text, instPos, Quaternion.identity);
        text.transform.SetParent(GameObject.Find("Canvas").transform);
        text.GetComponent<ScoreGetText>().Score = _score;
        ReleaseToPool();
    }

    /// <summary>
    /// オブジェクトプールに返す関数
    /// オブジェクトが消える時に呼び出す
    /// </summary>
    void ReleaseToPool()
    {
        OPAS.ReleaseToPool(gameObject);
    }

    IEnumerator MoveChange()
    {
        float moveInterval = Random.Range(_minMoveInterval, _maxMoveInterval);
        while (true)
        {
            _delta += Time.deltaTime;
            if (_delta >= moveInterval)
            {
                _delta = 0;
                MoveSetting();

                _sqrt2 = Mathf.Sqrt(2);
                if (1 / _sqrt2 <= Vector3.Dot(_direction, Vector3.right))
                {
                    _animator.SetTrigger("Right");
                    _childAnim.SetTrigger("Right");
                }
                else if (1 / _sqrt2 <= Vector3.Dot(_direction, Vector3.up))
                {
                    _animator.SetTrigger("Up");
                    _childAnim.SetTrigger("Up");
                }
                else if (1 / _sqrt2 <= Vector3.Dot(_direction, Vector3.down))
                {
                    _animator.SetTrigger("Down");
                    _childAnim.SetTrigger("Down");
                }
                else if (1 / _sqrt2 <= Vector3.Dot(_direction, Vector3.left))
                {
                    _animator.SetTrigger("Left");
                    _childAnim.SetTrigger("Left");
                }
            }

            if (_isGameClear || _isGameOver)
            {
                yield break;
            }
            yield return null;
        }
    }

    public void Pause()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _isPause = true;
    }

    public void Resume()
    {
        if (_coroutine != null)
        {
            StartCoroutine(_coroutine);
        }
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

    }
}
