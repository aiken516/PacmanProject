using System;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private int _score = 0;
    [SerializeField] private GameObject _particle;
    [SerializeField] private GameObject _sound;

    protected void Start()
    {
        GameManager.Instance.Field.AddItemToDict(new Vector2(transform.position.x, transform.position.z), this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject sfx = Instantiate(_sound);
            sfx.SetActive(true);
            sfx.transform.position = transform.position;

            Instantiate(_particle, transform.position + Vector3.up, Quaternion.identity);

            PlayerItem playerItem = other.gameObject.GetComponent<PlayerItem>();
            GetItem(playerItem);
            GameManager.Instance.AddScore(_score);

            GameManager.Instance.Field.RemoveItem(new Vector2(transform.position.x, transform.position.z));
        }
    }

    public abstract void GetItem(PlayerItem playerItem);

    private void Update()
    {
        transform.rotation = new Quaternion(0, transform.rotation.y + Time.deltaTime * 4, 0, 0);
    }
}
