using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int numberInInventory = 1;
    public Sprite icon;

    public virtual void OnHitEffect()
    {

    }

    public virtual void OnPickUp()
    {
        OnFirstPickUp();
    }

    public virtual void OnFirstPickUp()
    {
        if (!Inventory.Instance.inventory.Exists(x => x.ItemName() == ItemName()))
        {
            Debug.Log("first pickup");
            numberInInventory = 1;
        }
    }

    public virtual string GetDynamicDescriptionText()
    {
        return "default";
    }

    public virtual string ItemName()
    {
        return "base";
    }

    public virtual string FlavorText()
    {
        return "base";
    }
}
