using UnityEngine;

public class ExplosionRange : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().DestroyEnemy();
        }
        else if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
