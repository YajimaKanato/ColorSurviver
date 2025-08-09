using UnityEngine;
using ColorAttributes;

[RequireComponent(typeof(CircleCollider2D))]
public class TargetClassification : MonoBehaviour
{
    [SerializeField] ColorAttribute _targetColor;

    CircleCollider2D _cc2d;
    GameObject _target;
    TargetBase _targetBase;

    private void Start()
    {
        _cc2d = GetComponent<CircleCollider2D>();
        _cc2d.isTrigger = true;
    }

    private void Update()
    {
        if (_target)
        {
            if (!_targetBase.IsCatched)
            {
                if (_targetBase.ColorStatus.ColorAttribute == _targetColor)
                {
                    _targetBase.SuccessClassification();
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_target)
        {
            _targetBase = collision.gameObject.GetComponent<TargetBase>();
            if (_targetBase)
            {
                if (_targetBase.IsCatched)
                {
                    _target = collision.gameObject;
                }
            }
        }
    }
}
