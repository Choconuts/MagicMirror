using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothTemplate : MonoBehaviour
{
    public static ClothTemplate instance;
    public SkinnedMeshRenderer mesh;

    public Mesh GetMesh()
    {
        return mesh.sharedMesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
