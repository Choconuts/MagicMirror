using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shelf : MonoBehaviour
{
    float maxLift = -360;
    float liftTime = 0.3f;
    RectTransform rect;
    Vector2 originMin;
    Vector2 originMax;
    float lift;

    public void Hide()
    {
        DOTween.To(() => lift, (x) => lift = x, maxLift, liftTime).SetEase(Ease.OutQuad).OnComplete(()=> { gameObject.SetActive(false); });
    }

    public void Show()
    {
        gameObject.SetActive(true);
        DOTween.To(() => maxLift, (x) => lift = x, 0, liftTime).SetEase(Ease.OutQuad);
    }

    public void Switch()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        originMin = rect.offsetMin;
        originMax = rect.offsetMax;
        lift = 0;
    }

    // Update is called once per frame
    void Update()
    {
        rect.offsetMin = originMin + new Vector2(0, lift);
        rect.offsetMax = originMax + new Vector2(0, lift);
    }
}
