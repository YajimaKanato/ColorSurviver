using UnityEngine;

/// <summary>
/// �I�u�W�F�N�g�ɃA�^�b�`���A�ʂ̃X�N���v�g����Ăяo��
/// </summary>
public class ObjectActionForPool : MonoBehaviour
{
    /// <summary>
    /// �I�u�W�F�N�g�v�[����ݒ肷��v���p�e�B
    /// </summary>
    public ObjectPoolAndSpawn OPAS { get; set; }

    /// <summary>
    /// �I�u�W�F�N�g�v�[���ɕԂ��֐�
    /// �I�u�W�F�N�g�������鎞�ɌĂяo��
    /// </summary>
    public void ReleaseToPool()
    {
        OPAS.ReleaseToPool(gameObject);
    }
}
