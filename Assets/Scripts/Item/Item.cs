using System;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private int _score = 0;
    [SerializeField] private GameObject _particle;

    protected void Start()
    {
        GameManager.Instance.Field.AddItemToDict(new Vector2(transform.position.x, transform.position.z), this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerItem playerItem = other.gameObject.GetComponent<PlayerItem>();
            GetItem(playerItem);
            GameManager.Instance.Field.RemoveItem(new Vector2(transform.position.x, transform.position.z));
            GameManager.Instance.AddScore(_score);
            Instantiate(_particle, transform.position + Vector3.up, Quaternion.identity);
        }
    }

    public abstract void GetItem(PlayerItem playerItem);

    private void Update()
    {
        transform.rotation = new Quaternion(0, transform.rotation.y + Time.deltaTime * 4, 0, 0);
    }
}
