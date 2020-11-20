using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBall : Item
{

    public float bounceDamageIncrease = 1000f;

    public override void OnPickUp()
    {
        base.OnPickUp();
        if (!Inventory.Instance.IV.bulletProperties.Exists(x => x == "bounce"))
        {
            Inventory.Instance.IV.bulletProperties.Add("bounce");
            OnHitDamageInstances temp = new OnHitDamageInstances();
            temp.damageSource = "bounce";
            temp.damageType = "multiplicative";
            temp.damageIncrease = bounceDamageIncrease;
            Inventory.Instance.IV.onHitDamageInstances.Add(temp);
        }
    }

    public override string ItemName()
    {
        return "Cheap Rubber Ball";
    }

    public override string FlavorText()
    {
        return "Chewy.";
    }

    public override string GetDynamicDescriptionText()
    {
        return "Your projectiles bounce off of surfaces " + Inventory.Instance.inventory.Find(x => x.ItemName() == "Cheap Rubber Ball").numberInInventory.ToString() + " times. Each bounce increases your projectiles' damage by " + (bounceDamageIncrease - 100).ToString() + "%. (Multiplicative)";
    }
}
