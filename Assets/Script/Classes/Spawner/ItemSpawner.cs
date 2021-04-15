using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : Interactable
{

    public string itemToSpawn;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);

        if (IsWithinRange())
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                string itemPath = "Prefabs/Items/Passive Items/" + itemToSpawn;


                float xPos = radius * Mathf.Cos(Random.Range(0, 360));
                float yPos = radius * Mathf.Sin(Random.Range(0, 360));
                Vector3 pos = new Vector3(transform.position.x + xPos, transform.position.y + yPos, 0f);

                GameObject droppedItem = (GameObject)Instantiate(Resources.Load("Prefabs/Items/DroppedItem"), pos, Quaternion.identity);
                GameObject itemSpawned = (GameObject)Instantiate(Resources.Load(itemPath), droppedItem.transform);

                droppedItem.GetComponent<droppedItem>().item = itemSpawned.GetComponent<Item>();
                droppedItem.GetComponent<droppedItem>().UpdateDroppedItem();
                //Destroy(itemSpawned);
            }
        }
    }
}
