using UnityEngine;

public class Console : MonoBehaviour
{
  private void OnGUI()
  {
    // Create a button for each message we want to log
    if (GUI.Button(new Rect(10, 10, 150, 50), "Error"))
    {
      Debug.LogError("Error clicked");
    }
    if (GUI.Button(new Rect(10, 70, 150, 50), "Warning"))
    {
      Debug.LogWarning("Warning clicked");
    }
    if (GUI.Button(new Rect(10, 130, 150, 50), "Log"))
    {
      Debug.Log("Log clicked");
    }
  }
}
