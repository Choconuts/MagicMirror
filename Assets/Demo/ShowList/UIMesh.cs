using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMesh : MonoBehaviour
{

    public MeshFilter meshFilter;
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CanvasRenderer cr = GetComponent<CanvasRenderer>();
        cr.SetMesh(meshFilter.sharedMesh);
        transform.localScale = new Vector3(500, 500, 500);
    }
}
