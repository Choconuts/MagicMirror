using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshChanger : MonoBehaviour
{
    public Mesh mesh;

    public GameObject sample;

    // Start is called before the first frame update
    void Start()
    {

    }

    [ContextMenu("Clone")]
    void Clone()
    {
        mesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        if (mesh == null)
        {
            mesh = new Mesh();
            GetComponent<SkinnedMeshRenderer>().sharedMesh = mesh;
        }
        Mesh sampleMesh = sample.GetComponent<MeshFilter>().mesh;
        bool flag = false;
        if (mesh.vertexCount >= sampleMesh.vertexCount)
        {
            flag = true;
            mesh.triangles = sampleMesh.triangles;
        }
        Vector3[] vertices = (Vector3[])sampleMesh.vertices.Clone();
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = sample.transform.TransformPoint(vertices[i]);
        }
        mesh.vertices = vertices;
        if (!flag)
        {
            mesh.triangles = sampleMesh.triangles;
        }
        mesh.RecalculateNormals();
    }

    [ContextMenu("Flip")]
    void Flip()
    {
        mesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        mesh.RecalculateNormals();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
