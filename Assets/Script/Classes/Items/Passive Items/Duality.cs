using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duality : Item
{
    
    public float AttackSpeedIncreasePercent = .05f;
    public float DamageIncreasePercent = 20;

    public override void OnPickUp()
    {
        base.OnPickUp();
        Inventory.Instance.IV.fireRateIncrease += AttackSpeedIncreasePercent;
        Inventory.Instance.IV.damageIncreaseAdditive += DamageIncreasePercent;
    }

    public override string ItemName()
    {
        return "Duality";
    }

    public override string FlavorText()
    {
        return "This, as I take it, was because all human beings, as we meet them, are commingled out of good and evil.";
    }

    public override string GetDynamicDescriptionText()
    {
        int numInInv = Inventory.Instance.inventory.Find(x => x.ItemName() == "Duality").numberInInventory;
        return "Adds " + (numInInv* (AttackSpeedIncreasePercent * 100)).ToString() + "% to your attack speed (Multiplicative) and adds " + (numInInv * DamageIncreasePercent).ToString() + "% to your projectiles' damage (Additive)";
    }
}
