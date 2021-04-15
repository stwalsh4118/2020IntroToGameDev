using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance = null;
    public List<ShopItem> shopItemsPool;
    public List<ShopItem> itemsInShop;
    public List<int> selectedItems;
    public List<Image> itemImages;
    public List<TextMeshProUGUI> prices;

    private void Awake()
    {
        // If there is not already an instance of StateManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
        // Start is called before the first frame update
        void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StateManager.Instance.inMenu = false;
            transform.gameObject.SetActive(false);
        }
    }
    void OnEnable()
    {
        itemsInShop.Clear();
        selectedItems.Clear();
        for (int i = 0; i < 3; i++)
        {
            int selectedItem = UnityEngine.Random.Range(0, shopItemsPool.Count);
            if(selectedItems.Exists(x => x == selectedItem))
            {
                i--;
            }
            else
            {
                selectedItems.Add(selectedItem);
                itemsInShop.Add(shopItemsPool[selectedItem]);
            }
        }

        for(int i = 0; i < 3; i++)
        {
            itemImages[i].sprite = itemsInShop[i].sprite;
            prices[i].text = itemsInShop[i].price.ToString();
        }
    }

    [Serializable]
    public class ShopItem
    {
        public Sprite sprite;
        public string itemPath;
        public int price;
        public string name;
    }
}
