using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class spawnShopHealth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnItem()
    {
        int numGold = Convert.ToInt32(GameObject.Find("GoldNumber").GetComponent<TextMeshProUGUI>().text);
        if (numGold >= 50)
        {
            Transform player = GameObject.Find("Character").transform;
            GameObject spawned = (GameObject)Instantiate(Resources.Load("Prefabs/Health/Dropped/DroppedHeart", typeof(GameObject)), new Vector3(player.position.x, player.position.y-2, player.position.z), Quaternion.identity);
            numGold -= 50;
        }
        Debug.Log(numGold);
        GameObject.Find("PlayerObject").GetComponent<Player>().localPlayerData.numGold = numGold;
    }
}
