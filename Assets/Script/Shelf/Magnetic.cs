using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Magnetic : MonoBehaviour
{
    public float gridWidth;
    public List<GameObject> items;
    public int highlight = 0;
    public float originalOffset;
    public int ItemNum => transform.childCount;
    public float delay = 0.5f;
    public float animateTime = 0.5f;
    public float slideRefreshTime = 0.2f;
    public float reshapeThreshold;
    public float slideNextThreshold = 10f;
    public bool doNotAutoHighlight;
    bool updateOnce;
    
    public bool Active { get { return active; } set { lastTick = Time.time; active = value; } }
    bool active = true;
    RectTransform rect;
    Tweener tweener;

    float m_offset;
    public void BeginDrag()
    {
        Active = false;
        tweener.Kill();
        m_offset = rect.offsetMin.x;
    }

    public void Slide(float x)
    {
        if (Active) Debug.Log("Please call Begin Drag First!");
        //rect.offsetMin = new Vector2(m_offset + x, rect.offsetMin.y);
        DOTween.To(() => rect.offsetMin, (v) => rect.offsetMin = v, new Vector2(m_offset + x, rect.offsetMin.y), slideRefreshTime).SetEase(Ease.InOutSine);
        KeepZeroArea();
    }

    public void EndDrag()
    {
        Active = true;
    }

    public void RecalculateStride()
    {
        var vg = GetComponent<HorizontalLayoutGroup>();
        if (!vg) return;
        float spacing = vg.spacing;
        if (highlight < 0 || highlight >= ItemNum) highlight = 0;
        float width = Child(highlight).rect.width;
        gridWidth = width + spacing;
    }

    [ContextMenu("Switch")]
    void Switch()
    {
        Active = !Active;
    }

    void KeepZeroArea()
    {
        rect.offsetMax = new Vector2(rect.offsetMin.x, rect.offsetMax.y);
    }

    RectTransform Child(int i)
    {
        return transform.GetChild(i).gameObject.GetComponent<RectTransform>();
    }
    float lastTick = -100;
    bool Timer()
    {
        if (!Active || Time.time < lastTick + delay) return false;
        return true;
    }

    public void Slide(bool right)
    {
        if (right) highlight += 1;
        else highlight -= 1;
        if (highlight < 0)
        {
            highlight = 0;
            lastTick = Time.time;
        }
        else if (highlight >= ItemNum)
        {
            highlight = ItemNum - 1;
            lastTick = Time.time;
        }
        else lastTick = -100;
        updateOnce = true;
    }

    public void UpdateHighLight()
    {
        KeepZeroArea();
        if (!Timer()) return;
        //目前光标对准的位置的x坐标
        float offset = originalOffset - rect.offsetMin.x;
        //上次高光对准的目标的x坐标
        if (highlight < 0) highlight = 0;
        if (highlight >= ItemNum) highlight = ItemNum - 1;
        float lastOffset = highlight * gridWidth;
        int step = (int)Mathf.Sign(offset - lastOffset);
        if (step == 0) return;
        int index = highlight;
        //if (Mathf.Abs(offset - lastOffset) > slideNextThreshold)
        //{
        //    Debug.Log(step);
        //    index = highlight + step;
        //    if (index >= ItemNum || index < 0) index = highlight;
        //    highlight = index;
        //}
        float minDist = Mathf.Abs(Child(index).offsetMin.x - offset);
        while (!doNotAutoHighlight && index >= 0 && index < ItemNum)
        {
            var child = Child(index);
            float newDist = Mathf.Abs(child.offsetMin.x - offset);
            if (newDist < minDist)
            {
                highlight = index;
                minDist = newDist;
            }
            index += step;
        }
        lastTick = Time.time;
        float newOffsetMinX = originalOffset - highlight * gridWidth;
        Vector2 newOffset = new Vector2(newOffsetMinX, rect.offsetMin.y);
        tweener = DOTween.To(() => rect.offsetMin, (x) => rect.offsetMin = x, newOffset, animateTime).SetEase(Ease.InOutSine);


        updateOnce = false;
    }

    void UpdateOriginalOffset()
    {
        float diff = -Child(highlight).rect.width / 2 - originalOffset;
        originalOffset += diff;

        if (Mathf.Abs(diff) > reshapeThreshold)
        {
            rect.offsetMin = rect.offsetMin + new Vector2(diff, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        originalOffset = GetComponent<RectTransform>().offsetMin.x;
        for ( int i = 0; i < transform.childCount; i++)
        {
            items.Add(Child(i).gameObject);
        }
        if (gridWidth <= 0) RecalculateStride();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOriginalOffset();
        RecalculateStride();
        if (!doNotAutoHighlight || updateOnce)
            UpdateHighLight();
    }
}
