using UnityEngine;
using ColorAttributes;

/// <summary>
/// ���n�̃I�u�W�F�N�g�ɃA�^�b�`
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class TargetBase : MonoBehaviour, IColorChange
{
    [SerializeField] ColorPallete _colorPalette;
    [SerializeField] ColorStatus _colorStatus;

    Rigidbody2D _rb2d;
    CircleCollider2D _cc2d;
    /// <summary>
    /// �I�u�W�F�N�g�v�[����ݒ肷��v���p�e�B
    /// </summary>
    public ObjectPoolAndSpawn OPAS { get; set; }

    Vector3 _direction;
    float _theta;
    float _speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_colorStatus)
        {
            //�������x�ƕ�������
            _theta = Random.Range(0, 2 * Mathf.PI);
            _direction = new Vector3(Mathf.Cos(_theta), Mathf.Sin(_theta));
            _speed = Random.Range(_colorStatus.MinSpeed, _colorStatus.MaxSpeed);

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

        Invoke(nameof(ReleaseToPool), 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        _rb2d.linearVelocity = _direction * _speed;
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

    public void ColorChange(ColorAttribute color)
    {

    }

    /// <summary>
    /// �I�u�W�F�N�g�v�[���ɕԂ��֐�
    /// �I�u�W�F�N�g�������鎞�ɌĂяo��
    /// </summary>
    void ReleaseToPool()
    {
        OPAS.ReleaseToPool(gameObject);
    }
}
