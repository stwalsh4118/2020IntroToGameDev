using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemOnEnterRoom : MonoBehaviour
{
    [SerializeField] private bool _hasSpawned = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Character" && !_hasSpawned)
        {
            GetComponent<ItemDroptable>().dropRadius = 5f;
            GetComponent<ItemDroptable>().GetDrops();
            _hasSpawned = true;
        }
    }
}
