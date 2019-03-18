using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class InventoryController : MonoBehaviour
{
    public GameObject SlotPrefab;
    private const int SLOT = 18;
    public static List<Item> acquiredItems = ItemDB.AcquiredItems;
    public List<GameObject> slots = new List<GameObject>();
    public int[] itemCount = ItemDB.itemCount;
    public Sprite transparentImg;

    // connector
    private string connectionString;
    private string sqlQuery;
    IDbConnection dbConnection;
    IDbCommand dbCommand;
    
    // Use this for initialization
    void Start()
    {

        Debug.Log("Inventory Controller DB connection startup initialized");
	    connectionString = "URI=file:" + Application.dataPath + "/InventorySystem01/Assets/InventoryDatabase.db";
	    Debug.Log("Inventory Controller DB connection startup complete");

        for (int i = 0; i < 18; i++)
        {
            GameObject newSlot;
            
            string newText = "";
            newSlot = Instantiate(SlotPrefab,this.transform);
            newSlot.name = "Slot " + i;
            Transform newItem = newSlot.transform.Find("ItemPrefab");
            Item newItemS = newItem.GetComponent<Item>();
            Image itemImage = newItemS.transform.GetComponent<Image>();
            newItemS.enabled = !newItemS.enabled;
            newItem.name = "Item " + i;
            newSlot.GetComponent<Slot>().slotID = i;
            if (itemCount[i] > 1)
            {
                newText += itemCount[i];
            }
            Debug.Log("Slot "+i);
            newSlot.GetComponentInChildren<Text>().text = newText;
            if (i < acquiredItems.Count)
            {
                
                Debug.Log("Setting item: "+acquiredItems[i].itemName);
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
        Debug.Log("IC initialization completed");
    }

    // private void ReadAcquiredItemFromDB(){
    //     //Debug.Log();
    //     Debug.Log("IC: Read function called.");
    //     int i = 0;
    //     acquiredItems.Clear();
    //     Debug.Log("IC: Acquired item list cleared");
    //     using (dbConnection = new SqliteConnection(connectionString)){
    //         dbConnection.Open();
    //         Debug.Log("IC: connection opened.");
    //         dbCommand = dbConnection.CreateCommand();
    //         sqlQuery = "SELECT * FROM AcquiredItem LIMIT 18";
    //         dbCommand.CommandText = sqlQuery;
    //         IDataReader reader = dbCommand.ExecuteReader();
    //         while (reader.Read()){
    //             int x = reader.GetInt32(0);
    //             Debug.Log("IC: ItemID "+x+" loaded.");
    //             Item item = IDtoITEM(x);
    //             Debug.Log("IC: Item name: "+item.itemName);
    //             acquiredItems.Add( item );
    //             Debug.Log("IC: "+acquiredItems[acquiredItems.Count-1].itemName+" added to the list");
    //             itemCount[i]=reader.GetInt32(1);
    //             i++;
    //         }
    //         reader.Close();
    //         reader = null;
    //         dbCommand.Dispose();
    //         dbCommand = null;
    //         dbConnection.Close();
    //         dbConnection = null;
    //     }
    // }

    // /**
    //     translator
    // */
    // private Item IDtoITEM(int id){
    //     Item item = null;

    //     // get max length
    //     int MAX = ItemDB.ConsumableList.Count;
    //     if(MAX<ItemDB.EquipList.Count){
    //         if(ItemDB.EquipList.Count<ItemDB.ThrowableList.Count){
    //             MAX = ItemDB.ThrowableList.Count;
    //         } else {
    //             MAX = ItemDB.EquipList.Count;
    //         }
    //     } else if(MAX<ItemDB.ThrowableList.Count){
    //         MAX = ItemDB.ThrowableList.Count;
    //     }

    //     for(int i=0;i<MAX;i++){
    //         if(i<ItemDB.EquipList.Count && ItemDB.EquipList[i].itemID == id){
    //             item = ItemDB.EquipList[i];
    //         }
    //         if(i<ItemDB.ThrowableList.Count && ItemDB.ThrowableList[i].itemID == id){
    //             item = ItemDB.ThrowableList[i];
    //         }
    //         if(i<ItemDB.ConsumableList.Count && ItemDB.ConsumableList[i].itemID == id){
    //             item = ItemDB.ConsumableList[i];
    //         }
    //     }
    //     return item;

    // }
    
    // public void AddItem(Item item)
    // {
    //     if (acquiredItems.Count <= SLOT)
    //     {
    //         bool isInList = false;

    //         // check if item is in inventory
    //         if (acquiredItems.Count != 0)
    //         {
    //             for (int i = 0; i < acquiredItems.Count; i++)
    //             {
    //                 if (acquiredItems[i].itemName == item.itemName)
    //                 {
    //                     isInList = true;
    //                     itemCount[i]++;
    //                 }
    //             }
    //         }

    //         // if it's not in the list then add item to the inventory
    //         if (!isInList)
    //         {
    //             acquiredItems.Add(item);
    //             itemCount[acquiredItems.Count-1]++;
    //         }
    //     }
    // }

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
                itemS.itemEnabled = true;
                itemS.SetItem(acquiredItems[i]);
                itemImage.sprite = itemS.icon;
                slot.GetComponent<Slot>().item = acquiredItems[i];
                if (i == 0)
                {
                    itemS.SetSelect();
                }
            } else {
                itemS.itemEnabled = false;
                itemS.SetItem(null);
                itemImage.sprite = transparentImg;
                slot.GetComponent<Slot>().item = null;
            }
        }
    }

    private void SortItemCount(int offset){

        while(offset<SLOT-1){
            itemCount[offset] = itemCount[offset+1];
            offset++;
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
                if(i!=17){
                    SortItemCount(i);
                }
                Sort();
                DeleteAcquiredItem(item.itemID);
                break;
            }
        }
    }
    
    public void DeleteAcquiredItem(int id){
        // abc
        using (dbConnection = new SqliteConnection(connectionString)){
            dbConnection.Open(); // Open connection to the db
            dbCommand = dbConnection.CreateCommand();
            sqlQuery = " DELETE FROM EquipedItem WHERE ItemID = "+id;
            dbCommand.CommandText = sqlQuery;
            dbCommand.ExecuteScalar();
            dbConnection.Close();
        }
    }

}
