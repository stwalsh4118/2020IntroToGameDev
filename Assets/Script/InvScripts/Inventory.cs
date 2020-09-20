using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {


	public delegate void OnItemChange ();
	public OnItemChange onItemChangeCallback;
	public List<Item> inventory = new List<Item> (); // List of Items. Are ITEMS.
	private List<string> itemList; // List of the names of inventory's items. ARE STRINGS.
	public GameObject inventoryUI;
	[Tooltip("How many items can this inventory hold?")]
	public int inventorySpace = 20;
	public static Inventory instance;

	private Text layoutUI; // The layoutUI that's shown in Inventory UI. (Layout is text; InventoryUI is gameObject).

	#region Singleton
	void Awake (){

		if (instance != null){
			Debug.LogWarning("More than one instance of inventory found!");
			return;
		}

		instance = this;
	}
	#endregion

	void Start () {

		layoutUI = inventoryUI.GetComponent<Text> ();
		itemList = new List<string> ();
	}
	public  void UpdateInventoryUI (string itemName) {
		foreach (Item item in inventory) {
			layoutUI.text += itemName + "\n";
		}

	}
	public bool AddToInventory (Item item) {

		if (!item.isDefault){
		
			if (inventory.Count >= 20){
				Debug.Log("Not enough space in inventory");
				return false;
			}
			inventory.Add (item);
			UpdateInventoryUI (item.name);
		}
		if (onItemChangeCallback != null){
			onItemChangeCallback.Invoke();
		}

		return true;

	}

	public void RemoveFromInventory (Item item){
		inventory.Remove(item);
	}

	public void ShowInventory () {
		foreach (string name in ItemList) {
			print (name);
		}
	}

	public bool IsInInventory (string name) {
		if (ItemList.Contains(name)) {
			return true;
		}
		return false;
	}

	public List<string> ItemList {
		get {
			foreach (Item x in inventory) {
				itemList.Add (x.name);
			}
			return itemList;
		}
	}

}
