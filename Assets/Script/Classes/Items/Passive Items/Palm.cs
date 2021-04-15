using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palm : Item
{
    public override void OnPickUp()
    {
        base.OnPickUp();
        Inventory.Instance.IV.numExtraBullets++;
    }

    public override string ItemName()
    {
        return "Palm";
    }

    public override string FlavorText()
    {
        return "The sky is falling!";
    }

    public override string GetDynamicDescriptionText()
    {
        return "Adds " + (Inventory.Instance.inventory.Find(x => x.ItemName() == "Palm").numberInInventory).ToString() + " projectiles to your attack.";
    }
}
