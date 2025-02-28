using UnityEngine;

public class GameOverScroll : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;

    void Update()
    {
        transform.position += _speed * Time.deltaTime * Vector3.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
