using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour {

    public string name { get; set; }
    public Sprite icon { get; set; }
    public string description { get; set; }
    public enum Type { equip, consumables, throwable};
    public Type type;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        transform.parent.parent.parent.parent.GetComponent<BigCanvaController>().selectedItem = this.transform;
    }
    
}
