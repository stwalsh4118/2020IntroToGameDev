using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrassAbility : Ability
{

    [SerializeField] private GameObject grass;
    [SerializeField] private int _numGrassSpawned = 100;
    [SerializeField] private float _spawnRadius = 5f;
    [SerializeField] private float _grassLifetime = 5f;


    public int numGrassSpawned
    {
        get { return _numGrassSpawned; }
        set { _numGrassSpawned = value; }
    }

    public float spawnRadius
    {
        get { return _spawnRadius; }
        set { _spawnRadius = value; }
    }

    public float grassLifetime
    {
        get { return _grassLifetime; }
        set { _grassLifetime = value; }
    }

    public override IEnumerator activateAbility()
    {
        for (int i = 0; i < _numGrassSpawned; i++)
        {
            Transform player = FindObjectOfType<Player>().transform.parent.transform;
            float angle = Random.Range(0, 360f);
            float spawnPosition = Random.Range(1f, _spawnRadius);
            GameObject grassSpawned = (GameObject)Instantiate(grass, new Vector3(player.position.x + (spawnPosition * Mathf.Cos(angle)), player.position.y + (spawnPosition * Mathf.Sin(angle)), 0f), Quaternion.identity);
            grassSpawned.transform.localScale = new Vector3(grassSpawned.transform.localScale.x, 0, grassSpawned.transform.localScale.z);
            Sequence anim = DOTween.Sequence();
            anim.Insert(0, grassSpawned.transform.DOScaleY(2, .5f).SetDelay(Random.Range(0, .3f)));
            anim.Insert(0, grassSpawned.transform.DOScaleY(0, .5f).SetDelay(Random.Range(_grassLifetime, _grassLifetime + .3f)));
            anim.AppendCallback(() => Destroy(grassSpawned));
            yield return null;
        }
    }

}
