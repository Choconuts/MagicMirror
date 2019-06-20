using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneWeightBaker : MonoBehaviour
{

    public SkinnedMeshRenderer source;
    public MeshFilter target;
    public SkinnedMeshRenderer clone;
    public CloneSkinnedMesh ghost;
    public MeshMapper mapper;
    public bool remap;


    [ContextMenu("Transfer")]
    public void Transfer()
    {
        if (remap)
        {
            mapper.Save();
            remap = false;
        }
        Mesh sharedMesh = clone.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        if (sharedMesh == source.sharedMesh)
        {
            Debug.Log("please use clone!");
            return;
        }
        sharedMesh.triangles = new int[0];
        Vector3[] vertices = (Vector3[])target.mesh.vertices.Clone();
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = target.transform.TransformPoint(vertices[i]);
        }
        sharedMesh.vertices = vertices;
        sharedMesh.triangles = (int[])target.mesh.triangles.Clone();
        sharedMesh.RecalculateNormals();
        sharedMesh.RecalculateBounds();
        sharedMesh.RecalculateTangents();

        BoneWeight[] newBoneWeights = new BoneWeight[sharedMesh.vertexCount];

        if (sharedMesh.vertexCount != mapper.mapping.Length)
        {
            Debug.Log("please generate mapping!");
            return;
        }

        //Vector3[] positions = (Vector3[])source.GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices.Clone();
        BoneWeight[] boneWeights = (BoneWeight[])source.GetComponent<SkinnedMeshRenderer>().sharedMesh.boneWeights.Clone();
        for (int i = 0; i < sharedMesh.vertices.Length; i++)
        {
            newBoneWeights[i] = boneWeights[mapper.mapping[i]];
        }
        sharedMesh.boneWeights = newBoneWeights;
        sharedMesh.RecalculateNormals();


    }

    // Start is called before the first frame update
    void Start()
    {
        ghost.skinnedMeshObject = source.gameObject;
        mapper.toObject = target.gameObject;
        mapper.fromObject = ghost.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
