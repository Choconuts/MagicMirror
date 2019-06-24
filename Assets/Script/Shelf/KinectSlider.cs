using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KinectSlider : MonoBehaviour
{
    public static List<KinectSlider> activeInstances = new List<KinectSlider>();
    public Magnetic magnetic;
    public int layer;
    float cooldown = 1f;
    public enum Function { None, Hide, ChooseCloth, ChooseModel, Custom };
    public Function function = Function.None;
    public event Action<int> OnComfirm;

    public void Froze()
    {
        lastSlideTime = Time.time;
    }

    float lastSlideTime;

    bool LayerJudge()
    {
        foreach (var md in activeInstances)
        {
            if (md.layer > layer) return false;
        }
        return true;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        activeInstances.Add(this);
        lastSlideTime = -100;
    }

    private void OnDisable()
    {
        activeInstances.Remove(this);
    }

    private bool CoolDown()
    {
        return Time.time - lastSlideTime >= cooldown;
    }

    // Start is called before the first frame update
    void Start()
    {
        magnetic = GetComponentInChildren<Magnetic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MyGestureListener.Instance != null)
        {
            if (!Menu.gestureLock && MyGestureListener.Instance.IsSwipeLeft() && CoolDown())
            {
                magnetic.Slide(true);
                lastSlideTime = Time.time;
            }

            if (!Menu.gestureLock && MyGestureListener.Instance.IsSwipeRight() && CoolDown())
            {
                magnetic.Slide(false);
                lastSlideTime = Time.time;
            }

            if (!Menu.gestureLock && MyGestureListener.Instance.IsWave() && CoolDown())
            {
                switch(function)
                {
                    case Function.Hide:
                        GetComponent<Shelf>().Hide();
                        break;
                }
            }
        } 
    }
}
