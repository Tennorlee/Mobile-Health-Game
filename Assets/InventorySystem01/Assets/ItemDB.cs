using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour {

    public static List<List<Item>> ItemList = new List<List<Item>>();
    public static List<Item> ConsumableList = new List<Item>(); // 0
    public static List<Item> EquipList = new List<Item>(); // 1
    public static List<Item> ThrowableList = new List<Item>(); // 2
    public Sprite[] sprites;

    public static int CONSUMABLES = 0;
    public static int EQUIP = 1;
    public static int THROW = 2;

    // Use this for initialization
    void Start () {

        ItemList.Add(ConsumableList);
        ItemList.Add(EquipList);
        ItemList.Add(ThrowableList);

        //ITEM CREATION
        Item i0 = new Item
        {
            itemName = "The Black Sword",
            type = Item.Type.equip,
            icon = sprites[0],
            description = "Black. Basic. Boring.",
            damage = 10,
            criticalChance = 0.1,
            stats = "S\n10\n0.1"
        };
        ItemList[EQUIP].Add(i0);

        //ITEM CREATION
        Item i1 = new Item
        {
            itemName = "Healing Potion",
            type = Item.Type.consumables,
            icon = sprites[1],
            description = "It's green in color and it looked tasty.",
            addHealth = 10,
            stats = "10"
        };
        ItemList[CONSUMABLES].Add(i1);

        //ITEM CREATION
        Item i2 = new Item
        {
            itemName = "Fish Spear",
            type = Item.Type.throwable,
            icon = sprites[2],
            description = "Who says only fish are being speared?"
        };
        ItemList[THROW].Add(i2);

        //ITEM CREATION
        Item i3 = new Item
        {
            itemName = "Divine Raiper",
            type = Item.Type.equip,
            icon = sprites[3],
            description = "The legendary sword which only the worthy holds."
        };
        ItemList[EQUIP].Add(i3);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
