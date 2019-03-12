using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour {

    // item enabled to eventtrigger
    public bool itemEnabled = false;
    // item equiped?
    public bool isEquiped = false;

    public Item item;

    public string itemName;
    public Sprite icon;
    public string description; 
    public enum Type { equip, consumables, throwable};
    public Type type;

    public string stats;

    public double damage;
    public double criticalChance;

    public double addHealth;

    public void SetItem(Item a)
    {
        item = a;
        itemName = item.itemName;
        icon = item.icon;
        description = item.description;
        type = item.type;
        stats = item.stats;
        damage = item.damage;
        criticalChance = item.criticalChance;
        addHealth = item.addHealth;
    }

    public void SetSelect()
    {
        BigCanvaController bigCanva = transform.parent.parent.parent.parent.GetComponent<BigCanvaController>();
        bigCanva.selectedItem = this.transform;
        bigCanva.theItem = item;
    }

    void OnMouseDown()
    {
        if (itemEnabled)
        {
            //get script
            BigCanvaController bigCanva = transform.parent.parent.parent.parent.GetComponent<BigCanvaController>();
            bigCanva.selectedItem = this.transform;
            bigCanva.theItem = item;
        }
    }
    
}
