using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
  public Loader loader;

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
  }
}
