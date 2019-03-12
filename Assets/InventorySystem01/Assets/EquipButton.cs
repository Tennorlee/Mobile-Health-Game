using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipButton : MonoBehaviour {

    public BigCanvaController bigCanva;
    public Transform buttonText;
    public Transform equipController;

	// Use this for initialization
	void Start () {
        bigCanva = transform.parent.parent.parent.parent.GetComponent<BigCanvaController>();
        buttonText = transform.Find("Text");
	}
	
	// Update is called once per frame
    // Set button text
	void Update () {
		if (bigCanva.selectedItem.GetComponent<Item>().isEquiped)
        {
            buttonText.GetComponent<Text>().text = "UNEQUIP";
        }
        else
        {
            buttonText.GetComponent<Text>().text = "EQUIP";
        }
	}

    /**
     * When Clicked,
     * 1. if it's equiped:
     *      a. remove item from big canva to null
     *      b. if item.type == equip
     *             i) reset equipment to null
     *         else
     *             i) remove from quick slot
     *      b. item.isEquiped = false
     * 2. if it's not equiped:
     *      a. replace item of equiped item in big canva
     *      b. set equipController item = 
     *      c. item.isEquiped = true
     */
    private void OnMouseDown()
    {
        Item selItemS = bigCanva.selectedItem.GetComponent<Item>();
        if (selItemS.isEquiped) // UNEQUIP
        {
            EquipEvent(selItemS);
        }
        else // Equip
        {
            selItemS.isEquiped = !selItemS.isEquiped;
            bigCanva.equipedItem = bigCanva.selectedItem;
            equipController.GetComponent<EquipController>().SetEquip(bigCanva.selectedItem);
        }
    }

    // for global use
    public void EquipEvent(Item selItemS)
    {
        selItemS.isEquiped = !selItemS.isEquiped;
        if (selItemS.type == Item.Type.equip)
        {
            equipController.GetComponent<EquipController>().ResetEquip();
            bigCanva.equipedItem = null; // reset equiped item
        }
        else
        {
            // remove from slot
        }
    }
}
