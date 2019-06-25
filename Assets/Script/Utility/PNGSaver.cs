using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[RequireComponent(typeof(Camera))]
[UnityEditor.InitializeOnLoad]
public class PNGSaver : MonoBehaviour
{

    Camera m_Cam;
    [Tooltip("relative to Assets")]
    public string savePath = "/Demo/ShowList/";
    public string fileName = "cloth.png";

    [ContextMenu("Save")]
    public void SavePNG()
    {
        string path = Application.dataPath + savePath;
        string filename = fileName;
        Texture2D tex = TextureToTexture2D(GetComponent<RawImage>().texture);
        FileStream file = File.Open(path + filename, FileMode.Create);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(tex.EncodeToPNG());
        file.Close();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Texture2D TextureToTexture2D(Texture texture)
    {
        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 32);
        Graphics.Blit(texture, renderTexture);

        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = currentRT;
        RenderTexture.ReleaseTemporary(renderTexture);

        return texture2D;
    }

}
