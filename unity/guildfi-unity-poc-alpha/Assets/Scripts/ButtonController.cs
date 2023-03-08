using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
  public Loader loader;

  private void OnGUI()
  {
    // Create a button for each message we want to log
    if (GUI.Button(new Rect(10, 10, 150, 50), "0"))
    {
      loader.InstantiateAsync(0);
    }
  }
}
