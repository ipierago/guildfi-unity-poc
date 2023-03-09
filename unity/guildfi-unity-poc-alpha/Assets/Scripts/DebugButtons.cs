using UnityEngine;
using UnityEngine.UI;
#pragma warning disable 4014

public class DebugButtons : MonoBehaviour
{
  public Loader loader;
  public BrowserInterop browserInterop;

  private int modelLoadPercentage = 0;

#if UNITY_EDITOR == true
  private void OnGUI()
  {
    const int h = 50;
    const int g = 10;
    const int w = 150;

    int y = g;
    for (var i = 0; i < loader.assetReferences.Length; ++i)
    {
      if (GUI.Button(new Rect(g, y, w, h), $"Model {i}"))
      {
        loader.InstantiateAsync(i);
      }
      y += h + g;
    }
  }
#endif
}
