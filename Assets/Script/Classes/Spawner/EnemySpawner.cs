using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _Enemies;
    [SerializeField] private bool _hasSpawned = false;
    [SerializeField] private int _numSpawner = 0;
    [SerializeField] private int _minEnemies = 5;
    [SerializeField] private int _maxEnemies = 7;
    [SerializeField] private GameObject _enemySpawnIndicator;
    [SerializeField] private bool _haveOneSpawnDungeon;

    public List<GameObject> Enemies
    {
        get { return _Enemies; }
        set { _Enemies = value; }
    }

    public int minEnemies
    {
        get { return _minEnemies; }
        set { _minEnemies = value; }
    }

    public int maxEnemies
    {
        get { return _maxEnemies; }
        set { _maxEnemies = value; }
    }

    public bool HasSpawned { get => _hasSpawned; set => _hasSpawned = value; }
    public int NumSpawner { get => _numSpawner; set => _numSpawner = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 PointInArea()
    {
        var bounds = GetComponent<PolygonCollider2D>().bounds;
        var center = bounds.center;

        float x = 0;
        float y = 0;
        int attempt = 0;
        do
        {
            x = Random.Range(center.x - bounds.extents.x, center.x + bounds.extents.x);
            y = Random.Range(center.y - bounds.extents.y, center.y + bounds.extents.y);
            attempt++;
        } while (!GetComponent<PolygonCollider2D>().OverlapPoint(new Vector2(x, y)) || attempt <= 100);
        Debug.Log("Attemps: " + attempt);

        return new Vector3(x, y, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!_hasSpawned)
        {
            if (other.GetComponentInChildren<Player>() != null)
            {
                StartCoroutine(SpawnEnemies());
                _hasSpawned = true;
            }
        }
    }

    public IEnumerator SpawnEnemies()
    {
        int enemiesToSpawn = Random.Range(_minEnemies, _maxEnemies);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 spawn = PointInArea();
            GameObject spawnIndicator = Instantiate(_enemySpawnIndicator, spawn, Quaternion.identity);
            spawnIndicator.transform.localScale = new Vector3(0,0,0);
            if (StateManager.Instance.currentZone == "Limbo")
            {
                spawnIndicator.GetComponent<SpriteRenderer>().color = new Color32(0x5E, 0x5E, 0x5E, 0xFF);
            }
            Sequence anim = DOTween.Sequence();
            Debug.Log(i);
            if (i==0 && _haveOneSpawnDungeon)
            {
                anim.Append(spawnIndicator.transform.DOScale(new Vector3(3, 3, 0), 1f).OnComplete(() => createEnemy(spawn, true)));
            }
            else
            {
                anim.Append(spawnIndicator.transform.DOScale(new Vector3(3, 3, 0), 1f).OnComplete(() => createEnemy(spawn, false)));

            }

            anim.Append(spawnIndicator.transform.DOScale(new Vector3(0,0,0), 1f).OnComplete(()=> Destroy(spawnIndicator)));
            yield return new WaitForSeconds(.1f);
        }
        _hasSpawned = true;
        if(StateManager.Instance.CurrentScene == "Limbo")
        {
            StateManager.Instance.EnemySpawnerState[_numSpawner] = _hasSpawned;
        }
    }

    void createEnemy(Vector3 SpawnPoint, bool giveDungeon)
    {
        GameObject spawnedEnemy = (GameObject)Instantiate(_Enemies[Random.Range(0, _Enemies.Count)], SpawnPoint, Quaternion.identity);
        if(giveDungeon)
        {
            spawnedEnemy.GetComponent<ItemDroptable>().drops[0].itemsToDrop = 1;
        }
    }
}
