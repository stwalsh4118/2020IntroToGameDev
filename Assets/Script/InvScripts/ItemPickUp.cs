  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable {
	
	public Item item;

	public override void Interact(){
		base.Interact();
		PickUp ();
	}

	public void PickUp (){
    Inventory.instance.AddToInventory(item);
		//AreaDetector.flareFound = true;
		Destroy(gameObject);
	}

}
