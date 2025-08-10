using UnityEngine;
using ColorAttributes;

/// <summary>
/// ���n�̃I�u�W�F�N�g�ɃA�^�b�`
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
    /// �I�u�W�F�N�g�v�[����ݒ肷��v���p�e�B
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

            //���X�̃Z�b�e�B���O
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
                Debug.LogWarning("ScriptableObject/ColorPalette���ݒ肳��Ă��܂���");
            }
        }
        else
        {
            Debug.LogWarning("ScriptableObject/ColorStatus���ݒ肳��Ă��܂���");
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
        //�������x�ƕ�������
        _theta = Random.Range(0, 2 * Mathf.PI);
        _direction = new Vector3(Mathf.Cos(_theta), Mathf.Sin(_theta));
        _speed = Random.Range(_colorStatus.MinSpeed, _colorStatus.MaxSpeed);
    }

    /// <summary>
    /// �F��ς���֐�
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
    /// �I�u�W�F�N�g�v�[���ɕԂ��֐�
    /// �I�u�W�F�N�g�������鎞�ɌĂяo��
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
