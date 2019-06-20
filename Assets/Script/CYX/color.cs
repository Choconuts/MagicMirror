using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class color : MonoBehaviour {
    public static int colornum;
    public Magnetic other;

    // Use this for initialization
    void Start () {
        GameObject.DontDestroyOnLoad(gameObject);
        colornum = other.highlight;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
