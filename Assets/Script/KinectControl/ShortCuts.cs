using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCuts : MonoBehaviour
{
    public Shelf shelf;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CYZGestureManager.instance.flags[CYZGestureManager.Gesture.Psi])
        {
            shelf.Show();
        }
    }
}
