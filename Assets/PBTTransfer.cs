using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBTTransfer : MonoBehaviour
{
    Contactor contactor;
    public Contactor target;
    public double[] pose = new double[72];
    public bool refresh;
    public AvatarController avatarController;

    // Start is called before the first frame update
    void Start()
    {
        contactor = GetComponent<Contactor>();
    }

    void Transfer()
    {
        if (refresh)
        {
            //refresh = false;
            for (int i = 0; i < 72; i++)
            {
                pose[i] += (Random.value - 0.5) * 0.01;
            }
        }

        double[] tmp = new double[79];
        contactor.GetInfo(Contactor.Info.SelfDefine, tmp, 79);
        target.SetInfo(Contactor.Info.SelfDefine, pose, 72);
    }


    void Print()
    {
        Vector3[] poseData = new Vector3[20];
        long userId = KinectManager.Instance.GetUserIdByIndex(0);
        
        for (int i = 0; i < 20; i++)
        {
            poseData[i] = KinectManager.Instance.GetJointDirection(userId, i, true, false);
            //if (i == KinectManager.Instance.GetJointIndex(KinectInterop.JointType.ShoulderRight))
        }
        
 

    }

    // Update is called once per frame
    void Update()
    {
        Print();
    }
}
