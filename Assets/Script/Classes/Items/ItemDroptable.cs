using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ItemDroptable : MonoBehaviour
{
    public List<Droptable> drops;
    public float dropRadius = 2f;

    public void GetDrops()
    {
        foreach(Droptable dt in drops)
        {
            int itemsDropped = 0;
            List<Vector2> Ranges = new List<Vector2>();
            if (dt.weighted)
            {
                foreach (PercentItem item in dt.items)
                {
                    if (Ranges.Count == 0)
                    {
                        int x = 0;
                        int y = (int)(item.percent * 10000f);
                        Vector2 temp = new Vector2(x, y);
                        Ranges.Add(temp);
                    }
                    else
                    {
                        int x = (int)(Ranges.Last().y + 1);
                        int y = x + (int)(item.percent * 10000f);
                        Vector2 temp = new Vector2(x, y);
                        Ranges.Add(temp);
                    }
                }

                int draw = UnityEngine.Random.Range(0, 10000);
                Debug.Log(draw);
                int i = 0;
                foreach (Vector2 range in Ranges)
                {
                    if((draw >= range.x) && (draw < range.y))
                    {
                        float deg = UnityEngine.Random.Range(0, 360);
                        string iP = dt.items[i].ItemPath;
                        GameObject spawned = (GameObject)Instantiate(Resources.Load(iP, typeof(GameObject)), new Vector3(transform.position.x + (2 * Mathf.Cos(deg)), transform.position.y + (2 * Mathf.Sin(deg)), 0f), Quaternion.identity);
                    }
                    i++;
                }
            }
            else
            {
                foreach (PercentItem item in dt.items)
                {
                    if (itemsDropped == dt.itemsToDrop)
                    {
                        break;
                    }
                    int numToBeUnder = (int)(item.percent * 10000f);
                    int draw = UnityEngine.Random.Range(0, 10000);
                    float deg = UnityEngine.Random.Range(0, 360);
                    if (draw < numToBeUnder)
                    {
                        GameObject spawned = (GameObject)Instantiate(Resources.Load(item.ItemPath, typeof(GameObject)), new Vector3(transform.position.x + (dropRadius * Mathf.Cos(deg)), transform.position.y + (dropRadius * Mathf.Sin(deg)), 0f), Quaternion.identity);
                        itemsDropped++;
                    }
                }
            }
        }
    }
}

[Serializable]
public class PercentItem
{
    public float percent;
    public string ItemPath;
}

[Serializable]
public class Droptable
{
    public int itemsToDrop;
    public bool weighted;
    public List<PercentItem> items;
}
