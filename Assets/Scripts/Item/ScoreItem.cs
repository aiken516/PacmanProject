using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : Item
{
    [SerializeField] private List<GameObject> _modelList;

    private void Awake()
    {
        _modelList[Random.Range(0, _modelList.Count)].SetActive(true);
    }

    public override void GetItem(PlayerItem playerItem)
    {
    }
}
