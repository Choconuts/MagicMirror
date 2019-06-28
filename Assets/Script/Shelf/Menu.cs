using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Shelf mainMenu;
    public List<Shelf> exclusives;
    public static bool gestureLock;
    public bool showOnUnlock = true;
    public Text lockInfo;
    public static Dictionary<bool, string> infoDict = new Dictionary<bool, string>() {
         { true, "Locked" },
         { false, "To Lock" },
    };

    public void Lock(bool status = true)
    {
        gestureLock = status;
        if (lockInfo)
        {
            lockInfo.text = infoDict[gestureLock];
        }
        if (gestureLock)
        {
            foreach (var shelf in exclusives)
            {
                shelf.Hide();
            }
            mainMenu.Hide();
        }
        else if (showOnUnlock)
        {
            mainMenu.Show();
            mainMenu.GetComponent<KinectSlider>().Froze();
        }
    }

    public void SwitchLock()
    {
        Lock(!gestureLock);
    }

    bool IsIdle()
    {
        if (mainMenu.gameObject.activeSelf) return false;
        foreach(var ex in exclusives)
        {
            if (ex.gameObject.activeSelf) return false;
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(MyGestureListener.Instance != null)
        {
            if (!gestureLock && CYZGestureManager.instance.flags[CYZGestureManager.Gesture.Wave] && IsIdle())
                {
                    mainMenu.Show();
                    mainMenu.GetComponent<KinectSlider>().Froze();
                }
        }
    }
}
