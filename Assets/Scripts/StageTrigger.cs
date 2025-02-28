using UnityEngine;

public class StageTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.Field.CreateStage();
            Destroy(gameObject);
        }
    }
}
