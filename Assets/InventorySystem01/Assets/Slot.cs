using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {

    public int slotID;
    public Item item;
    //private string childname;

	// Use this for initialization
	void Start () {

        if(slotID == 1)
        {
            transform.parent.parent.parent.GetComponent<BigCanvaController>().selectedItem = transform.Find("Item 0");
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        //item = transform.Find(childname).GetComponent<Item>().item;
	}

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public Item GetItem()
    {
        return item;
    }

    public string getItemName()
    {
        return item.itemName;
    }

    public string GetItemDescription()
    {
        return item.description;
    }
    
    public void SetID(int id)
    {
        slotID = id;
    }
}
