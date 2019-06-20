using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseTry : MonoBehaviour {
    public Magnetic other;
    public int num;
    public Button R;
    public GameObject obj;

    // Use this for initialization
    void Start () {
        R.onClick.AddListener(Re);
        obj.SetActive(false);
    }

    void Re()
    {
        num = other.highlight;
        obj.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
        
    }
}
