using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Transform[] _spawners;
    [SerializeField] private float _repeatRate;
    [SerializeField] private int _poolCapacity = 3;
    [SerializeField] private int _poolMaxSize = 3;
    [SerializeField] private CoinPickUp _collector;

    private ObjectPool<Coin> _coinsPool;
    private Coroutine _spawnCoroutine;

    private void Awake()
    {
        CreatePool();
        _collector.onPicked += ReleaseCoin;
    }

    private ObjectPool<Coin> CreatePool()
    {
        _coinsPool = new ObjectPool<Coin>(
            createFunc: () => Instantiate(),
            actionOnGet: (coin) => GetOnAction(coin),
            actionOnRelease: coin => OnRelease(coin),
            actionOnDestroy: (coin) => Destroy(coin.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);

        return _coinsPool;
    }

    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(SpawnCoins());
    }

    private void OnDisable()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnCoins()
    {
        var wait = new WaitForSeconds(_repeatRate);

        while (enabled)
        {
            yield return wait;

            Transform selectedSpawner = GetSpawnPosition();

            if (_coinsPool.CountActive < _poolMaxSize)
            {
                _coinsPool.Get();
            }
        }
    }

    private Coin Instantiate()
    {
        return Instantiate(_coinPrefab);
    }

    private void GetOnAction(Coin coin)
    {
        Transform spawnTransform = GetSpawnPosition();
        coin.transform.position = spawnTransform.position;
        coin.gameObject.SetActive(true);
    }

    private void ReleaseCoin(Coin coin)
    {
        Debug.Log($"Монета собрана: {coin.gameObject.name}");
        CoinPickUp pickUp = coin.GetComponent<CoinPickUp>();

        if (pickUp != null)
        {
            pickUp.onPicked -= ReleaseCoin;
        }

        _coinsPool.Release(coin);
    }

    private void OnRelease(Coin coin)
    {
        coin.gameObject.SetActive(false);
    }

    private Transform GetSpawnPosition()
    {
        int randomIndex = Random.Range(0, _spawners.Length);
        return _spawners[randomIndex];
    }

    private void OnDestroy()
    {
        if (_coinsPool != null)
            _coinsPool.Clear();

        if (_collector != null)
            _collector.onPicked -= OnRelease;
    }
}