using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CYZGestureManager : Singleton<CYZGestureManager>
{

    MyGestureListener listener;
    public enum Gesture { Wave, SwipeLeft, SwipeRight, SwipeUp, SwipeDown, Stop };
    public List<Gesture> gestures = new List<Gesture>() { Gesture.Wave, Gesture.Stop, Gesture.SwipeDown, Gesture.SwipeLeft, Gesture.SwipeRight, Gesture.SwipeUp };
    public Dictionary<Gesture, bool> flags = new Dictionary<Gesture, bool>();

    public void ResetFlags()
    {
        foreach (var ges in gestures)
        {
            if (flags.ContainsKey(ges)) flags[ges] = false;
            else flags.Add(ges, false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        listener = MyGestureListener.Instance;
        ResetFlags();
    }

    // Update is called once per frame
    void Update()
    {
        ResetFlags();
        if (listener)
        {
            if (listener.IsWave())
            {
                flags[Gesture.Wave] = true;
            }
            if (listener.IsSwipeLeft())
            {
                flags[Gesture.SwipeLeft] = true;
            }
            if (listener.IsSwipeRight())
            {
                flags[Gesture.SwipeRight] = true;
            }
            if (listener.IsSwipeDown())
            {
                flags[Gesture.SwipeDown] = true;
            }
            if (listener.IsSwipeUp())
            {
                flags[Gesture.SwipeUp] = true;
            }
        }
    }
}
