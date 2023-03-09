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
    if (GUI.Button(new Rect(10, 10, 150, 50), "0"))
    {
      loader.InstantiateAsync(0);
    }
    if (GUI.Button(new Rect(10, 70, 150, 50), "1"))
    {
      loader.InstantiateAsync(1);
    }
    if (GUI.Button(new Rect(10, 130, 150, 50), "2"))
    {
      loader.InstantiateAsync(2);
    }
    if (GUI.Button(new Rect(10, 190, 150, 50), "SetModeLoadPercentage"))
    {
      ++modelLoadPercentage;
      browserInterop.CallSetModelLoadPercentage(modelLoadPercentage);
    }
  }
#endif
}
