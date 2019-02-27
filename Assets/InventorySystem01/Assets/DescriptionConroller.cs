using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionConroller : MonoBehaviour {

    public Transform selectedItem;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        selectedItem = transform.parent.GetComponent<BigCanvaController>().selectedItem;
	}

 
}
