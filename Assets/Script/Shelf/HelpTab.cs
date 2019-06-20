using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpTab : MonoBehaviour
{
    public RectTransform sizeDefiner;
    public RectTransform rect;

    void SetSize(float width, float height)
    {
        rect.offsetMax = new Vector2(rect.offsetMin.x + width, rect.offsetMin.y + height);
    }

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        sizeDefiner = GameObject.FindGameObjectWithTag("HelpSizeDefiner").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!sizeDefiner) sizeDefiner = GameObject.FindGameObjectWithTag("HelpSizeDefiner").GetComponent<RectTransform>();
        SetSize(sizeDefiner.rect.width, sizeDefiner.rect.height);
        var mg = transform.parent.GetComponent<Magnetic>();
    }
}
