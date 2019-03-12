using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipController : MonoBehaviour {

    public Transform equipedItem;
    public Transform Icon;
    public Transform description;
    public Transform stats;

	// Use this for initialization
	void Start () {
        equipedItem = transform.parent.parent.GetComponent<BigCanvaController>().equipedItem;
        SetEquip(equipedItem);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetEquip(Transform item)
    {
        equipedItem = item;
        Item itemS = item.GetComponent<Item>();
        Icon.GetComponent<Image>().sprite = itemS.icon;
        description.GetComponent<Text>().text = itemS.description;
        stats.GetComponent<Text>().text = itemS.stats;
    }

    //to reset while unequiped
    public void ResetEquip()
    {
        equipedItem = null;
        Icon.GetComponent<Image>().enabled = !Icon.GetComponent<Image>().enabled;
        description.GetComponent<Text>().text = "";
        stats.GetComponent<Text>().text = "";
    }
}
