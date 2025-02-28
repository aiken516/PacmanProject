using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private PlayerAnimation _playerAnimation;
    private Rigidbody _rigidbody;

    //private Vector3 _moveInput;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        if (!GameManager.Instance.IsPlay)
        {
            return;
        }

        //float horizintalMove = Input.GetAxis("Horizontal");
        //float VerticalMove = Input.GetAxis("Vertical");

        //Vector3 directtion = new Vector3(horizintalMove, 0, VerticalMove);

        //transform.position += _speed * Time.deltaTime * directtion;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0.0f, moveZ);

        if (moveDirection != Vector3.zero) // 움직일 때만 회전
        {
            _playerAnimation.SetBool("Move", true);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            _playerAnimation.SetPlayerRotation(
                Quaternion.Slerp(_playerAnimation.GetPlayerRotation(), targetRotation, Time.deltaTime * 10f));
        }
        else 
        {
            _playerAnimation.SetBool("Move", false);
        }


        //_moveInput = new Vector3(moveX, 0, moveZ).normalized * _speed;
        _rigidbody.linearVelocity = new Vector3(moveX, 0, moveZ).normalized * _speed;

    }
/*
    void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector3(_moveInput.x, _rigidbody.linearVelocity.y, _moveInput.z);
    }*/
}
