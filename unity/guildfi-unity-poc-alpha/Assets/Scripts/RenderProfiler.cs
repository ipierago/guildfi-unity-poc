using System.Collections.Generic;
using System.Text;
using Unity.Profiling;
using UnityEngine;

public class RenderProfiler : MonoBehaviour
{
  string statsText;
  ProfilerRecorder renderUsedTextureBytes;
  ProfilerRecorder renderUsedTexturesCount;

  static double GetRecorderFrameAverage(ProfilerRecorder recorder)
  {
    var samplesCount = recorder.Capacity;
    if (samplesCount == 0)
      return 0;

    double r = 0;
    unsafe
    {
      var samples = stackalloc ProfilerRecorderSample[samplesCount];
      recorder.CopyTo(samples, samplesCount);
      for (var i = 0; i < samplesCount; ++i)
        r += samples[i].Value;
      r /= samplesCount;
    }

    return r;
  }

  void OnEnable()
  {
    renderUsedTextureBytes = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Used Texture Bytes");
    renderUsedTexturesCount = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Used Texture Count");
  }

  void OnDisable()
  {
    renderUsedTextureBytes.Dispose();
    renderUsedTexturesCount.Dispose();
  }

  void Update()
  {
    var sb = new StringBuilder(500);
    sb.AppendLine($"Render Used Texture Bytes: {renderUsedTextureBytes.LastValue / (1024 * 1024)} MB");
    sb.AppendLine($"Render Used Texture Count: {renderUsedTexturesCount.LastValue}");
    statsText = sb.ToString();
  }

  void OnGUI()
  {
    GUI.TextArea(new Rect(10, 30, 250, 100), statsText);
  }
}
