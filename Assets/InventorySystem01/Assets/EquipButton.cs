using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipButton : MonoBehaviour {

    public Transform bigCanva;
    public BigCanvaController bigCanvaS;
    public Transform buttonText;
    public Transform equipController;
    public List<Item> QSList = EquipController.QSList;

	// Use this for initialization
	void Start () {
        bigCanvaS = bigCanva.GetComponent<BigCanvaController>();
	}
	
	// Update is called once per frame
    // Set button text
	void Update () {
        if (bigCanvaS.selectedItem!=null){
		    if (bigCanvaS.selectedItem.GetComponent<Item>().isEquiped)
            {
                buttonText.GetComponent<Text>().text = "UNEQUIP";
            }
            else
            {
                buttonText.GetComponent<Text>().text = "EQUIP";
            }
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
        Item selItemS = bigCanvaS.selectedItem.GetComponent<Item>();
        if (selItemS.isEquiped) // UNEQUIP
        {
            EquipEvent(selItemS);
            bigCanvaS.isUnequiped = true;
        }
        else // Equip
        {
            selItemS.isEquiped = !selItemS.isEquiped;
            if (selItemS.type == Item.Type.equip)
            {
                if(bigCanvaS.equipedItem != null){
                    bigCanvaS.equipedItem.isEquiped = false; // unequip the previous item.
                }
                equipController.GetComponent<EquipController>().SetWeapon(bigCanvaS.selectedItem.GetComponent<Item>());
            }
            else
            {
                if(QSList.Count <= 4){
                    int j =0;
                    bool isListed =false;
                    while(j < QSList.Count){
                        if(QSList[j].itemName == selItemS.itemName){
                            isListed = true;
                        }
                    }
                    if(!isListed){
                        equipController.GetComponent<EquipController>().SetEquip(bigCanvaS.selectedItem);
                    }
                }
            }
            bigCanvaS.equipedItem = bigCanvaS.selectedItem.GetComponent<Item>();
            
        }
    }

    // for global use
    public void EquipEvent(Item selItemS)
    {
        selItemS.isEquiped = !selItemS.isEquiped;
        if (selItemS.type == Item.Type.equip)
        {
            equipController.GetComponent<EquipController>().ResetWeapon(0);
            bigCanvaS.equipedItem = null; // reset equiped item
        }
        else
        {
            equipController.GetComponent<EquipController>().Unequip(bigCanvaS.selectedItem);
            bigCanvaS.equipedItem = null;
        }
    }
}
