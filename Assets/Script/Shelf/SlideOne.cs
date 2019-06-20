using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideOne : MonoBehaviour
{
    public Magnetic shelfList;

    // Start is called before the first frame update
    void Start()
    {
        //可以直接在inspector做这些设置，第一个bool决定了是否用之前的拖拽版本
        shelfList.doNotAutoHighlight = true;
        shelfList.delay = 0.3f;
        shelfList.slideRefreshTime = 0.4f;
        shelfList.animateTime = 0.6f;
    }

    [ContextMenu("SlideNext")]
    void SlideRight()
    {
        // 下面这一步是用来做到头拉不动的动画的
        shelfList.BeginDrag();
        shelfList.Slide(-40);
        shelfList.EndDrag();
        shelfList.Slide(true);
    }

    [ContextMenu("SlideLast")]
    void SlideLeft()
    {
        shelfList.BeginDrag();
        shelfList.Slide(40);
        shelfList.EndDrag();
        shelfList.Slide(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
