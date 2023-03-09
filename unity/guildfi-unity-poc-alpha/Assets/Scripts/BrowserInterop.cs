using UnityEngine;
using System.Runtime.InteropServices;

public class BrowserInterop : MonoBehaviour
{
  [DllImport("__Internal")]
  private static extern void SetModelLoadPercentage(int modelLoadPercentage);

  public void CallSetModelLoadPercentage(int modelLoadPercentage)
  {
    Debug.Log("CallSetModelLoadPercentage: " + modelLoadPercentage);
#if UNITY_WEBGL == true && UNITY_EDITOR == false
    SetModelLoadPercentage (modelLoadPercentage);
#endif
  }

  void Awake()
  {
    DontDestroyOnLoad(gameObject);
  }
}
