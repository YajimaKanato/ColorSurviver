using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer))]
public class MouseAction : MonoBehaviour
{
    [SerializeField] Sprite _handOpen;
    [SerializeField] Sprite _handClose;

    CircleCollider2D _cc2d;
    GameObject _target;
    TargetBase _targetBase;
    SpriteRenderer _sr;

    Vector3 _mousePos;
    Vector3 _thisPos;

    private void Start()
    {
        _cc2d = GetComponent<CircleCollider2D>();
        _cc2d.isTrigger = true;
        _cc2d.radius = 0.1f;
        _cc2d.enabled = false;
        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = _handOpen;
    }

    private void Update()
    {
        _mousePos = Input.mousePosition;
        _thisPos = Camera.main.ScreenToWorldPoint(_mousePos);
        _thisPos.z = 0;
        transform.position = _thisPos;

        if (Input.GetMouseButtonDown(0))
        {
            _sr.sprite = _handClose;
            _cc2d.enabled = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _sr.sprite = _handOpen;
            _targetBase.IsCatched = false;
            _target?.transform.SetParent(null);
            _target = null;
            _cc2d.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_target)
        {
            _target = collision.gameObject;
            _targetBase = _target.GetComponent<TargetBase>();
            _targetBase.IsCatched = true;
            _target.transform.SetParent(this.transform);
        }
    }
}
