using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSwitch : MonoBehaviour
{

    public BackgroundRemovalManager background;
    public GUITexture image;
    public GUITexture real;

    [ContextMenu("Switch")]
    public void Switch()
    {
        background.enabled = !background.enabled;
        real.enabled = !real.enabled;
        image.enabled = !image.enabled;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
