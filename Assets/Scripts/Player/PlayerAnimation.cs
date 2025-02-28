using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _playerModel;

    public void SetBool(string name, bool param)
    { 
        _animator.SetBool(name, param);
    }

    public void SetTrigger(string name)
    {
        _animator.SetTrigger(name);
    }

    public void SetPlayerRotation(Quaternion quaternion)
    {
        _playerModel.transform.rotation = quaternion;
    }

    public Quaternion GetPlayerRotation() => _playerModel.transform.rotation;
}
