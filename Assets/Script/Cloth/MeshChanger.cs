using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.IO.MemoryMappedFiles;

public class MeshChanger : MonoBehaviour
{
    public Contactor contactor;
    MeshFilter meshFilter;

    public void Debugger(Vector3[] vs)
    {
        //Debug.Log(vs[232]);
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3[] verts = new Vector3[7366];
        int[] faces = new int[14496 * 3];
        
        meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        Debug.Log(contactor.viewAccessor == null);
        //contactor.viewAccessor.ReadArray(contactor.GetInfoOffset(Contactor.Info.Vertex), verts, 0, contactor.vert);
        //contactor.viewAccessor.ReadArray(contactor.GetInfoOffset(Contactor.Info.Face), faces, 0, contactor.face * 3);
        contactor.GetInfo(Contactor.Info.Vertex, verts);
        contactor.GetInfo(Contactor.Info.Face, faces);
        mesh.vertices = verts;
        mesh.triangles = faces;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
        
    }

    // Update is called once per frame
    void Update()
    {
        int[] faces = new int[14496 * 3];

        meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;
        Vector3[] verts = new Vector3[7366];
        contactor.GetInfo(Contactor.Info.Vertex, verts, 7366);
        contactor.GetInfo(Contactor.Info.Face, faces, 14496 * 3);
        mesh.triangles = faces;
        mesh.vertices = verts;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }
}
