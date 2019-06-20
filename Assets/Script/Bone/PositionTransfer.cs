using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTransfer : MonoBehaviour
{
    public GameObject source;
    public GameObject target;
    public bool isTransfered;
    Mesh mesh;
    MeshMapper mapper;
    Vector3[] positions;

    // Start is called before the first frame update
    void Start()
    {
        mapper = GetComponent<MeshMapper>();
        if (mapper.mapping.Length == 0) mapper.Load();

    }

    // Update is called once per frame
    void Update()
    {
        mesh = target.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        Vector3[] newVertices = new Vector3[mesh.vertexCount];
        BoneWeight[] newBoneWeights = new BoneWeight[mesh.vertexCount];

        if (!isTransfered && mesh.vertexCount == mapper.mapping.Length)
        {
            positions = (Vector3[])source.GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices.Clone();
            BoneWeight[] boneWeights = (BoneWeight[])source.GetComponent<SkinnedMeshRenderer>().sharedMesh.boneWeights.Clone();
            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                newVertices[i] = positions[mapper.mapping[i]];
                newBoneWeights[i] = boneWeights[mapper.mapping[i]];
            }
            isTransfered = true;
            mesh.vertices = newVertices;
            mesh.boneWeights = newBoneWeights;
            mesh.RecalculateNormals();
        }
    }

    private void OnDestroy()
    {

    }
}
