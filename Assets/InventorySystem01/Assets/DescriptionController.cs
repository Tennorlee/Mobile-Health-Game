using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionController : MonoBehaviour {

    public Transform selectedItem;
    public BigCanvaController bigCanva;
    public Transform descriptionPanel;
    public Item chosenItem;
    public Transform textPanel;
    private Transform itemName;
    private Transform type;
    private Transform description;
    private Transform statsL;
    private Transform statsR;

    // Use this for initialization
    void Start () {
        bigCanva = transform.parent.GetComponent<BigCanvaController>();
        itemName = textPanel.Find("ItemName");
        type = textPanel.Find("ItemType");
        description = descriptionPanel.Find("Text");
        statsL = descriptionPanel.Find("stats").Find("left");
        statsR = descriptionPanel.Find("stats").Find("right");
    }

    // Update is called once per frame
    void Update()
    {
        selectedItem = bigCanva.selectedItem;
        chosenItem = bigCanva.theItem;
        itemName.GetComponent<Text>().text = chosenItem.itemName;
        if (chosenItem.type == Item.Type.equip)
        {
            type.GetComponent<Text>().text = "Equipable";
            type.GetComponent<Text>().color = Color.magenta;
            statsL.GetComponent<Text>().text = "Stats:\nDamage:\nCritical Chance:";
            statsR.GetComponent<Text>().text = chosenItem.stats;
        }
        if (chosenItem.type == Item.Type.consumables)
        {
            type.GetComponent<Text>().text = "Consumable";
            type.GetComponent<Text>().color = Color.green;
            statsL.GetComponent<Text>().text = "Healing Amount:";
            statsR.GetComponent<Text>().text = chosenItem.stats;
        }
        if (chosenItem.type == Item.Type.throwable)
        {
            type.GetComponent<Text>().text = "Throwable";
            type.GetComponent<Text>().color = Color.red;
        }
        description.GetComponent<Text>().text = chosenItem.description;
    }

 
}
