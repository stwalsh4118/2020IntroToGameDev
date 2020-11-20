using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopSpawnItem : MonoBehaviour
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
        int itemNum = Convert.ToInt32(gameObject.name);
        if (numGold >= ShopManager.Instance.itemsInShop[itemNum].price)
        {
            string itemPath = ShopManager.Instance.itemsInShop[itemNum].itemPath;
            Transform player = GameObject.Find("Character").transform;
            GameObject spawned = (GameObject)Instantiate(Resources.Load(itemPath, typeof(GameObject)), new Vector3(player.position.x, player.position.y, player.position.z), Quaternion.identity);
            numGold -= ShopManager.Instance.itemsInShop[itemNum].price;
        }
        Debug.Log(numGold);
        GameObject.Find("PlayerObject").GetComponent<Player>().localPlayerData.numGold = numGold;
    }


}
