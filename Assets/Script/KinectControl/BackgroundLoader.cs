using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundLoader : MonoBehaviour
{
    public Camera foreground;
    public Camera background;
    public GUITexture image;
    KinectManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = KinectManager.Instance;

        if (manager)
        {
            OverlayController overlay = manager.GetComponent<OverlayController>();
            overlay.foregroundCamera = foreground;
            overlay.backgroundCamera = background;
            overlay.backgroundImage = image;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (manager)
        {
            OverlayController overlay = manager.GetComponent<OverlayController>();
            overlay.foregroundCamera = foreground;
            overlay.backgroundCamera = background;
            overlay.backgroundImage = image;
        }

    }
}
