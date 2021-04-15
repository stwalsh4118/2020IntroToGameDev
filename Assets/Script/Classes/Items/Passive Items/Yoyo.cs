using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yoyo : Item
{

    public float AttackSpeedIncreasePercent = .1f;

    public override void OnPickUp()
    {
        base.OnPickUp();
        Inventory.Instance.IV.fireRateIncrease += AttackSpeedIncreasePercent;
    }

    public override string ItemName()
    {
        return "Yoyo";
    }

    public override string FlavorText()
    {
        return "Walking the Dogs.";
    }

    public override string GetDynamicDescriptionText()
    {
        return "Adds " + (Inventory.Instance.inventory.Find(x => x.ItemName() == "Yoyo").numberInInventory * (AttackSpeedIncreasePercent * 100)).ToString() + "% to your attack speed. (Multiplicative)";
    }
}
