using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokedexIcon : MonoBehaviour {

    public Transform icon;
    public BigCanvaController bigCanva;
    
	// Use this for initialization
	void Start () {
        bigCanva = transform.parent.parent.parent.parent.parent.GetComponent<BigCanvaController>();
    }
	
	// Update is called once per frame
	void Update () {
        icon = bigCanva.selectedItem;
        transform.GetComponent<Image>().sprite = icon.GetComponent<Image>().sprite;
	}
}
