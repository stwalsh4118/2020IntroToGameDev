using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorter : MonoBehaviour
{
    SpriteRenderer Layer;

    [SerializeField] private int defaultSortingOrder = 100;

    // Start is called before the first frame update
    void Start()
    {
        Layer = transform.parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("wtf");
        if (other.tag == "WalkBehind")
        {
            Layer.sortingOrder = other.GetComponent<SpriteRenderer>().sortingOrder - 1;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "WalkBehind")
        {
            Layer.sortingOrder = defaultSortingOrder;
        }
    }
}
