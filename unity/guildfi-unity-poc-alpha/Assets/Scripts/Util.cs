using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public static class Util
{

  public static Texture[] FindTexturesInCurrentScene()
  {
    // Get the current scene
    Scene currentScene = SceneManager.GetActiveScene();
    // Get all game objects in the current scene
    GameObject[] rootObjects = currentScene.GetRootGameObjects();

    // Create a list to hold all textures found in the scene
    List<Texture> texturesFound = new List<Texture>();

    // Loop through each game object and its children
    foreach (GameObject obj in rootObjects)
    {
      Component[] components = obj.GetComponentsInChildren<Component>();

      foreach (Component component in components)
      {
        // Check if the component has a texture
        if (component is Renderer)
        {
          Renderer renderer = (Renderer)component;
          foreach (Material material in renderer.materials)
          {
            if (material.mainTexture != null)
            {
              // Add the texture to the list
              texturesFound.Add(material.mainTexture);
            }
          }
        }
        else if (component is SpriteRenderer)
        {
          SpriteRenderer spriteRenderer = (SpriteRenderer)component;
          if (spriteRenderer.sprite != null && spriteRenderer.sprite.texture != null)
          {
            // Add the texture to the list
            texturesFound.Add(spriteRenderer.sprite.texture);
          }
        }
      }
    }

    // Convert the list to an array and return it
    return texturesFound.ToArray();
  }

  public static List<TextureFormat> FindTextureFormats(Texture[] textures)
  {
    // Create a hash set to hold all unique texture formats found
    HashSet<TextureFormat> textureFormatsFound = new HashSet<TextureFormat>();

    // Loop through each texture in the array
    foreach (Texture texture in textures)
    {
      var texture2D = texture as Texture2D;
      if (texture2D != null)
      {
        // Get the texture format and add it to the hash set
        var textureFormat = texture2D.format;
        if (!textureFormatsFound.Contains(textureFormat))
        {
          textureFormatsFound.Add(textureFormat);
        }
      }
    }

    // Convert the hash set to a list and return it
    return new List<TextureFormat>(textureFormatsFound);
  }
}
