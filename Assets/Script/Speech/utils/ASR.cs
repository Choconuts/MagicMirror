using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// use restful API to get text
namespace BaiduAip.Speech
{
    // Token response package struct
    [Serializable]
    class TokenResponse
    {
        public string access_token = null;
    }
    
    // Asr response package struct
    [Serializable]
    public class AsrResponse
    {
        public int err_no;
        public string err_msg;
        public string sn;
        public string[] result;
    }
    
    public class Asr
    {
        private const string UrlAsr = "https://vop.baidu.com/server_api";
        
        // get token status
        protected enum TokenFetchStatus
        {
            NotFetched,
            Fetching,
            Success,
            Failed
        }
        
        protected TokenFetchStatus tokenFetchStatus = TokenFetchStatus.NotFetched;
        
        public string SecretKey { get; private set; }

        public string APIKey { get; private set; }

        public string Token { get; private set; }

        public Asr(string apiKey, string secretKey)
        {
            APIKey = apiKey;
            SecretKey = secretKey;
        }
        
        public IEnumerator GetAccessToken()
        {
            Debug.Log("[ASR]Start fetching token...");
            tokenFetchStatus = TokenFetchStatus.Fetching;

            var uri =
                string.Format(
                    "https://openapi.baidu.com/oauth/2.0/token?grant_type=client_credentials&client_id={0}&client_secret={1}",
                    APIKey, SecretKey);
            var www = UnityWebRequest.Get(uri);
            yield return www.SendWebRequest();

            if (string.IsNullOrEmpty(www.error))
            {
                Debug.Log("[ASR]" + www.downloadHandler.text);
                var result = JsonUtility.FromJson<TokenResponse>(www.downloadHandler.text);
                Token = result.access_token;
                Debug.Log("[ASR]Token has been fetched successfully");
                tokenFetchStatus = TokenFetchStatus.Success;
            }
            else
            {
                Debug.LogError("[ASR]"+www.error);
                Debug.LogError("[ASR]Token was fetched failed. Please check your APIKey and SecretKey");
                tokenFetchStatus = TokenFetchStatus.Failed;
            }
        }
        
        protected IEnumerator PreAction()
        {
            if (tokenFetchStatus == TokenFetchStatus.NotFetched)
            {
                Debug.Log("[ASR]Token has not been fetched, now fetching...");
                yield return GetAccessToken();
            }

            if (tokenFetchStatus == TokenFetchStatus.Fetching)
            {
                Debug.Log("[ASR]Token is still being fetched, waiting...");
            }

            while (tokenFetchStatus == TokenFetchStatus.Fetching)
            {
                yield return null;
            }
        }

        public IEnumerator Recognize(byte[] data, Action<AsrResponse> callback)
        {
            yield return PreAction ();

            if (tokenFetchStatus == TokenFetchStatus.Failed) {
                Debug.LogError("Token fetched failed, please check your APIKey and SecretKey");
                yield break;
            }

            var uri = string.Format("{0}?dev_pid=1536&cuid={1}&token={2}", UrlAsr, SystemInfo.deviceUniqueIdentifier, Token);

            var form = new WWWForm();
            form.AddBinaryData("audio", data);
            var www = UnityWebRequest.Post(uri, form);
            www.SetRequestHeader("Content-Type", "audio/pcm;rate=16000");
            yield return www.SendWebRequest();

            if (string.IsNullOrEmpty(www.error))
            {
                Debug.Log("[ASR]"+www.downloadHandler.text);
                callback(JsonUtility.FromJson<AsrResponse>(www.downloadHandler.text));
            }
            else
                Debug.LogError(www.error);
        }

        // convert unity's AudioClip to PCM data type
        public static byte[] ConvertAudioClipToPCM16(AudioClip clip)
        {
            var samples = new float[clip.samples * clip.channels];
            clip.GetData(samples, 0);
            var samples_int16 = new short[samples.Length];

            for (var index = 0; index < samples.Length; index++)
            {
                var f = samples[index];
                samples_int16[index] = (short) (f * short.MaxValue);
            }

            var byteArray = new byte[samples_int16.Length * 2];
            Buffer.BlockCopy(samples_int16, 0, byteArray, 0, byteArray.Length);

            return byteArray;
        }
    }
}

