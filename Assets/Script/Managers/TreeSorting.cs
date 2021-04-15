using MapGeneration.Core.ChainDecompositions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeSorting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> toSort = new List<GameObject>();

        foreach(Transform child in transform)
        {
            toSort.Add(child.gameObject);
        }

        int order = 100;
        foreach (var go in toSort.OrderByDescending(go => go.transform.position.y))
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = order;
            order++;
        }
    }
}
