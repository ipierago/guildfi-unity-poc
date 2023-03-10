using UnityEngine;
using UnityEditor;
using System.IO;

public class ImportObjCopies : EditorWindow
{
  private const int NUM_COPIES = 500;

  [MenuItem("Tools/GuildFi/Import OBJ Copies")]
  public static void ImportCopies()
  {
    // Get the selected OBJ model asset in the Project window
    GameObject selectedModel = Selection.activeObject as GameObject;
    var objAssetPath = AssetDatabase.GetAssetPath(selectedModel);
    if (selectedModel == null || !objAssetPath.EndsWith(".obj"))
    {
      Debug.LogWarning("Please select a valid OBJ model asset in the Project window.");
      return;
    }

    // Get the folder path of the selected model asset
    string folderPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedModel));

    // Duplicate the model asset multiple times
    for (int i = 0; i < NUM_COPIES; i++)
    {
      // Set the name of the new OBJ file
      string newName = selectedModel.name.Replace(".obj", "") + "_" + i.ToString("D4") + ".obj";

      // Create a new file path for the duplicated OBJ file
      string newPath = folderPath + "/" + newName;

      // Copy the original OBJ file to the new file path
      File.Copy(AssetDatabase.GetAssetPath(selectedModel), newPath);

      // Import the duplicated OBJ file into the Asset Database
      AssetDatabase.ImportAsset(newPath, ImportAssetOptions.ForceUpdate);
    }

    // Refresh the AssetDatabase to see the newly imported model assets
    AssetDatabase.Refresh();
  }
}