using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointTracker : MonoBehaviour
{
    public Transform root;
    public Transform special;
    public Transform[] nodes = new Transform[20];
    public Contactor contactor;
    public string prefix = "S_";

    Vector3[] GenJointPosArray()
    {
        Vector3[] array = new Vector3[21];
        Vector3 axangle;
        float angle;
        (nodes[0].rotation * special.rotation).ToAngleAxis(out angle, out axangle);
        array[0] = axangle * angle;
        Debug.Log(array[0]);
        for (int i = 0; i < 20; i++)
        {
            
            array[i + 1] = (nodes[i].position + special.position);
            for (int j = 0; j < 3; j++)
                array[i + 1][j] *= special.localScale[j];
        }
        return array;
    }

    // Start is called before the first frame update
    void Start()
    {
        nodes = new Transform[20];
        foreach (var joint in jointNames)
        {
            nodes[joint.Key] = GameObject.Find(prefix + joint.Value).transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        contactor.SetInfo(Contactor.Info.SelfDefine, GenJointPosArray(), 21);
    }

    readonly Dictionary<int, string> jointNames = new Dictionary<int, string>()
    {
        {0, "Root" },
        {1, "Spine" },
        {2, "Neck" },
        {3, "Head" },
        {4, "Shoulder_Left" },
        {5, "Elbow_Left" },
        {6, "Wrist_Left" },
        {7, "Fingers_Left" },
        {8, "Shoulder_Right" },
        {9, "Elbow_Right" },
        {10, "Wrist_Right" },
        {11, "Fingers_Right" },
        {12, "Hip_Left" },
        {13, "Knee_Left" },
        {14, "Ankle_Left" },
        {15, "Toe_Left" },
        {16, "Hip_Right" },
        {17, "Knee_Right" },
        {18, "Ankle_Right" },
        {19, "Toe_Right" },
    };
}
