using UnityEngine;
using UnityEditor;
using System.IO;

public class InstantiateModels : EditorWindow
{
  private const float MIN_POSITION = -4f;
  private const float MAX_POSITION = 4f;

  [MenuItem("Tools/GuildFi/Instantiate Models")]
  public static void InstantiateModelsAndMaterials()
  {
    // Get the selected folder in the Project window
    string selectedFolderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
    if (!Directory.Exists(selectedFolderPath))
    {
      Debug.LogWarning("Please select a valid folder in the Project window.");
      return;
    }

    // Get all the model assets in the selected folder
    string[] modelPaths = Directory.GetFiles(selectedFolderPath, "*.obj");
    if (modelPaths.Length == 0)
    {
      Debug.LogWarning("No model assets found in the selected folder.");
      return;
    }

    // Get all the material assets in the selected folder
    string[] materialPaths = Directory.GetFiles(selectedFolderPath, "*.mat");
    if (materialPaths.Length == 0)
    {
      Debug.LogWarning("No material assets found in the selected folder.");
      return;
    }

    var rootGameObject = new GameObject("root");

    // Instantiate game objects for each pair of models and materials with the same name
    for (int i = 0; i < modelPaths.Length; i++)
    {
      string modelName = Path.GetFileNameWithoutExtension(modelPaths[i]);
      for (int j = 0; j < materialPaths.Length; j++)
      {
        string materialName = Path.GetFileNameWithoutExtension(materialPaths[j]);
        if (modelName == materialName)
        {
          var mesh = AssetDatabase.LoadAssetAtPath<Mesh>(modelPaths[i]);
          var material = AssetDatabase.LoadAssetAtPath<Material>(materialPaths[j]);
          var go = new GameObject();
          go.name = modelName;
          var meshFilter = go.AddComponent<MeshFilter>();
          meshFilter.sharedMesh = mesh;
          var meshRenderer = go.AddComponent<MeshRenderer>();
          meshRenderer.sharedMaterial = material;
          go.transform.parent = rootGameObject.transform;
          var position = new Vector3(Random.Range(MIN_POSITION, MAX_POSITION), Random.Range(MIN_POSITION, MAX_POSITION), Random.Range(MIN_POSITION, MAX_POSITION));
          go.transform.position = position;

        }
      }
    }
  }
}
