/*
 * Add for combine two audio clip
 */
using System;
using System.IO;
using UnityEngine;

public class AudioClipHelper
{
    public static AudioClip Combine(params AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0)
            return null;

        int channels = clips[0].channels;
        int frequency = clips[0].frequency;
        for(int i = 1; i < clips.Length; i++)
        {
            if(clips[i].channels != channels || clips[i].frequency != frequency)
            {
                return null;
            }
        }

        using (MemoryStream memoryStream = new MemoryStream())
        {
            for (int i = 0; i < clips.Length; i++)
            {
                if (clips[i] == null)
                    continue;

                clips[i].LoadAudioData();

                var buffer = clips[i].GetData();

                memoryStream.Write(buffer, 0, buffer.Length);
            }

            var bytes = memoryStream.ToArray();
            var result = AudioClip.Create("Combine", bytes.Length / 4 / channels, channels, frequency, false);
            result.SetData(bytes);

            return result;
        }
    }
}


public static class AudioClipExtension
{
    public static byte[] GetData(this AudioClip clip)
    {
        float[] data = new float[clip.samples * clip.channels];

        clip.GetData(data, 0);

        byte[] bytes = new byte[data.Length * 4];
        Buffer.BlockCopy(data, 0, bytes, 0, bytes.Length);

        return bytes;
    }

    public static void SetData(this AudioClip clip, byte[] bytes)
    {
        float[] data = new float[bytes.Length / 4];
        Buffer.BlockCopy(bytes, 0, data, 0, bytes.Length);

        clip.SetData(data, 0);
    }
}