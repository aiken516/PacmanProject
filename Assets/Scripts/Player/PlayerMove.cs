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
            GetComponent<PlayerSound>().WalkSFX.gameObject.SetActive(false);
            return;
        }

        //float horizintalMove = Input.GetAxis("Horizontal");
        //float VerticalMove = Input.GetAxis("Vertical");

        //Vector3 directtion = new Vector3(horizintalMove, 0, VerticalMove);

        //transform.position += _speed * Time.deltaTime * directtion;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0.0f, moveZ);

        if (moveDirection != Vector3.zero) // 움직일 때
        {
            GetComponent<PlayerSound>().WalkSFX.gameObject.SetActive(true);
            _playerAnimation.SetBool("Move", true);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            _playerAnimation.SetPlayerRotation(
                Quaternion.Slerp(_playerAnimation.GetPlayerRotation(), targetRotation, Time.deltaTime * 10f));
        }
        else
        {
            GetComponent<PlayerSound>().WalkSFX.gameObject.SetActive(false);

            _playerAnimation.SetBool("Move", false);

            Vector3 dest = new(Mathf.RoundToInt(transform.position.x), transform.position.y,
                Mathf.RoundToInt(transform.position.z));

            transform.position = Vector3.MoveTowards(transform.position, dest, _speed * Time.deltaTime * 0.1f);
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
