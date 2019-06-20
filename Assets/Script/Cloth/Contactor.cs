using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.MemoryMappedFiles;

public class Contactor : MonoBehaviour
{
    public enum Info { Vertex, Face, UV, Shape, Pose, Index, SelfDefine };
    public string id;
    public int face = 14496;
    public int vert = 7366;
    public int beta = 4;
    public int pose = 24;
    public List<Info> infos = new List<Info>();
    public int selfDefineBytesNum;

    MeshInfo meshInfo;
    MemoryMappedFile mmf;
    public MemoryMappedViewAccessor viewAccessor;

    public void GetInfo<T>(Info info, T[] ts, int num = -1) where T : struct
    {
        if (num < 0)
        switch(info)
        {
            case Info.Vertex:
                num = vert;
                break;
            case Info.Face:
                num = face * 3;
                break;
            case Info.Pose:
                num = pose;
                break;
            case Info.Shape:
                num = beta;
                break;
            case Info.UV:
                num = vert;
                break;
            case Info.Index:
                num = 1;
                break;
        }
        Debug.Log(num);
        viewAccessor.ReadArray(GetInfoOffset(info), ts, 0, num);
    }

    public void SetInfo<T>(Info info, T[] ts, int len) where T : struct
    {
        viewAccessor.WriteArray(GetInfoOffset(info), ts, 0, len);
    }

    int capacity;

    [ContextMenu("Clear")]
    void Clear()
    {
        mmf = null;
        viewAccessor = null;
    }

    [ContextMenu("Awake")]
    // Start is called before the first frame update
    void Awake()
    {
        meshInfo = new MeshInfo(vert, face);
        capacity = 0;
        foreach(var info in infos) {
            capacity += GetInfoSize(info);
        }
        mmf = MemoryMappedFile.CreateOrOpen(id, capacity, MemoryMappedFileAccess.ReadWrite);
        viewAccessor = mmf.CreateViewAccessor(0, capacity);
        if (viewAccessor == null) Debug.Log("Error!Connect failed!");
        Debug.Log(capacity);
    }

    public int GetInfoIndex(Info info)
    {
        return infos.FindIndex(0, (Info x) => x == info);
    }

    public int GetInfoOffset(Info info)
    {
        int index = GetInfoIndex(info);
        return GetInfoOffset(index);
    }

    public int GetInfoOffset(int index)
    {
        int offset = 0;
        for (int i = 0; i < index; i++)
        {
            offset += GetInfoSize(infos[i]);
        }
        return offset;
    }

    public int GetInfoSize(Info info)
    {
        switch (info)
        {
            case Info.Vertex:
                return meshInfo.VertexBytes();
            case Info.Face:
                return meshInfo.FaceBytes();
            case Info.UV:
                return meshInfo.UVBytes();
            case Info.Shape:
                return meshInfo.ShapeBytes();
            case Info.Pose:
                return meshInfo.PoseBytes();
            case Info.Index:
                return meshInfo.intSize;
            case Info.SelfDefine:
                return selfDefineBytesNum;
        }
        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public class MeshInfo
{
    readonly int vertexNum;
    readonly int faceNum;
    public readonly int floatSize = 4;
    public readonly int intSize = 4;

    public MeshInfo(int nvert, int nface)
    {
        vertexNum = nvert;
        faceNum = nface;
    }

    public int VertexBytes()
    {
        return vertexNum * 3 * floatSize;
    }

    public int FaceBytes()
    {
        return faceNum * 3 * intSize;
    }

    public int UVBytes()
    {
        return vertexNum * 2 * floatSize;
    }

    public int ShapeBytes(int n = 4)
    {
        return n * floatSize;
    }

    public int PoseBytes(int n = 24)
    {
        return n * 3 * floatSize;
    }
}