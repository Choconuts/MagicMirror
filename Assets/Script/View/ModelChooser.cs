using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelChooser : MonoBehaviour
{
    public Magnetic magnetic;
    public List<GameObject> models;
    public int now = -1;

    public void Highlight(int index)
    {
        //if (index >= models.Count || index < 0)
        //{
        //    print("invalid index");
        //    return;
        //}

        for (int i = 0; i < models.Count; i++)
        {
            if (index == i) models[i].SetActive(true);
            else models[i].SetActive(false);
        }
    }

    public void Refresh()
    {
        int next = magnetic.highlight;
        if (next != now)
        {
            GetComponent<ModelViewer>().ResetView();
        }
        now = next;
        Highlight(now);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in transform)
        {
            models.Add(t.gameObject);
        }
        Highlight(now);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
