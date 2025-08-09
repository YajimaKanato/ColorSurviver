using UnityEngine;
using ColorAttributes;

/// <summary>
/// 白地のオブジェクトにアタッチ
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class TargetBase : MonoBehaviour, IColorChange
{
    [SerializeField] ColorPallete _colorPalette;
    [SerializeField] ColorStatus _colorStatus;

    Rigidbody2D _rb2d;
    CircleCollider2D _cc2d;
    /// <summary>
    /// オブジェクトプールを設定するプロパティ
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
            //動く速度と方向決め
            _theta = Random.Range(0, 2 * Mathf.PI);
            _direction = new Vector3(Mathf.Cos(_theta), Mathf.Sin(_theta));
            _speed = Random.Range(_colorStatus.MinSpeed, _colorStatus.MaxSpeed);

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

    public void ColorChange(ColorAttribute color)
    {

    }

    /// <summary>
    /// オブジェクトプールに返す関数
    /// オブジェクトが消える時に呼び出す
    /// </summary>
    void ReleaseToPool()
    {
        OPAS.ReleaseToPool(gameObject);
    }
}
