using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoom : MonoBehaviour
{
    [SerializeField] private GameObject _boomPrefab;

    private Dictionary<Vector2, Boom> _fieldBoomDict = new();

    public int BoomMaxCount = 1;
    public int BoomPower = 1;

    private void Update()
    {
        if (!GameManager.Instance.IsPlay)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateBoom();
        }
    }

    private void CreateBoom()
    {
        if (_fieldBoomDict.Count >= BoomMaxCount)
        {
            return;
        }

        int x = Mathf.RoundToInt(transform.position.x);
        int z = Mathf.RoundToInt(transform.position.z);

        Vector2 boomPosition = new Vector2(x, z);

        if (!_fieldBoomDict.ContainsKey(boomPosition))
        {
            GameObject boomObject = Instantiate(_boomPrefab);

            boomObject.transform.position = new Vector3(x, 0, z);

            Boom boom = boomObject.GetComponent<Boom>();
            boom.SetPlayer(this);
            boom.SetBoom(boomPosition, BoomPower);

            _fieldBoomDict.Add(boomPosition, boom);
        }
    }

    public void BoomDestroyed(Boom boom)
    {
        _fieldBoomDict.Remove(boom.Position);
    }
}
