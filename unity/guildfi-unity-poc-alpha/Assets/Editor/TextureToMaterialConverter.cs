using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class TextureToMaterialConverter : EditorWindow
{
  [MenuItem("Tools/GuildFi/Convert Textures to Materials")]
  public static void ConvertTexturesToMaterials()
  {
    // Get the selected asset folder in the Project window
    string selectedFolderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
    if (!Directory.Exists(selectedFolderPath))
    {
      Debug.LogWarning("Please select a valid folder in the Project window.");
      return;
    }

    // Get all textures in the selected folder and its subfolders
    List<string> texturePaths = new List<string>();
    texturePaths.AddRange(Directory.GetFiles(selectedFolderPath, "*.png", SearchOption.AllDirectories));
    texturePaths.AddRange(Directory.GetFiles(selectedFolderPath, "*.jpg", SearchOption.AllDirectories));

    // Create a new material asset for each texture
    foreach (string texturePath in texturePaths)
    {
      // Load the texture as an asset
      Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath);
      if (texture == null)
      {
        Debug.LogWarning("Failed to load texture at path: " + texturePath);
        continue;
      }

      // Create a new material asset
      string materialPath = Path.GetDirectoryName(texturePath) + "/" + Path.GetFileNameWithoutExtension(texturePath) + ".mat";
      Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
      material.SetTexture("_BaseMap", texture);
      AssetDatabase.CreateAsset(material, materialPath);
    }

    // Refresh the AssetDatabase to see the newly created material assets
    AssetDatabase.Refresh();
  }
}
