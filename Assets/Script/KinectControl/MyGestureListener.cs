using UnityEngine;
using System;
using UnityEngine.UI;


public class MyGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    [Tooltip("GUI-Text to display gesture-listener messages and gesture information.")]
    public Text gestureInfo;
    [Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
    public int playerIndex = 0;
    public static MyGestureListener Instance;
    private void Awake()
    {
        Instance = this;
    }
    // private bool to track if progress message has been displayed
    private bool progressDisplayed;
    private float progressGestureTime;


    // whether the needed gesture has been detected or not


    /// <summary>
    /// 右手举起过肩并保持至少一秒
    /// </summary>
    private bool RaiseRightHand;
    /// <summary>
    /// 左手举起过肩并保持至少一秒
    /// </summary>
    private bool RaiseLeftHand;
    /// <summary>
    /// 双手举起过肩并保持至少一秒
    /// </summary>
    private bool Psi;
    /// <summary>
    ///  双手下垂
    /// </summary>
    private bool Stop;
    /// <summary>
    /// 左手或右手举起来回摆动
    /// </summary>
    private bool Wave;
    /// <summary>
    /// 左手或右手在适当的位置停留至少2.5秒
    /// </summary>
    private bool Click;
    /// <summary>
    /// 假手势，用来使光标随着手移动
    /// </summary>
    private bool RightHandCursor;
    /// <summary>
    /// 假手势，用来使光标随着手移动
    /// </summary>
    private bool LeftHandCursor;
    /// <summary>
    ///手肘向下，左右手掌合在一起（求佛的手势），然后慢慢分开.
    /// </summary>
    private bool ZoomOut;
    /// <summary>
    /// 手肘向下，两手掌相聚至少0.7米，然后慢慢合在一起
    /// </summary>
    private bool ZoomIn;
    private float zoomFactor = 1f;
    /// <summary>
    /// 英文原版描述不清，就是ZoomOut/In的手势，只不过在动的时候是前后而非左右
    /// </summary>
    private bool Wheel;
    /// <summary>
    ///  在1.5秒内髋关节中心至少下降10厘米
    /// </summary>
    private bool Squat;
    /// <summary>
    ///  在1.5秒内将左手或右手向外推
    /// </summary>
    private bool Push;
    /// <summary>
    /// 在1.5秒内将左手或右手向里拉
    /// </summary>
    private bool Pull;
    /// <summary>
    /// 右手向左挥
    /// </summary>
    private bool SwipeLeft;
    /// <summary>
    ///  左手向右挥.
    /// </summary>
    private bool SwipeRight;
    /// <summary>
    ///  左手或者右手向上挥
    /// </summary>
    private bool SwipeUp;
    /// <summary>
    ///  左手或者右手向下挥
    /// </summary>
    private bool SwipeDown;
    /// <summary>
    /// 在1.5秒内髋关节中心至少上升10厘米
    /// </summary>
    private bool Jump;
    /// <summary>
    /// T字体位
    /// </summary>
    private bool Tpose;


    /// <summary>
    /// Determines whether swipe left is detected.
    /// </summary>
    /// <returns><c>true</c> if swipe left is detected; otherwise, <c>false</c>.</returns>
    public bool IsRaiseRightHand()
    {
        if (RaiseRightHand)
        {
            RaiseRightHand = false;
            return true;
        }


        return false;
    }
    public bool IsRaiseLeftHand()
    {
        if (RaiseLeftHand)
        {
            RaiseLeftHand = false;
            return true;
        }


        return false;
    }
    public bool IsPsi()
    {
        if (Psi)
        {
            Psi = false;
            return true;
        }


        return false;
    }
    public bool IsStop()
    {
        if (Stop)
        {
            Stop = false;
            return true;
        }


        return false;
    }
    public bool IsWave()
    {
        if (Wave)
        {
            Wave = false;
            return true;
        }


        return false;
    }
    public bool IsSwipeLeft()
    {
        if (SwipeLeft)
        {
            SwipeLeft = false;
            return true;
        }


        return false;
    }
    public bool IsJump()
    {
        if (Jump)
        {
            Jump = false;
            return true;
        }


        return false;
    }
    public bool IsSwipeRight()
    {
        if (SwipeRight)
        {
            SwipeRight = false;
            return true;
        }


        return false;
    }
    public bool IsSwipeUp()
    {
        if (SwipeUp)
        {
            SwipeUp = false;
            return true;
        }


        return false;
    }
    public bool IsSwipeDown()
    {
        if (SwipeDown)
        {
            SwipeDown = false;
            return true;
        }


        return false;
    }
    public bool IsZoomOut()
    {
        if (ZoomOut)
        {
            return ZoomOut;
        }


        return false;
    }
    public bool IsZoomIn()
    {
        return ZoomIn;


    }
    /// <summary>
    /// Gets the zoom factor.
    /// </summary>
    /// <returns>The zoom factor.</returns>
    public float GetZoomFactor()
    {
        return zoomFactor;
    }
    public bool IsWheel()
    {
        if (Wheel)
        {
            Wheel = false;
            return true;
        }


        return false;
    }
    public bool IsSquat()
    {
        if (Squat)
        {
            Squat = false;
            return true;
        }


        return false;
    }
    public bool IsPush()
    {
        if (Push)
        {
            Push = false;
            return true;
        }


        return false;
    }
    public bool IsPull()
    {
        if (Pull)
        {
            Pull = false;
            return true;
        }


        return false;
    }
    public bool IsTpose()
    {
        if (Tpose)
        {
            Tpose = false;
            return true;
        }


        return false;
    }
    /*
    * 当识别到用户时调用该函数
    */
    public void UserDetected(long userId, int userIndex)
    {
        // as an example - detect these user specific gestures
        KinectManager manager = KinectManager.Instance;
        manager.DetectGesture(userId, KinectGestures.Gestures.RaiseRightHand);
        manager.DetectGesture(userId, KinectGestures.Gestures.RaiseLeftHand);
        manager.DetectGesture(userId, KinectGestures.Gestures.Psi);
        manager.DetectGesture(userId, KinectGestures.Gestures.Stop);
        manager.DetectGesture(userId, KinectGestures.Gestures.Wave);
        manager.DetectGesture(userId, KinectGestures.Gestures.RaiseRightHand);
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeUp);
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeDown);
        // gestures 枚举里 Click默认为 注释
        //  manager.DetectGesture(userId, KinectGestures.Gestures.Click);
        manager.DetectGesture(userId, KinectGestures.Gestures.ZoomOut);
        manager.DetectGesture(userId, KinectGestures.Gestures.ZoomIn);
        manager.DetectGesture(userId, KinectGestures.Gestures.Wheel);
        manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
        manager.DetectGesture(userId, KinectGestures.Gestures.Squat);
        manager.DetectGesture(userId, KinectGestures.Gestures.Push);
        manager.DetectGesture(userId, KinectGestures.Gestures.Pull);
        manager.DetectGesture(userId, KinectGestures.Gestures.Tpose);


        if (gestureInfo != null)
        {
            gestureInfo.GetComponent<Text>().text = "暂未检测到手势";
        }
        Debug.Log("发现用户");
    }






    /*
    * 当失去用户时出发
    */
    public void UserLost(long userId, int userIndex)
    {
        if (gestureInfo != null)
        {
            gestureInfo.GetComponent<Text>().text = string.Empty;
        }
        Debug.Log("失去用户");
    }




    /// <summary>
    /// Invoked when a gesture is in progress.
    /// </summary>
    /// <param name="userId">被识别者的id</param>
    /// <param name="userIndex">被识别者的序号</param>
    /// <param name="gesture">手势类型</param>
    /// <param name="progress">手势识别的进度，可以认为是相似度。范围是[0,1]</param>
    /// <param name="joint">关节类型</param>
    /// <param name="screenPos">视图坐标的单位向量</param>
    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture,
                 float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {
        /*
         * 主要将一些需要动态监测的手势放在这个函数下
         * 比如说缩放、滚轮都是依据你两手之间的距离来判断应该缩放或旋转多少度
         */


        //监测缩放，如果相似度大于50%
        // the gestures are allowed for the primary user only
        if (userIndex != playerIndex)
            return;


        if (gesture == KinectGestures.Gestures.ZoomOut)
        {
            if (progress > 0.5f)
            {
                ZoomOut = true;
                zoomFactor = screenPos.z;


                if (gestureInfo != null)
                {
                    string sGestureText = string.Format("{0} factor: {1:F0}%", gesture, screenPos.z * 100f);
                    gestureInfo.text = sGestureText;


                    progressDisplayed = true;
                    progressGestureTime = Time.realtimeSinceStartup;
                }
            }
            else
            {
                ZoomOut = false;
            }
        }
        else if (gesture == KinectGestures.Gestures.ZoomIn)
        {
            if (progress > 0.5f)
            {
                ZoomIn = true;
                zoomFactor = screenPos.z;


                if (gestureInfo != null)
                {
                    string sGestureText = string.Format("{0} factor: {1:F0}%", gesture, screenPos.z * 100f);
                    gestureInfo.text = sGestureText;


                    progressDisplayed = true;
                    progressGestureTime = Time.realtimeSinceStartup;
                }
            }
            else
            {
                ZoomIn = false;
            }
        }
        else if ((gesture == KinectGestures.Gestures.Wheel || gesture == KinectGestures.Gestures.LeanLeft ||
            gesture == KinectGestures.Gestures.LeanRight) && progress > 0.5f)
        {
            if (gestureInfo != null)
            {
                string sGestureText = string.Format("{0} - {1:F0} degrees", gesture, screenPos.z);
                gestureInfo.GetComponent<Text>().text = sGestureText;


                progressDisplayed = true;
                progressGestureTime = Time.realtimeSinceStartup;
            }
        }
        else if (gesture == KinectGestures.Gestures.Run && progress > 0.5f)
        {
            if (gestureInfo != null)
            {
                string sGestureText = string.Format("{0} - progress: {1:F0}%", gesture, progress * 100);
                gestureInfo.GetComponent<Text>().text = sGestureText;


                progressDisplayed = true;
                progressGestureTime = Time.realtimeSinceStartup;
            }
        }
    }








    /// <summary>
    /// 当一个手势识别完成后被调用
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    /// <param name="userId">被识别者的ID</param>
    /// <param name="userIndex">被识别者的序号</param>
    /// <param name="gesture">被识别到的手势类型</param>
    /// <param name="joint">被识别到的关节类型</param>
    /// <param name="screenPos">视图坐标的单位向量</param>
    public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture,
                 KinectInterop.JointType joint, Vector3 screenPos)
    {
        if (progressDisplayed)
            return true;


        string sGestureText = gesture + " detected";
        if (gestureInfo != null)
        {
            gestureInfo.GetComponent<Text>().text = sGestureText;
        }
        // 可以在这里写识别到的所有手势,完成之后去调用某个方法,
        // 但为了右面可能不止一个场景可能使用到相同的手势,所以使用bool跳出去,在使用到的时候去写完成动作之后的方法
        switch (gesture)
        {
            case KinectGestures.Gestures.None:
                break;

            case KinectGestures.Gestures.RaiseRightHand:
                RaiseRightHand = true;
                break;
            case KinectGestures.Gestures.RaiseLeftHand:
                RaiseLeftHand = true;
                break;
            case KinectGestures.Gestures.Psi:
                Psi = true;
                break;
            case KinectGestures.Gestures.Tpose:
                Tpose = true;
                break;
            case KinectGestures.Gestures.Stop:
                Stop = true;
                break;
            case KinectGestures.Gestures.Wave:
                Wave = true;
                break;
            case KinectGestures.Gestures.SwipeLeft:
                SwipeLeft = true;
                break;
            case KinectGestures.Gestures.SwipeRight:
                SwipeRight = true;
                break;
            case KinectGestures.Gestures.SwipeUp:
                SwipeUp = true;
                break;
            case KinectGestures.Gestures.SwipeDown:
                SwipeDown = true;
                break;
            case KinectGestures.Gestures.ZoomIn:
                ZoomIn = true;
                break;
            case KinectGestures.Gestures.ZoomOut:
                ZoomOut = true;
                break;
            case KinectGestures.Gestures.Wheel:
                Wheel = true;
                break;
            case KinectGestures.Gestures.Jump:
                Jump = true;
                break;
            case KinectGestures.Gestures.Squat:
                Squat = true;
                break;
            case KinectGestures.Gestures.Push:
                Push = true;
                break;
            case KinectGestures.Gestures.Pull:
                Pull = true;
                break;
            case KinectGestures.Gestures.ShoulderLeftFront:
                break;
            case KinectGestures.Gestures.ShoulderRightFront:
                break;
            case KinectGestures.Gestures.LeanLeft:
                break;
            case KinectGestures.Gestures.LeanRight:
                break;
            case KinectGestures.Gestures.LeanForward:
                break;
            case KinectGestures.Gestures.LeanBack:
                break;
            case KinectGestures.Gestures.KickLeft:
                break;
            case KinectGestures.Gestures.KickRight:
                break;
            case KinectGestures.Gestures.Run:
                break;
            case KinectGestures.Gestures.RaisedRightHorizontalLeftHand:
                break;
            case KinectGestures.Gestures.RaisedLeftHorizontalRightHand:
                break;
            case KinectGestures.Gestures.UserGesture1:
                break;
            case KinectGestures.Gestures.UserGesture2:
                break;
            case KinectGestures.Gestures.UserGesture3:
                break;
            case KinectGestures.Gestures.UserGesture4:
                break;
            case KinectGestures.Gestures.UserGesture5:
                break;
            case KinectGestures.Gestures.UserGesture6:
                break;
            case KinectGestures.Gestures.UserGesture7:
                break;
            case KinectGestures.Gestures.UserGesture8:
                break;
            case KinectGestures.Gestures.UserGesture9:
                break;
            case KinectGestures.Gestures.UserGesture10:
                break;
            default:
                break;
        }

        return true;
    }






    //参数同上，在手势被取消的时候调用
    public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture,
                 KinectInterop.JointType joint)
    {
        if (progressDisplayed)
        {
            progressDisplayed = false;


            if (gestureInfo != null)
            {
                gestureInfo.GetComponent<Text>().text = String.Empty;
            }
        }


        return true;
    }


    public void Update()
    {
        if (progressDisplayed && ((Time.realtimeSinceStartup - progressGestureTime) > 2f))
        {
            progressDisplayed = false;


            if (gestureInfo != null)
            {
                gestureInfo.GetComponent<Text>().text = String.Empty;
            }


            Debug.Log("Forced progress to end.");
        }
    }


}