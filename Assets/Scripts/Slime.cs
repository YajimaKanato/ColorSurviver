using UnityEngine;

public class Slime : TargetBase
{
    protected override void MoveSetting()
    {
        //�������x�ƕ�������
        _theta = Random.Range(0, 2 * Mathf.PI);
        _direction = new Vector3(Mathf.Cos(_theta), Mathf.Sin(_theta));
        _speed = Random.Range(_colorStatus.MinSpeed, _colorStatus.MaxSpeed);
    }
}
