using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    public static List<MouseDrag> activeInstances = new List<MouseDrag>();
    public Magnetic shelf;
    public ModelViewer viewer;
    public float restThreshold;
    public float moveThreshold;
    public float moveFactor = 1;
    public int layer;

    public bool hold;
    public float mouseX;
    public float mouseY;
    public float lastMoveTime;
    public float lastMouseX;

    void ShelfDragOld()
    {
        if (!LayerJudge())
        {
            hold = false;
            shelf.EndDrag();
            return;
        }
        if (hold)
        {
            shelf.Slide(Input.mousePosition.x - mouseX);
        }

        if (Input.GetMouseButtonDown(0))
        {
            shelf.BeginDrag();
            hold = true;
            mouseX = Input.mousePosition.x;
        }

        if (Input.GetMouseButtonUp(0))
        {
            hold = false;
            shelf.EndDrag();
        }
    }

    void ShelfDrag()
    {
        if (!LayerJudge())
        {
            hold = false;
            shelf.EndDrag();
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            shelf.BeginDrag();
            hold = true;
            mouseX = Input.mousePosition.x;
            lastMoveTime = Time.time;
        }

        if (hold && Mathf.Abs(Input.mousePosition.x - lastMouseX) > moveThreshold)
        {
            if (shelf.Active)
            {
                shelf.BeginDrag();
                mouseX = Input.mousePosition.x;
            }
            shelf.Slide((Input.mousePosition.x - mouseX) * moveFactor);
            lastMoveTime = Time.time;
            lastMouseX = Input.mousePosition.x;
        }

        if (lastMoveTime > 0 && hold && Time.time - lastMoveTime > restThreshold)
        {
            shelf.EndDrag();
            lastMoveTime = -1;
        }

        if (Input.GetMouseButtonUp(0))
        {
            hold = false;
            shelf.EndDrag();
        }
    }

    void ViewScaleDrag()
    {
        if (!LayerJudge())
        {
            hold = false;
            viewer.EndDrag();
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            viewer.BeginDrag();
            hold = true;
            mouseX = Input.mousePosition.x;
            mouseY = Input.mousePosition.y;
        }

        if (hold)
        {
            //viewer.Scale(((Vector2)Input.mousePosition - new Vector2(mouseX, mouseY)).x * moveFactor);  缩放
            viewer.Rotate(((Vector2)Input.mousePosition - new Vector2(mouseX, mouseY)) * moveFactor);   //平移
        }

        if (Input.GetMouseButtonUp(0))
        {
            hold = false;
            viewer.EndDrag();
        }
    }

    bool LayerJudge()
    {
        foreach(var md in activeInstances)
        {
            if (md.layer > layer) return false;
        }
        return true;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        activeInstances.Add(this);
        lastMoveTime = -100;
        hold = false;
    }

    private void OnDisable()
    {
        activeInstances.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (shelf)
            ShelfDrag();
        if (viewer)
        {
            ViewScaleDrag();
        }
    }
}
