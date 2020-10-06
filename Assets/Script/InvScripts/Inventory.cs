using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{


    public List<Item> inventory = new List<Item>();
    public static Inventory Instance;

    public ItemValues IV = new ItemValues();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {

    }
	
    public void AddToInventory(Item item)
    {
        if (Instance.inventory.Exists(x => x.ItemName() == item.ItemName()))
        {
            Instance.inventory.Find(x => x.ItemName() == item.ItemName()).numberInInventory++;
        }
        else
        {
            inventory.Add(item);
        }
		item.OnPickUp();

    }

    public void RemoveFromInventory(Item item)
    {
        inventory.Remove(item);
    }

}

[Serializable]

public class ItemValues
{
    public List<string> bulletProperties;
    public float damageIncreaseAdditive;
    public float fireRateIncrease;
    public float bulletAccelerationIncrease;
    public List<float> damageIncreaseMultiplicative;
    public List<OnHitDamageInstances> onHitDamageInstances;
}

[Serializable]
public class OnHitDamageInstances
{
    public string damageSource;
    public string damageType;
    public float damageIncrease;
}
