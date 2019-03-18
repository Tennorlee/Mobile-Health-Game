using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class EquipController : MonoBehaviour {

    public Transform QuickSlots;
    public Transform SlotPrefabs;

    private List<Slot> slots = new List<Slot>();
    public static List<Item> QSList = new List<Item>();
    public Transform equipButton;
    public Transform deleteButton;
    public Item equipedItem;
    public Transform Icon;
    public Transform itemName;
    public Transform description;
    public Transform stats;
    public Sprite img;
    public Item emptyItem;

    // connector
    private string connectionString;
    private string sqlQuery;
    IDbConnection dbConnection;
    IDbCommand dbCommand;

	// Use this for initialization
	void Start () {

        Debug.Log("EC starting up..");
        Debug.Log("Equip Controller DB startup initialized");

	    connectionString = "URI=file:" + Application.dataPath + "/InventorySystem01/Assets/InventoryDatabase.db";

	    Debug.Log("Equip Controller DB startup complete");

        emptyItem = new Item(){
            //name = "item",
            itemID = 0 ,
            itemName = null,
            icon = img,
            description = null,
            damage = 0,
            effectType = 0,
            effectNum = 0,
            isEquiped = false
        };

        Debug.Log("Instantiating quick slots..");

        for(int i = 0; i < 4; i++ ){

            Transform newSlot = Instantiate(SlotPrefabs, QuickSlots);
            newSlot.name = ""+i;
            Item newItem = newSlot.GetComponentInChildren<Item>();
            Sprite itemSprite = newItem.transform.GetComponent<Image>().sprite;
            itemSprite = img;
            newItem.name = "Item";
            slots.Add(newSlot.GetComponent<Slot>());
            QSList.Add(newItem);
            
        }

        Debug.Log("Quick slots generated.");

        ReadQS_DB();
        ResetWeapon(1);

        if (transform.parent.parent.GetComponent<BigCanvaController>().isUnequiped){
            equipedItem = emptyItem;
            Debug.Log("EC reads: null, from Bigcanva: null");
        } else{
            equipedItem = transform.parent.parent.GetComponent<BigCanvaController>().equipedItem;
            Debug.Log("EC reads: "+equipedItem.itemName+" from Bigcanva: "+transform.parent.parent.GetComponent<BigCanvaController>().equipedItem.itemName);
            SetWeapon(equipedItem);
            Debug.Log(equipedItem.itemName+" is set");
            transform.parent.parent.GetComponent<BigCanvaController>().isUnequiped = false;
        }
        
        Debug.Log("EC startup completed.");
    }

    private void ReadQS_DB(){
        for(int i = 0; i<ItemDB.QuickSlotItems.Count; i++){
            for (int j=0; j<4; j++){
                if(QSList[j]==emptyItem){
                    QSList[j]=ItemDB.QuickSlotItems[i];
                }
            }
        }
    }

    public void SetWeapon(Item itemS)
    {
        //equipedItem = item;
        string stat="";
        if ( itemS.itemName!=null ){
            //Item itemS = item.GetComponent<Item>();            
            Icon.GetComponent<Image>().sprite = itemS.icon;
            itemName.GetComponent<Text>().text = itemS.itemName;
            description.GetComponent<Text>().text = itemS.description;
            itemS.isEquiped = true;
            switch(itemS.effectType){

                case 1:                
                    stat += "ALL\n";
                    break;

                case 2:
                    stat += "Grand Mal\n";
                    break;

                default:
                    break;

            }
            stats.GetComponent<Text>().text = stat + itemS.damage;
            UpdateDB(1,itemS.itemID);
            ItemDB.EquipedItem = itemS;
        } else {
            ResetWeapon(0);
        }
    }

    public void SetEquip(Transform item){
        Item itemS = item.GetComponent<Item>();
        for(int i = 0; i< 4; i++ ){
            if(!QSList[i].isEquiped){
                QSList[i]=itemS;
                QSList[i].isEquiped = true;
                UpdateDB( 2, QSList[i].itemID );
                ItemDB.QuickSlotItems.Add(itemS);
            }
        }
    }

    public void Unequip(Transform item){
        Item itemS = item.GetComponent<Item>();
        for(int i = 0; i< 4; i++ ){
            if(QSList[i].itemID == itemS.itemID){
                ItemDB.QuickSlotItems.Remove(itemS);
                itemS.isEquiped = false;
                QSList[i] = emptyItem;
                UpdateDB( 0, itemS.itemID );
            }
        }
    }

    //to reset while unequiped
    public void ResetWeapon(int i)
    {
        equipedItem = null;
        ItemDB.EquipedItem = emptyItem;
        Icon.GetComponent<Image>().sprite = img;
        description.GetComponent<Text>().text = "Equip Something!";
        stats.GetComponent<Text>().text = "";
        itemName.GetComponent<Text>().text = "";
        if(i == 0){
            UpdateDB(0,1); // set to null
        }
    }

    /* while ID = 0,  */
    private void UpdateDB(int ID,int itemID){

        using ( dbConnection = new SqliteConnection(connectionString)){

            Debug.Log("Updating equip DB");

            dbConnection.Open(); // Open connection to the db
            dbCommand = dbConnection.CreateCommand();
            if( ID == 0 && itemID == 1){
                sqlQuery = "UPDATE EquipedItem SET ItemID = 0 WHERE ID = 1";
            }else if( ID==0 ){
                sqlQuery = "UPDATE EquipedItem SET ItemID = 0 WHERE ItemID = "+itemID;
            }else if( ID==1 ){
                sqlQuery = "UPDATE EquipedItem SET ItemID = "+itemID+" WHERE ID = 1";
            }else{
                sqlQuery = "UPDATE EquipedItem SET ItemID = "+itemID+" WHERE ItemID = NULL AND ItemID > 1 ";
            }
            dbCommand.CommandText = sqlQuery;
            dbCommand.ExecuteScalar();
            dbConnection.Close();

        }
    }
}
