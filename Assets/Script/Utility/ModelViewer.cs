using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Rotate(Vector3 move)
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate(new Vector2(0.0f, 0.01f));
    }
}
