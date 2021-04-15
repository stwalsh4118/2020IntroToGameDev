using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleRocket : Item
{
    public float bulletAccelerationIncrease = 4f;

    public override void OnPickUp()
    {
        base.OnPickUp();
        Inventory.Instance.IV.bulletAccelerationIncrease += bulletAccelerationIncrease;
    }

    public override string ItemName()
    {
        return "Bottle Rocket";
    }

    public override string FlavorText()
    {
        return "It's literally rocket science.";
    }

    public override string GetDynamicDescriptionText()
    {
        float a = (Inventory.Instance.inventory.Find(x => x.ItemName() == "Bottle Rocket").numberInInventory * bulletAccelerationIncrease);
        return "Halves your projectiles' speed but accelerates your projectiles by " + a.ToString() + " tiles per second. Increases your projectiles damage by " + Inventory.Instance.inventory.Find(x => x.ItemName() == "Bottle Rocket").numberInInventory * 25 + "% per second. (Additive)";
    }
}
