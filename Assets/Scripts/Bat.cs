using UnityEngine;

public class Bat : TargetBase
{
    bool _stop = false;

    protected override void MoveSetting()
    {
        if (!_stop)
        {
            //“®‚­‘¬“x‚Æ•ûŒüŒˆ‚ß
            _theta = Random.Range(0, 2 * Mathf.PI);
            _direction = new Vector3(Mathf.Cos(_theta), Mathf.Sin(_theta));
            _speed = Random.Range(_colorStatus.MinSpeed, _colorStatus.MaxSpeed);
        }
        else
        {
            _speed = 0;
        }
        _stop = !_stop;
    }
}
