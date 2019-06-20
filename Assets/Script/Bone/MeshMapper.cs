using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class MeshMapper : MonoBehaviour
{
    public GameObject fromObject;
    public GameObject tempObject;
    public GameObject toObject;
    public string savePath = "Demo/Mesh/map.txt";
    public int loadSize;

    public int[] mapping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    Mesh GetMesh(GameObject obj)
    {
        MeshFilter mf = obj.GetComponent<MeshFilter>();
        if (mf) return mf.sharedMesh;
        SkinnedMeshRenderer smr = obj.GetComponent<SkinnedMeshRenderer>();
        if (smr) return smr.sharedMesh;
        return null;
    }

    [ContextMenu("Bake")]
    public void Bake()
    {
        fromObject.GetComponent<SkinnedMeshRenderer>().BakeMesh(tempObject.GetComponent<MeshFilter>().mesh);
        
    }

    [ContextMenu("Save")]
    public void Save()
    {
        Vector3[] fromVertex = (Vector3[])GetMesh(fromObject).vertices.Clone();
        Vector3[] toVertex = (Vector3[])GetMesh(toObject).vertices.Clone();

        for (int i = 0; i < toVertex.Length; i++)
        {
            toVertex[i] = toObject.transform.TransformPoint(toVertex[i]);
        }

        for (int i = 0; i < fromVertex.Length; i++)
        {
            fromVertex[i] = fromObject.transform.TransformPoint(fromVertex[i]);
        }

        mapping = new int[toVertex.Length];




        for (int i = 0; i < toVertex.Length; i++)
        {
            float minDist = 100000;
            int minIndex = -1;
            for (int j = 0; j < fromVertex.Length; j++)
            {
                float dist = (toVertex[i] - fromVertex[j]).magnitude;
                if (dist < minDist)
                {
                    minDist = dist;
                    minIndex = j;
                }
            }
            mapping[i] = minIndex;
        }

        FileStream file = File.Open(savePath, FileMode.Create);
        BinaryWriter writer = new BinaryWriter(file);
        
        for (int i = 0; i < mapping.Length; i++)
        {
            writer.Write(mapping[i]);
        }
        file.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        int length;
        if (loadSize > 0) length = loadSize;
        else length = GetMesh(toObject).vertices.Length;
        mapping = new int[length];

        FileStream file = File.Open(savePath, FileMode.Open);
        BinaryReader reader = new BinaryReader(file);

        for (int i = 0; i < length; i++)
        {
            int index = reader.ReadInt32();
            mapping[i] = index;
        }
        file.Close();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
