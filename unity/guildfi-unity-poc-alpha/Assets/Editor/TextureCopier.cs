using UnityEngine;
using UnityEditor;
using System.IO;

public class TextureCopier : MonoBehaviour
{
  private const int NUM_COPIES = 500; // Set the number of copies to create
  private const string DIGIT_FORMAT = "D4"; // Set the format for the four digits

  [MenuItem("Tools/GuildFi/Copy Texture Multiple Times")]
  public static void CopyTextureMultipleTimes()
  {
    string texturePath = AssetDatabase.GetAssetPath(Selection.activeObject); // Get the selected texture asset path
    Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath); // Load the selected texture asset
    if (texture != null)
    {
      string textureName = Path.GetFileNameWithoutExtension(texturePath); // Get the name of the texture asset
      string textureExtension = Path.GetExtension(texturePath); // Get the extension of the texture asset
      string textureFolder = Path.GetDirectoryName(texturePath); // Get the folder path of the texture asset
      for (int i = 0; i < NUM_COPIES; i++)
      {
        string digitString = i.ToString(DIGIT_FORMAT); // Convert the index to a string with four digits
        string newTextureName = $"{textureName}_{digitString}"; // Create the new texture asset name
        string newTexturePath = Path.Combine(textureFolder, $"{newTextureName}{textureExtension}"); // Create the new texture asset path
        FileUtil.CopyFileOrDirectory(texturePath, newTexturePath); // Copy the texture asset to the new path
        AssetDatabase.ImportAsset(newTexturePath); // Import the new texture asset to the asset database
      }
    }
  }
}
