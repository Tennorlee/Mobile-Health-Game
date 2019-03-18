using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCanvaController : MonoBehaviour {

    public Transform selectedItem = null;
    public Item equipedItem = null;
    public Item prevEquiped = null;
    private Transform prevItem = null;
    public Transform DC;
    private DescriptionController DCs;
    public bool isUnequiped = false;

    void Start(){
        Debug.Log("BigCanva Starting up..");
        DCs = DC.GetComponent<DescriptionController>();
        Debug.Log("BigCanva Startup Completed.");
    }

    void Update(){
        if(selectedItem!=prevItem && selectedItem != null){
            Debug.Log("Change detected in BigCanva!");
            prevItem = selectedItem;
            DCs.SetDescription( selectedItem.GetComponent<Item>() );
            prevItem = selectedItem;
        }
        if(selectedItem == null && prevItem != null){
            Debug.Log("Reseting pokedex.");
            DCs.ResetDescription();
            Debug.Log("Reset completed.");
            prevItem = null;
        }
        if(equipedItem!=prevEquiped && equipedItem != null){
            Debug.Log("Equiped item changed!");
            prevEquiped = equipedItem;
        }
    }

	
}
