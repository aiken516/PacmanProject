using CartoonFX;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private GameObject _particle;

    [SerializeField] private int _score = 10;

    [SerializeField] private int _maxDurability = 1;
    private int _currentDurability = 1;

    public BlockType Type;

    private void Start()
    {
        GameManager.Instance.Field.AddBlockToField(new Vector2(transform.position.x, transform.position.z), this);
        _currentDurability = _maxDurability;
    }

    public void SetBlock(BlockType type)
    {
        _currentDurability = _maxDurability;
        Type = type;
    }

    public void GetDamage()
    {
        _currentDurability--;
        if (_currentDurability <= 0)
        {
            GameManager.Instance.Field.RemoveBlock(new Vector2(transform.position.x, transform.position.z));
            Instantiate(_particle, transform.position + Vector3.up, Quaternion.identity);

            GetScore();
        }
    }

    public void GetScore()
    {
        GameManager.Instance.AddScore(_score);
    }
}
