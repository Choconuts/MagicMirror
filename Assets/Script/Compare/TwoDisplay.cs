using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDisplay : Singleton<TwoDisplay>
{
    public GameObject leftGroup;
    public GameObject rightGroup;
    public GameObject originGroup;

    public enum Mode { LeftRight, Origin };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeMode(Mode mode)
    {
        if (mode == Mode.LeftRight)
        {
            leftGroup.SetActive(true);
            rightGroup.SetActive(true);
            originGroup.SetActive(false);
        }
        if (mode == Mode.Origin)
        {
            leftGroup.SetActive(false);
            rightGroup.SetActive(false);
            originGroup.SetActive(true);
        }
    }

    [ContextMenu("Switch")]
    public void SwitchCompare()
    {
        if (originGroup.activeSelf) ChangeMode(Mode.LeftRight);
        else ChangeMode(Mode.Origin);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
