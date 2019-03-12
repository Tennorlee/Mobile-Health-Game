using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : MonoBehaviour {

    public BigCanvaController bigCanva;

    public Transform inventory;
    public InventoryController inventoryS;

    public Transform equipButton;
    public EquipButton equipButtonS;

    // Use this for initialization
    void Start()
    {
        bigCanva = transform.parent.parent.parent.parent.GetComponent<BigCanvaController>();
        equipButtonS = equipButton.GetComponent<EquipButton>();
    }

    /**
     * On delete item:
     * 1. remove item from acquired list
     * 2. remove selected item of big canva
     * 3. remove shown item on the pokedex 
     * 4. if it's equiped, reset the equipment
     */
    private void OnMouseDown()
    {
        Item selItem = bigCanva.selectedItem.GetComponent<Item>();

        if (selItem.itemName != "The Black Sword")
        {
            if (selItem.isEquiped)
            {
                equipButtonS.EquipEvent(selItem);
            }

            RemoveFromList();
        }
    }

    public void RemoveFromList()
    {
        inventory.GetComponent<InventoryController>().
    }
}
