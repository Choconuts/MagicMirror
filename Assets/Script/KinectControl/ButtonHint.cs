using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHint : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();
    public List<Transform> hints = new List<Transform>();
    public GameObject pointer;
    public Transform remind;
    public InteractionManager interactionManager;
    public Camera screenCamera;

    private Vector3 screenNormalPos = Vector3.zero;
    private Vector2 screenPixelPos = Vector2.zero;

    int HoverIndex()
    {
        if (screenCamera == null)
        {
            screenCamera = Camera.main;
        }
        if (interactionManager == null)
        {
            interactionManager = InteractionManager.Instance;
        }

        screenNormalPos = interactionManager.IsLeftHandPrimary() ? interactionManager.GetLeftHandScreenPos() : interactionManager.GetRightHandScreenPos();

        screenPixelPos.x = (int)(screenNormalPos.x * (screenCamera ? screenCamera.pixelWidth : Screen.width));
        screenPixelPos.y = (int)(screenNormalPos.y * (screenCamera ? screenCamera.pixelHeight : Screen.height));

        for (int i = 0; i < buttons.Count; i++)
        {
            Button btn = buttons[i];
            if (btn.gameObject.activeSelf || RectTransformUtility.RectangleContainsScreenPoint(btn.image.rectTransform, screenPixelPos, null))
            {
                return i;
            }
        }
        return -1;
    }

    void ShowHint(int index)
    {
        for(int i = 0; i < hints.Count; i++)
        {
            if (i == index) hints[i].gameObject.SetActive(true);
            else hints[i].gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in remind)
            hints.Add(t);
    }

    // Update is called once per frame
    void Update()
    {
        ShowHint(HoverIndex());
    }
}
