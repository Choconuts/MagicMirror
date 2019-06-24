using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkinnedMesh : MonoBehaviour
{
    public GameObject skinnedMeshObject;

    Mesh mesh;
    Mesh sharedMesh;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshFilter>().mesh = new Mesh();
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Update is called once per frame
    void Update()
    {
        if (!sharedMesh) sharedMesh = skinnedMeshObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        mesh.vertices = sharedMesh.vertices;
        mesh.triangles = sharedMesh.triangles;
        mesh.RecalculateNormals();
    }
}
