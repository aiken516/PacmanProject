using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boom : MonoBehaviour
{
    [SerializeField] GameObject _boomRangePrefab;

    private PlayerBoom _playerBoom = null;
    public Vector2 Position => _position;
    private Vector2 _position;
 
    private int _explosionPower;

    public void SetPlayer(PlayerBoom playerBoom)
    { 
        _playerBoom = playerBoom;
    }

    public void SetBoom(Vector2 position, int explosionPower = 1, float explosionTime = 2.0f)
    {
        _position = position;
        _explosionPower = explosionPower;

        GameManager.Instance.PlayAfterCoroutine(() => { Explosion(); }, explosionTime);
    }

    public void Explosion()
    {
        List<Vector2> udrl = new() { Vector2.up, Vector2.down, Vector2.right, Vector2.left };

        List<Vector2> explosionRange = new();
        explosionRange.Add(Position);

        foreach (Vector2 direction in udrl)
        {
            for (int i = 1; i < _explosionPower + 1; i++)
            {
                if (GameManager.Instance.Field.CheckBlock(_position + direction * i))
                {
                    GameManager.Instance.Field.DestroyBlock(_position + direction * i);
                    break;
                }
                explosionRange.Add(_position + direction * i);
            }
        }

        foreach (Vector2 range in explosionRange)
        {
            GameObject boomRange = Instantiate(_boomRangePrefab);
            boomRange.transform.position = new Vector3(range.x, 0, range.y);
        }

        _playerBoom.BoomDestroyed(this);
        Destroy(gameObject);
    }
}
