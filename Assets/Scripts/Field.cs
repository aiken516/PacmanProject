using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Normal = 0,
    Metal = 1,
}

public enum ItemType
{ 
    ExtraBoom = 0,
    ExplosionPower = 1,
    Score = 2,
}

public class Field : MonoBehaviour
{
    [SerializeField] private StageGenerator _generator;

    [SerializeField] private GameObject _floorPrefab;

    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private GameObject _metalBlockPrefab;

    [SerializeField] private List<GameObject> _itemPrefabList;

    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private List<GameObject> _blockObjectPool;
    [SerializeField] private List<GameObject> _metalBlockObjectPool;

    [SerializeField] private GameObject _blockParent;

    private Dictionary<Vector2, Block> _blockDict = new();
    private Dictionary<Vector2, Item> _itemDict = new();

    private void Start()
    {
        CreateStage();
    }

    public bool CreateBlock(Vector2 position, BlockType type = BlockType.Normal)
    {
        if (CheckBlock(position))
        {
            return false;
        }

        GameObject blockObject = GetBlockFromPool(type);
        blockObject.SetActive(true);

        blockObject.transform.position = new Vector3(position.x, 0, position.y);

        Block block = blockObject.GetComponent<Block>();

        block.SetBlock(type);
        _blockDict.Add(position, block);

        return true;
    }

    private GameObject GetBlockFromPool(BlockType type)
    {
        GameObject returnObject;

        if (type == BlockType.Metal)
        {
            if (_metalBlockObjectPool.Count <= 0)
            {
                _metalBlockObjectPool.Add(Instantiate(_metalBlockPrefab, _blockParent.transform));
            }

            returnObject = _metalBlockObjectPool[0];
            _metalBlockObjectPool.Remove(returnObject);
        }
        else
        {
            if (_blockObjectPool.Count <= 0)
            {
                _blockObjectPool.Add(Instantiate(_blockPrefab, _blockParent.transform));
            }

            returnObject = _blockObjectPool[0];
            _blockObjectPool.Remove(returnObject);
        }

        return returnObject;
    }

    public void DestroyBlock(Vector2 position)
    {
        Block destroyBlock = _blockDict[position];
        destroyBlock.GetDamage();
    }

    public void RemoveBlock(Vector2 position)
    {
        Block removeBlock = _blockDict[position];
        _blockDict.Remove(position);

        removeBlock.gameObject.SetActive(false);
        if (removeBlock.Type == BlockType.Metal)
        {
            _metalBlockObjectPool.Add(removeBlock.gameObject);
        }
        else
        {
            _blockObjectPool.Add(removeBlock.gameObject);
        }
    }

    public bool CheckBlock(Vector2 position) => _blockDict.ContainsKey(position);
    public Block GetBlock(Vector2 position) => _blockDict[position];
    public void AddBlockToField(Vector2 position, Block block)
    {
        if (!_blockDict.ContainsKey(position))
        {
            _blockDict.Add(position, block);
        }
    }
    public bool CheckItem(Vector2 position) => _itemDict.ContainsKey(position);
    public void AddItemToDict(Vector2 position, Item item)
    {
        if (!_itemDict.ContainsKey(position))
        {
            _itemDict.Add(position, item);
        }
    }

    public void CreateItem(Vector2 position, ItemType type)
    {
        if (CheckItem(position))
        {
            return;
        }

        GameObject itemObject = Instantiate(_itemPrefabList[(int)type]);

        itemObject.transform.position = new Vector3(position.x, 0, position.y);

        Item item = itemObject.GetComponent<Item>();

        _itemDict.Add(position, item);
    }

    public void RemoveItem(Vector2 position)
    {
        GameObject removeItem = _itemDict[position].gameObject;
        _itemDict.Remove(position);

        Destroy(removeItem);
    }

    public void CreateEnemy(Vector2 position)
    {
        if (CheckItem(position) || CheckBlock(position))
        {
            return;
        }

        GameObject enemyObject = Instantiate(_enemyPrefab);

        enemyObject.transform.position = new Vector3(position.x, 0, position.y);
    }

    public void CreateStage()
    {
        _generator.CreateStage();
        GameObject floorObject = Instantiate(_floorPrefab);
        floorObject.transform.position += new Vector3(0, 0, _generator.CurrentPosition);

        _generator.ProgressCurrentPosition();
    }
}
