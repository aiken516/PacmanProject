using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _enemyModel;
    [SerializeField] private GameObject _particle;
    [SerializeField] private float _speed = 3.0f;

    private GameObject _player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        { 
            GameManager.Instance.GameOver();
        }
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null)
        { 
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Mathf.RoundToInt(_player.transform.position.x) == transform.position.x)
        {
            Vector3 moveDirection = Vector3.zero;

            if (transform.position.z - _player.transform.position.z > 0)
            {
                transform.position += _speed * Time.deltaTime * Vector3.back;
                moveDirection.z = -1;
            }
            else
            {
                transform.position += _speed * Time.deltaTime * Vector3.forward;
                moveDirection.z = 1;
            }

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                _enemyModel.transform.rotation = Quaternion.Slerp(_enemyModel.transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
        else if (Mathf.RoundToInt(_player.transform.position.z) == transform.position.z)
        {
            Vector3 moveDirection = new Vector3(0, 0, 0);

            if (transform.position.x - _player.transform.position.x > 0)
            {
                transform.position += _speed * Time.deltaTime * Vector3.left;
                moveDirection.x = -1;
            }
            else
            {
                transform.position += _speed * Time.deltaTime * Vector3.right;
                moveDirection.x = 1;
            }

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                _enemyModel.transform.rotation = Quaternion.Slerp(_enemyModel.transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
        else
        {
            Vector3 dest = new(Mathf.RoundToInt(transform.position.x), 0,
                Mathf.RoundToInt(transform.position.z));

            transform.position = Vector3.MoveTowards(transform.position, dest, _speed * Time.deltaTime);
        }
    }

    public void DestroyEnemy()
    {
        Instantiate(_particle, transform.position + Vector3.up, Quaternion.identity);
        GameManager.Instance.AddScore(500);
        Destroy(gameObject);
    }
}
