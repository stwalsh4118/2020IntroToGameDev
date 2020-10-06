using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NotAMinecraftBlockAbility : Ability
{

    [SerializeField] private GameObject pillar;
    [SerializeField] private int _numPillarsSpawned = 3;
    [SerializeField] private float _distanceBetweenPillars = 2f;
    [SerializeField] private float _pillarLifetime = 5f;

    public int numPillarsSpawned
    {
        get { return _numPillarsSpawned; }
        set { _numPillarsSpawned = value; }
    }

    public float distanceBetweenPillars
    {
        get { return _distanceBetweenPillars; }
        set { _distanceBetweenPillars = value; }
    }

    public float pillarLifetime
    {
        get { return _pillarLifetime; }
        set { _pillarLifetime = value; }
    }

    public override IEnumerator activateAbility()
    {
        Transform player = FindObjectOfType<Player>().transform.parent.transform;
        Vector3 playerPosition = player.position;
        playerPosition.z = 0f;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        int h = -(int)(Mathf.Floor(_numPillarsSpawned / 2f));


        Vector3 v = playerPosition - mousePosition;
        v.z = 0f;

        Vector3 perpendicular = new Vector3(-v.y, v.x, 0f).normalized;
        Vector3 forward = v.normalized;


        for (int i = h; i < _numPillarsSpawned + h; i++)
        {
            Vector3 point = new Vector3(-v.y, v.x, 0f).normalized * i;
            GameObject pillarSpawned = (GameObject)Instantiate(pillar, mousePosition + perpendicular * (i * _distanceBetweenPillars), Quaternion.identity);
            pillarSpawned.transform.localScale = new Vector3(pillarSpawned.transform.localScale.x, 0, pillarSpawned.transform.localScale.z);
            Sequence anim = DOTween.Sequence();
            anim.Insert(0, pillarSpawned.transform.DOScaleY(1, .5f));
            anim.Insert(0, pillarSpawned.transform.DOLocalMoveY(pillarSpawned.transform.position.y + .7f, .5f));
            anim.Insert(0, pillarSpawned.transform.DOScaleY(0, .5f).SetDelay(Random.Range(_pillarLifetime, _pillarLifetime + .3f)));
            anim.AppendCallback(() => Destroy(pillarSpawned));
            yield return null;

        }
    }
}
