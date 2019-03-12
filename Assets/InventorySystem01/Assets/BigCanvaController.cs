using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCanvaController : MonoBehaviour {

    public Transform selectedItem = null;
    public Transform equipedItem;
    public Item theItem;
    public List<Item> quickSlotItems;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        theItem = selectedItem.GetComponent<Item>();
	}
}
