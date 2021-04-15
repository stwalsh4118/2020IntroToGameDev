using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MarbledRock : Item
{
    public float DamageIncreasePercent = 100;
    
    public override void OnPickUp()
    {
        base.OnPickUp();
        Inventory.Instance.IV.damageIncreaseAdditive += DamageIncreasePercent;
    }

    public override string ItemName()
    {
        return "Marbled Rock";
    }

    public override string FlavorText()
    {
        return "Your projectiles feel a little heavier than usual.";
    }

    public override string GetDynamicDescriptionText()
    {
        return "Adds " + (Inventory.Instance.inventory.Find(x => x.ItemName() == "Marbled Rock").numberInInventory * DamageIncreasePercent).ToString() + "% to your projectiles' damage (Additive)";
    }
}
