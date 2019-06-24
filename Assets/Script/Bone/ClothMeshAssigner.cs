using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothMeshAssigner : MonoBehaviour
{

    [ContextMenu("Assign")]
    public void Assign()
    {
        if (ClothTemplate.instance)
        {
            Mesh mesh = ClothTemplate.instance.GetMesh();
            GetComponent<SkinnedMeshRenderer>().sharedMesh = mesh;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
