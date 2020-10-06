using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    GameObject InventoryUI;
    [SerializeField] GameObject ItemHolder;
    void Start()
    {
        InventoryUI = GameObject.Find("InventoryUI");
        InventoryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            StateManager.Instance.inMenu = !StateManager.Instance.inMenu;
            foreach(Transform child in InventoryUI.transform.Find("Panel/Scroll View/Viewport/Content").transform)
            {
                Destroy(child.gameObject);
            }
            foreach(Item item in Inventory.Instance.inventory)
            {
                GameObject createdItemHolder = (GameObject)Instantiate(ItemHolder, InventoryUI.GetComponentInChildren<GridLayoutGroup>().transform);
                createdItemHolder.GetComponent<ItemHolder>().item = item;
                createdItemHolder.transform.Find("Image").GetComponent<Image>().sprite = item.icon; 
            }
            if (InventoryUI.activeInHierarchy)
            {
                ResetItemDescriptionPanel();
            }
            InventoryUI.SetActive(!InventoryUI.activeInHierarchy);

        }
    }

    public void ResetItemDescriptionPanel()
    {
        GameObject ID = GameObject.Find("ItemDescription");
        ID.transform.Find("ItemName").gameObject.GetComponent<TextMeshProUGUI>().text = "";
        ID.transform.Find("FlavorText").gameObject.GetComponent<TextMeshProUGUI>().text = "";
        ID.transform.Find("DescriptionText").gameObject.GetComponent<TextMeshProUGUI>().text = "";
        ID.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite = null;
        ID.transform.Find("ItemImage").gameObject.GetComponent<Image>().color = new Color32(0xFF, 0xFF, 0xFF, 0x00);
    }
}
