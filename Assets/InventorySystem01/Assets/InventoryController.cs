using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject SlotPrefab;
    private const int SLOT = 18;
    public List<Item> acquiredItems = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();
    public int[] itemCount = new int[18];
    
    // Use this for initialization
    void Start()
    {
        Item basicWeapon = new Item();
        basicWeapon.itemName = ItemDB.EquipList[0].itemName;
        basicWeapon.icon = ItemDB.ItemList[ItemDB.EQUIP][0].icon;
        basicWeapon.description = ItemDB.ItemList[ItemDB.EQUIP][0].description;
        basicWeapon.type = ItemDB.ItemList[ItemDB.EQUIP][0].type;
        AddItem(basicWeapon);

        PlaceDummy();

        for (int i = 0; i < 18; i++)
        {
            GameObject newSlot;
            
            string newText = "";
            newSlot = Instantiate(SlotPrefab,this.transform);
            newSlot.name = "Slot " + i;
            Transform newItem = newSlot.transform.Find("ItemPrefab");
            Item newItemS = newItem.GetComponent<Item>();
            Image itemImage = newItem.transform.GetComponent<Image>();
            newItemS.enabled = !newItemS.enabled;
            newItem.name = "Item " + i;
            newSlot.GetComponent<Slot>().slotID = i;
            if (itemCount[i] > 1)
            {
                newText += itemCount[i];
            }
            newSlot.GetComponentInChildren<Text>().text = newText;
            if (i < acquiredItems.Count)
            {
                newItemS.itemEnabled = !newItemS.itemEnabled;
                newItemS.SetItem(acquiredItems[i]);                
                itemImage.sprite = newItemS.icon;
                newSlot.GetComponent<Slot>().item = acquiredItems[i];
                if (i == 0)
                {
                    newItemS.SetSelect();
                }
            }
            slots.Add(newSlot);
        }
    }

    // Update is called once per frame
    void Update()
    {
                
    }
    
    public void AddItem(Item item)
    {
        if (acquiredItems.Count <= SLOT)
        {
            bool isInList = false;

            // check if item is in inventory
            if (acquiredItems.Count != 0)
            {
                for (int i = 0; i < acquiredItems.Count; i++)
                {
                    if (acquiredItems[i].itemName == item.itemName)
                    {
                        isInList = true;
                        itemCount[i]++;
                    }
                }
            }

            // if it's not in the list then add item to the inventory
            if (!isInList)
            {
                acquiredItems.Add(item);
                itemCount[acquiredItems.Count-1]++;
            }
        }
    }

    private void Sort()
    {
        for (int i = 0; i < 18; i++)
        {
            Transform slot;

            string newText = "";
            string slotName = "Slot " + i;
            string itemName = "Item " + i;
            slot = transform.Find(slotName);
            Transform item = slot.transform.Find(itemName);
            Item itemS = item.GetComponent<Item>();
            Image itemImage = item.transform.GetComponent<Image>();
            if (itemCount[i] > 1)
            {
                newText += itemCount[i];
            }
            slot.GetComponentInChildren<Text>().text = newText;
            if (i < acquiredItems.Count)
            {
                itemS.itemEnabled = !itemS.itemEnabled;
                itemS.SetItem(acquiredItems[i]);
                itemImage.sprite = itemS.icon;
                slot.GetComponent<Slot>().item = acquiredItems[i];
                if (i == 0)
                {
                    itemS.SetSelect();
                }
            }
        }
    }

    /**
     * Remove in steps:
     * - trace item in list
     * - trace item in slot
     * - trace the slotID
     * - remove item from slot
     * 
     */
    public void RemoveItemFromList(Item item)
    {
        string itemName = item.itemName;
        int i;
        for(i = 0; i<SLOT; i++)
        {
            string itemslot = "Item " + i;
            if (slots[i].transform.Find(itemslot).GetComponent<Item>().itemName == itemName)
            {
                acquiredItems.Remove(item);
                break;
            }
        }
    }






    public void PlaceDummy()
    {
        Item potion = new Item();
        potion.itemName = ItemDB.ConsumableList[0].itemName;
        potion.icon = ItemDB.ItemList[ItemDB.CONSUMABLES][0].icon;
        potion.description = ItemDB.ItemList[ItemDB.CONSUMABLES][0].description;
        potion.type = ItemDB.ItemList[ItemDB.CONSUMABLES][0].type;
        AddItem(potion);
        AddItem(potion);

        Item spear = new Item();
        spear.itemName = ItemDB.ThrowableList[0].itemName;
        spear.icon = ItemDB.ItemList[ItemDB.THROW][0].icon;
        spear.description = ItemDB.ItemList[ItemDB.THROW][0].description;
        spear.type = ItemDB.ItemList[ItemDB.THROW][0].type;
        AddItem(spear);
        AddItem(spear);
        AddItem(spear);
        AddItem(spear);

        Item dr = new Item();
        dr.itemName = ItemDB.EquipList[1].itemName;
        dr.icon = ItemDB.ItemList[ItemDB.EQUIP][1].icon;
        dr.description = ItemDB.ItemList[ItemDB.EQUIP][1].description;
        dr.type = ItemDB.ItemList[ItemDB.EQUIP][1].type;
        AddItem(dr);
    }
}
