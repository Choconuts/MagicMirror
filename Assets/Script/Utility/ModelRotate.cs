using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotate : MonoBehaviour, InteractionListenerInterface
{
    [Tooltip("Interaction manager instance, used to detect hand interactions. If left empty, it will be the first interaction manager found in the scene.")]
    public InteractionManager interactionManager;

    [Tooltip("Camera used for screen ray-casting. This is usually the main camera.")]
    public Camera screenCamera;

    private InteractionManager.HandEventType lastHandEvent = InteractionManager.HandEventType.None;
    private InteractionManager.HandEventType nowHandEvent = InteractionManager.HandEventType.None;
    private Vector3 screenNormalPos = Vector3.zero;
    private Vector2 screenPixelPos = Vector2.zero;
    private Vector2 lastScreenPixelPos = Vector2.zero;

    private Vector2 rotation = Vector2.zero;

    void Start()
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
    }

    void Update()
    {
        if (interactionManager != null && interactionManager.IsInteractionInited())
        {
            lastScreenPixelPos = screenPixelPos;

            // convert the normalized screen pos to pixel pos
            screenNormalPos = interactionManager.IsLeftHandPrimary() ? interactionManager.GetLeftHandScreenPos() : interactionManager.GetRightHandScreenPos();

            screenPixelPos.x = (int)(screenNormalPos.x * (screenCamera ? screenCamera.pixelWidth : Screen.width));
            screenPixelPos.y = (int)(screenNormalPos.y * (screenCamera ? screenCamera.pixelHeight : Screen.height));

            //print(lastHandEvent);

            if (lastHandEvent != InteractionManager.HandEventType.Grip && nowHandEvent == InteractionManager.HandEventType.Grip)
            {
                rotation.x = (screenPixelPos.x - lastScreenPixelPos.x)*0.001f;
                print(rotation);
                if(rotation.x != 0)
                {
                    GameObject.FindGameObjectWithTag("model").GetComponent<ModelViewer>().BeginDrag();
                    GameObject.FindGameObjectWithTag("model").GetComponent<ModelViewer>().Rotate(rotation);

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
