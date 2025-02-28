using UnityEngine;

public class StageRemover : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            GameManager.Instance.Field.RemoveBlock(
                new Vector2(other.transform.position.x, other.transform.position.z));
        }
        else if (other.CompareTag("Item"))
        {
            GameManager.Instance.Field.RemoveItem(
                new Vector2(other.transform.position.x, other.transform.position.z));
        }
        else if (other.CompareTag("Floor") || other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
