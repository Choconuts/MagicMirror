using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelViewer : MonoBehaviour
{
    public float originalScale;
    public float relatedScale;
    [Tooltip("x is min and y is max, y < x means no limit")]
    public Vector2 scaleLimit = new Vector2(0.5f, 2);

    public Vector2 originalOffset;
    public Vector2 relatedOffset;
    [Tooltip("X will clamp into -x to x, Y will clamp into -y to y, x < 0 means no limit")]
    public Vector2 offsetLimit = new Vector2(2, 1);

    public Quaternion originalRotation;
    public Quaternion relatedRotation;

    private MyGestureListener gestureListener;
    //public Text text;

    public void Rotate(Vector3 move)
    {
        print("rotate");
        var euler = new Vector3(move.y * 360, -move.x * 360, -move.z * 360);
        transform.rotation = Quaternion.Euler(euler) * relatedRotation;
    }

    //should large than -1
    public void Scale(float value)
    {
        float factor = relatedScale * (1 + value);
        if (scaleLimit.y >= scaleLimit.x)
        {
            factor = Mathf.Clamp(factor, scaleLimit.x * originalScale, scaleLimit.y * originalScale);
        }
        transform.localScale = new Vector3(1, 1, 1) * factor;
    }

    public void MoveXY(Vector2 move)
    {
        Vector3 newPos = relatedOffset + move;
        newPos.z = transform.position.z;
        if (offsetLimit.x >= 0)
        {
            newPos.x = Mathf.Clamp(newPos.x, -offsetLimit.x + originalOffset.x, offsetLimit.x + originalOffset.x);
        }
        if (offsetLimit.y >= 0)
        {
            newPos.y = Mathf.Clamp(newPos.y, -offsetLimit.y + originalOffset.y, offsetLimit.y + originalOffset.y);
        }
        transform.position = newPos;
    }

    public void BeginDrag()
    {
        relatedScale = transform.localScale.x;
        relatedOffset = transform.position;
        relatedRotation = transform.rotation;
    }

    public void EndDrag()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale.x;
        originalOffset = transform.position;
        originalRotation = transform.rotation;
        //Rotate(new Vector2(0.0f, 0.2f));
        //Rotate(new Vector2(0.1f, 0f));

        gestureListener = MyGestureListener.Instance;
        print(gestureListener);
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate(new Vector2(0.0f, 0.01f));

        if (!gestureListener)
        {
            print("未成功加载 MyGestureListener");
            return;
        }

        //if (gestureListener.IsSwipeUp())
        //{
        //    text.GetComponent<Text>().text = "向上挥动";
        //    print("up");
        //    if(upFlag == false)
        //    {
        //        upFlag = true;
        //        Scale(0.1f);
        //    }

        //}
        //if (gestureListener.IsSwipeDown())
        //{
        //    text.GetComponent<Text>().text = "向下挥动";
        //    print("down");
        //    if(upFlag == true)
        //    {
        //        upFlag = false;
        //    }
        //    else
        //        Scale(-0.1f);
        //}
        if (gestureListener.IsRaiseRightHand())
        {
            //text.GetComponent<Text>().text = "右手举起过肩并保持至少一秒";
            print("large");
            BeginDrag();
            Scale(0.2f);

        }
        if (gestureListener.IsRaiseLeftHand())
        {
            //text.GetComponent<Text>().text = "左手举起过肩并保持至少一秒";
            print("small");
            BeginDrag();
            Scale(-0.2f);
        }
    }
}
