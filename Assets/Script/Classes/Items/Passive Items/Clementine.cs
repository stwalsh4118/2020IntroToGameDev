using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clementine : Item
{
    public override void OnPickUp()
    {
        base.OnPickUp();
        HealthBar.playerHP.AddRedHeart();
    }

    public override string ItemName()
    {
        return "Clementine";
    }

    public override string FlavorText()
    {
        return "The only Cutie I see here is you ;)";
    }

    public override string GetDynamicDescriptionText()
    {
        return "Adds " + (Inventory.Instance.inventory.Find(x => x.ItemName() == "Clementine").numberInInventory).ToString() + " hearts.";
    }
}
