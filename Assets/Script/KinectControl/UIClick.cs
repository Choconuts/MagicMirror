using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIClick : MonoBehaviour, InteractionListenerInterface
{
    [Tooltip("Interaction manager instance, used to detect hand interactions. If left empty, it will be the first interaction manager found in the scene.")]
    public InteractionManager interactionManager;

    [Tooltip("Camera used for screen ray-casting. This is usually the main camera.")]
    public Camera screenCamera;

    public float gripDelay = 0.6f;
    float lastGripTime = -1;

    private Button[] btns;

    private InteractionManager.HandEventType lastHandEvent = InteractionManager.HandEventType.None;
    private InteractionManager.HandEventType nowHandEvent = InteractionManager.HandEventType.None;
    private Vector3 screenNormalPos = Vector3.zero;
    private Vector2 screenPixelPos = Vector2.zero;

    private Button lastOnButton = null;
    private Color btn_normal = Color.white;
    private Color btn_focused = Color.grey;
    private Color btn_pressed = Color.yellow;

    void Start()
    {

    }

    void Update()
    {
        // by default set the main-camera to be screen-camera
        if (screenCamera == null)
        {
            screenCamera = Camera.main;
        }
        // get the interaction manager instance
        if (interactionManager == null)
        {
            interactionManager = InteractionManager.Instance;
        }
        // get all buttons in scene
        btns = GameObject.FindObjectsOfType<Button>();

        if (interactionManager != null && interactionManager.IsInteractionInited())
        {
            // convert the normalized screen pos to pixel pos
            screenNormalPos = interactionManager.IsLeftHandPrimary() ? interactionManager.GetLeftHandScreenPos() : interactionManager.GetRightHandScreenPos();

            screenPixelPos.x = (int)(screenNormalPos.x * (screenCamera ? screenCamera.pixelWidth : Screen.width));
            screenPixelPos.y = (int)(screenNormalPos.y * (screenCamera ? screenCamera.pixelHeight : Screen.height));

            if (lastOnButton == null)
            {
                foreach (Button btn in btns)
                {
                    if (RectTransformUtility.RectangleContainsScreenPoint(btn.image.rectTransform, screenPixelPos, null))
                    {
                        lastOnButton = btn;
                        btn.image.color = btn_focused;
                        break;
                    }
                }
            }
            else
            {
                if (!RectTransformUtility.RectangleContainsScreenPoint(lastOnButton.image.rectTransform, screenPixelPos, null))
                {
                    lastOnButton.image.color = btn_normal;
                    lastOnButton = null;
                    return;
                }
                if (lastHandEvent != InteractionManager.HandEventType.Grip && nowHandEvent == InteractionManager.HandEventType.Grip)
                {
                    if (lastOnButton && lastGripTime < 0)
                    {
                        lastGripTime = Time.time;
                    }
                    if (lastOnButton == null)
                    {
                        lastGripTime = -1;
                        lastHandEvent = InteractionManager.HandEventType.Grip;
                    }
                    if (lastGripTime > 0 && Time.time - lastGripTime > gripDelay)
                    {
                        lastOnButton.image.color = btn_pressed;
                        lastOnButton.onClick.Invoke();
                        lastHandEvent = InteractionManager.HandEventType.Grip;

                        lastGripTime = -1;
                    }
                }
                else if (lastHandEvent == InteractionManager.HandEventType.Grip && nowHandEvent != InteractionManager.HandEventType.Grip)
                {
                    lastOnButton.image.color = btn_focused;
                }
            }
        }
    }

    public void HandGripDetected(long userId, int userIndex, bool isRightHand, bool isHandInteracting, Vector3 handScreenPos)
    {
        if (!isHandInteracting || !interactionManager)
            return;
        if (userId != interactionManager.GetUserID())
            return;

        lastHandEvent = nowHandEvent;
        nowHandEvent = InteractionManager.HandEventType.Grip;
        //isLeftHandDrag = !isRightHand;
        screenNormalPos = handScreenPos;
    }

    public void HandReleaseDetected(long userId, int userIndex, bool isRightHand, bool isHandInteracting, Vector3 handScreenPos)
    {
        if (!isHandInteracting || !interactionManager)
            return;
        if (userId != interactionManager.GetUserID())
            return;

        lastHandEvent = nowHandEvent;
        nowHandEvent = InteractionManager.HandEventType.Release;
        //isLeftHandDrag = !isRightHand;
        screenNormalPos = handScreenPos;
    }

    public bool HandClickDetected(long userId, int userIndex, bool isRightHand, Vector3 handScreenPos)
    {
        return true;
    }
}
