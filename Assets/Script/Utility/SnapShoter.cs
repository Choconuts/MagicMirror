using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SnapShoter : MonoBehaviour
{
    public float secound = 1f;  //不可以小于0.1f

    public List<GameObject> uiToHide = new List<GameObject>();

    public Image black;

    public Text countText;
    bool[] uiStatus;

    int count;
    void CountDown()
    {
        int t = count;
        count--;
        if (t <= 0)
        {
            countText.gameObject.SetActive(false);
            return;
        }
        countText.gameObject.SetActive(true);
        countText.text = t.ToString();
    }

    void HideUI()
    {
        uiStatus = new bool[uiToHide.Count];
        for (int i = 0; i < uiToHide.Count; i++)
        {
            uiStatus[i] = uiToHide[i].activeSelf;
            uiToHide[i].SetActive(false);
        }

        Menu.gestureLock = true;
    }

    void BlackScreen()
    {
        black.gameObject.SetActive(true);
    }

    void WhiteScreen()
    {
        black.gameObject.SetActive(false);
    }

    void RecoverUI()
    {
        for (int i = 0; i < uiToHide.Count; i++)
        {
            uiToHide[i].SetActive(uiStatus[i]);
        }

        Menu.gestureLock = false;
    }

    void SaveImage()
    {
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/Images" + "/ScreenShot.png", 0);

    }

    public void BeginSnapShot()
    {
        count = 3;
        HideUI();

        Invoke("CountDown", 0f);
        Invoke("CountDown", secound);
        Invoke("CountDown", secound * 2);
        Invoke("CountDown", secound * 3 - 0.1f);

        Invoke("BlackScreen", secound * 3 - 0.1f);
        Invoke("WhiteScreen", secound * 3 + 0.08f);

        Invoke("SaveImage", secound * 3 + 0.1f);

        Invoke("RecoverUI", secound * 3 + 0.3f);
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
