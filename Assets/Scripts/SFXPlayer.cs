using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;

    void Start()
    {
        _audioSource.Play();
        Destroy(this.gameObject, _audioSource.clip.length);
    }
}
