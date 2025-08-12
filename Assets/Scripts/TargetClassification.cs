using UnityEngine;
using ColorAttributes;

[RequireComponent(typeof(CircleCollider2D))]
public class TargetClassification : MonoBehaviour, IPause, IGameControl
{
    [SerializeField] ColorAttribute _targetColor;

    CircleCollider2D _cc2d;
    GameObject _target;
    TargetBase _targetBase;

    bool _isPause = false;
    bool _isGameClear = false;
    bool _isGameOver = false;

    private void Start()
    {
        _cc2d = GetComponent<CircleCollider2D>();
        _cc2d.isTrigger = true;
    }

    private void Update()
    {
        if (!_isPause && !_isGameClear && !_isGameOver)
        {
            if (_target)
            {
                if (!_targetBase.IsCatched)
                {
                    if (_targetBase.ColorStatus.ColorAttribute == _targetColor)
                    {
                        FindFirstObjectByType<ScoreManager>().AddScore(_targetBase.ColorStatus.ColorAttribute, _targetBase.Score);
                        _targetBase.SuccessClassification();
                        _target = null;
                    }
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
        else
        {
            if (_targetBase)
            {
                if (!_targetBase.IsCatched)
                {
                    _target = null;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_target)
        {
            _targetBase = collision.gameObject.GetComponent<TargetBase>();
            if (_targetBase)
            {
                if (_targetBase.IsCatched)
                {
                    _target = null;
                }
            }
        }
    }

    public void Pause()
    {
        _isPause = true;
    }

    public void Resume()
    {
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
