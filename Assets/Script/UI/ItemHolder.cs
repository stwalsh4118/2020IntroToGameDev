using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour
{
    public Item item;

    public void SetItemDescriptionPanel()
    {
        GameObject ID = GameObject.Find("ItemDescription");
        ID.transform.Find("ItemName").gameObject.GetComponent<TextMeshProUGUI>().text = item.ItemName();
        ID.transform.Find("FlavorText").gameObject.GetComponent<TextMeshProUGUI>().text = item.FlavorText();
        ID.transform.Find("DescriptionText").gameObject.GetComponent<TextMeshProUGUI>().text = item.GetDynamicDescriptionText();
        ID.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite = item.icon;
        ID.transform.Find("ItemImage").gameObject.GetComponent<Image>().color = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
    }
}
