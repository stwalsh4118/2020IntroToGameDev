using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorter : MonoBehaviour
{
    SpriteRenderer Layer;

    [SerializeField] private int defaultSortingOrder = 100;
    public List<GameObject> behind;

    // Start is called before the first frame update
    void Start()
    {
        Layer = transform.parent.GetComponent<SpriteRenderer>();
        behind = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "WalkBehind")
        {
            if ((other.GetComponent<SpriteRenderer>().sortingOrder - 1) < Layer.sortingOrder)
            {
                Layer.sortingOrder = other.GetComponent<SpriteRenderer>().sortingOrder - 1;
            }
            behind.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        behind.Remove(other.gameObject);
        if (other.CompareTag("WalkBehind"))
        {
            if (other.GetComponent<SpriteRenderer>().sortingOrder == Layer.sortingOrder + 1)
            {
                Layer.sortingOrder++;
            }
            if (behind.Count == 0)
            {
                Layer.sortingOrder = defaultSortingOrder;
            }
        }
    }
}
