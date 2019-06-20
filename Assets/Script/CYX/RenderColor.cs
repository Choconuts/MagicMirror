using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderColor : MonoBehaviour {
    public Material[] material;
    Renderer rend;
    //public ChooseTry other;
    public Magnetic other;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[other.highlight];
    }
	
	// Update is called once per frame
	void Update () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[other.highlight];
    }
}
