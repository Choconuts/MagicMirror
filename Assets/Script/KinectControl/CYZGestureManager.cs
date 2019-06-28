using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CYZGestureManager : Singleton<CYZGestureManager>
{

    MyGestureListener listener;
    public enum Gesture { Wave, SwipeLeft, SwipeRight, SwipeUp, SwipeDown, Stop, Psi };
    public List<Gesture> gestures = new List<Gesture>() { Gesture.Wave, Gesture.Stop, Gesture.SwipeDown, Gesture.SwipeLeft, Gesture.SwipeRight, Gesture.SwipeUp, Gesture.Psi };
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

    void Set(Gesture gesture)
    {
        if (flags.ContainsKey(gesture)) flags[gesture] = true;
    }

    // Update is called once per frame
    void Update()
    {
        ResetFlags();
        if (listener)
        {
            if (listener.IsWave())
            {
                Set(Gesture.Wave);
            }
            if (listener.IsSwipeLeft())
            {
                Set(Gesture.SwipeLeft);
            }
            if (listener.IsSwipeRight())
            {
                Set(Gesture.SwipeRight);
            }
            if (listener.IsSwipeDown())
            {
                Set(Gesture.SwipeDown);
            }
            if (listener.IsSwipeUp())
            {
                Set(Gesture.SwipeUp);
            }
            if (listener.IsPsi())
            {
                Set(Gesture.Psi);
            }
        }
    }
}
