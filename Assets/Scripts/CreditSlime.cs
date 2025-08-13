using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class CreditSlime : MonoBehaviour
{
    [SerializeField] float _speed = 5;

    Rigidbody2D _rb2d;
    Animator _animator;
    Animator _animatorClear;

    Vector3 _direction;

    float _speedX, _speedY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0;
        _rb2d.freezeRotation = true;
        _animator = GetComponent<Animator>();
        _animatorClear = transform.GetChild(0).GetComponent<Animator>();
        _direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        _speedX = Input.GetAxisRaw("Horizontal");
        _speedY = Input.GetAxisRaw("Vertical");

        _animator.SetInteger("SpeedX",(int)_speedX);
        _animatorClear.SetInteger("SpeedX", (int)_speedX);
        _animator.SetInteger("SpeedY", (int)_speedY);
        _animatorClear.SetInteger("SpeedY", (int)_speedY);

        _direction.x = _speedX;
        _direction.y = _speedY;
    }

    private void FixedUpdate()
    {
        _rb2d.linearVelocity = _direction * _speed;
    }
}
