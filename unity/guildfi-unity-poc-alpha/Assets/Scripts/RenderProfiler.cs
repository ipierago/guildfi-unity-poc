using System.Collections.Generic;
using System.Text;
using Unity.Profiling;
using UnityEngine;

public class RenderProfiler : MonoBehaviour
{
  string statsText;
  ProfilerRecorder renderUsedTexturesBytes;
  ProfilerRecorder renderUsedTexturesCount;
  ProfilerRecorder renderTrianglesCount;
  ProfilerRecorder renderDrawCallsCount;
  ProfilerRecorder renderBatchesCount;
  ProfilerRecorder renderVerticesCount;
  List<TextureFormat> textureFormats;

  void OnEnable()
  {
    renderUsedTexturesBytes = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Used Textures Bytes");
    renderUsedTexturesCount = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Used Textures Count");
    renderTrianglesCount = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Triangles Count");
    renderDrawCallsCount = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
    renderBatchesCount = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Batches Count");
    renderVerticesCount = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Vertices Count");
  }

  void OnDisable()
  {
    renderUsedTexturesBytes.Dispose();
    renderUsedTexturesCount.Dispose();
    renderTrianglesCount.Dispose();
    renderDrawCallsCount.Dispose();
    renderBatchesCount.Dispose();
    renderVerticesCount.Dispose();
  }

  void Start()
  {
    Texture[] textures = Util.FindTexturesInCurrentScene();
    textureFormats = Util.FindTextureFormats(textures);
    foreach (TextureFormat textureFormat in textureFormats)
    {
      Debug.Log("Texture format found: " + textureFormat.ToString());
    }
  }


  void Update()
  {
    var sb = new StringBuilder(500);
    sb.AppendLine($"Draw Calls Count: {renderDrawCallsCount.LastValue}");
    sb.AppendLine($"Batches Count: {renderBatchesCount.LastValue}");
    sb.AppendLine($"Used Texture Bytes: {renderUsedTexturesBytes.LastValue / (1024)} KB");
    sb.AppendLine($"Used Texture Count: {renderUsedTexturesCount.LastValue}");
    sb.AppendLine($"Triangles Count: {renderTrianglesCount.LastValue}");
    sb.AppendLine($"Vertices Count: {renderVerticesCount.LastValue}");
    sb.AppendLine($"Texture Formats: {string.Join(", ", textureFormats)}");

    statsText = sb.ToString();
  }

  void OnGUI()
  {
    GUI.TextArea(new Rect(10, 30, 250, 150), statsText);
  }
}
