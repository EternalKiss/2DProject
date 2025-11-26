using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Item[] _itemsPrefab;
    [SerializeField] private ItemsType _itemsType;
    [SerializeField] private Transform[] _coinSpawners;
    [SerializeField] private Transform[] _healthPackSpawners;
    [SerializeField] private float _repeatRate;
    [SerializeField] private int _poolCapacity = 3;
    [SerializeField] private int _poolMaxSize = 3;
    [SerializeField] private ItemPickUp _collector;

    private Dictionary<ItemsType, ObjectPool<Item>> _itemsPool = new Dictionary<ItemsType, ObjectPool<Item>>();
    private Coroutine _spawnCoinCoroutine;
    private Coroutine _spawnHealthPackCoroutine;

    private void Awake()
    {
        for (int i = 0; i < _itemsPrefab.Length; i++)
        {
            CreatePool(_itemsPrefab[i]);
            _collector.ItemPicked += ReleaseItem;
        }

    }

    private void OnEnable()
    {
        _spawnHealthPackCoroutine = StartCoroutine(SpawnHealthPack());
        _spawnCoinCoroutine = StartCoroutine(SpawnCoins());
    }

    private void OnDisable()
    {
        if (_spawnCoinCoroutine != null && _spawnHealthPackCoroutine != null)
        {
            StopCoroutine(_spawnCoinCoroutine);
            StopCoroutine(_spawnHealthPackCoroutine);

            _spawnHealthPackCoroutine = null;
            _spawnCoinCoroutine = null;
        }
    }

    private ObjectPool<Item> CreatePool(Item itemPrefab)
    {
        var pool = new ObjectPool<Item>(
            createFunc: () => Instantiate(itemPrefab),
            actionOnGet: (item) => GetOnAction(item),
            actionOnRelease: item => OnRelease(item),
            actionOnDestroy: (item) => Destroy(item.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);

        _itemsPool[itemPrefab.Type] = pool;
        return pool;
    }

    private IEnumerator SpawnCoins()
    {
        var wait = new WaitForSeconds(_repeatRate);

        while (enabled)
        {
            yield return wait;

            Transform selectedSpawner = GetSpawnPositionCoin();

            if (_itemsPool[ItemsType.Coin].CountActive < _poolMaxSize)
            {
                _itemsPool[ItemsType.Coin].Get();
            }
        }
    }

    private IEnumerator SpawnHealthPack()
    {
        while (enabled)
        {
            yield return null;

            Transform selectedSpawner = GetSpawnPositionHealthPack();

            if (_itemsPool[ItemsType.HealthPack].CountActive < _poolMaxSize)
            {
                _itemsPool[ItemsType.HealthPack].Get();
            }

        }
    }

    private void GetOnAction(Item item)
    {
        Transform spawnPosition = null;

        switch (item.Type)
        {
            case ItemsType.Coin:
                spawnPosition = GetSpawnPositionCoin();
                break;
            case ItemsType.HealthPack:
                spawnPosition = GetSpawnPositionHealthPack();
                break;
        }

        if (spawnPosition != null)
        {
            item.transform.position = spawnPosition.position;
        }
    }

    private void ReleaseItem(Item item)
    {
        if(item == null || !item.gameObject.activeSelf)
        {
            return;
        }

        ItemPickUp pickUp = item.GetComponent<ItemPickUp>();

        if (pickUp != null)
        {
            pickUp.ItemPicked -= ReleaseItem;
        }

        if (_itemsPool.ContainsKey(item.Type))
        {
            _itemsPool[item.Type].Release(item);
        }
    }

    private void OnRelease(Item item)
    {
        item.gameObject.SetActive(false);
    }

    private Transform GetSpawnPositionCoin()
    {
        int randomIndex = Random.Range(0, _coinSpawners.Length);
        return _coinSpawners[randomIndex];
    }

    private Transform GetSpawnPositionHealthPack()
    {
        int randomIndex = Random.Range(0, _healthPackSpawners.Length);
        return _healthPackSpawners[randomIndex];
    }

    private void OnDestroy()
    {
        if (_itemsPool != null)
        {
            _itemsPool.Clear();
        }

        if (_collector != null)
        {
            _collector.ItemPicked -= OnRelease;
        }
    }
}