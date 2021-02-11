using System.Collections;
using System.Collections.Generic;
using NaughtyBiker.InfoObjects;
using UnityEngine;

public class foo : MonoBehaviour {
	// Start is called before the first frame update
	void Start() {
        Debug.Log(gameObject.GetComponent<InfoObject>().GetInfo());
	}
}
