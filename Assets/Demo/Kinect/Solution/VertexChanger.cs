using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexChanger : MonoBehaviour
{

    public Mesh mesh;
    public Contactor contactor;
    public GameObject cube;
    public bool change;
    int vertexNum = 7366;
    int faceNum = 14496;

    Vector3[] tmpVertex;
    int[] tmpFaces;

    // Start is called before the first frame update
    void Start()
    {

        mesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        tmpVertex = (Vector3[])mesh.vertices.Clone();
        tmpFaces = (int[])mesh.triangles.Clone();
        Debug.Log(tmpVertex.Length);
        Debug.Log(tmpFaces.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (cube)
        {
            mesh = cube.GetComponent<MeshFilter>().mesh;
        }

        if (change)
        {
            Vector3[] vertices = new Vector3[vertexNum];
            int[] faces = new int[faceNum * 3];
            contactor.GetInfo(Contactor.Info.Vertex, vertices);
            contactor.GetInfo(Contactor.Info.Face, faces);

            mesh.triangles = faces;

            //Vector3[] newVertices = new Vector3[tmpVertex.Length];
            //for (int i = 0; i < faceNum * 3; i += 3)
            //{
            //    for (int j = 0; j < 3; j++)
            //        newVertices[mesh.triangles[i + j]] = vertices[faces[i + j]] * 0.01f;
    
            //}
            mesh.vertices = vertices;
            mesh.RecalculateNormals();
            Debug.Log(mesh.vertexCount);
            change = false;
        }
    }

    private void OnDestroy()
    {
        mesh.vertices = tmpVertex;
        mesh.triangles = tmpFaces;
        mesh.RecalculateNormals();
    }
}
