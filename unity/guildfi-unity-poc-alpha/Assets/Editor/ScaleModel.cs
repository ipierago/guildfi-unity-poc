using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;

public class ScaleModel : EditorWindow
{
  private const float MAX_SIZE = 1f;

  [MenuItem("Tools/GuildFi/Scale Model")]
  public static void ScaleSelectedModel()
  {
    // Get the selected OBJ model file
    string selectedModelPath = AssetDatabase.GetAssetPath(Selection.activeObject);
    if (!selectedModelPath.ToLower().EndsWith(".obj"))
    {
      Debug.LogWarning("Please select a valid OBJ model file in the Project window.");
      return;
    }

    // Load the OBJ model as a mesh
    Mesh mesh = LoadObjFile(selectedModelPath);

    // Scale the mesh so that the bounds are no larger than one unit
    ScaleMesh(mesh);

    // Save the new OBJ file with "_scaled" appended to the original file name
    string newModelPath = Path.Combine(Path.GetDirectoryName(selectedModelPath), Path.GetFileNameWithoutExtension(selectedModelPath) + "_scaled.obj");
    SaveObjFile(newModelPath, mesh);

    // Destroy the original mesh
    DestroyImmediate(mesh);

    // Refresh the Project window to show the new OBJ file
    AssetDatabase.Refresh();
  }

  private static void ScaleMesh(Mesh mesh)
  {
    // Get the bounds of the mesh
    Bounds bounds = mesh.bounds;

    // Calculate the scale factor to make the bounds no larger than one unit
    float scaleFactor = MAX_SIZE / Mathf.Max(Mathf.Max(bounds.size.x, bounds.size.y), bounds.size.z);

    // Scale the mesh
    var vertices = mesh.vertices;
    for (int i = 0; i < vertices.Length; i++)
    {
      var v1 = vertices[i];
      var v2 = v1 * scaleFactor;
      vertices[i] = v2;
    }
    mesh.vertices = vertices;
  }

  private static Mesh LoadObjFile(string objFilePath)
  {
    Mesh mesh = new Mesh();
    mesh.name = Path.GetFileNameWithoutExtension(objFilePath);

    StreamReader objReader = new StreamReader(objFilePath);
    string objContents = objReader.ReadToEnd();
    objReader.Close();

    string[] lines = Regex.Split(objContents, "\r\n|\r|\n");

    int numVertices = 0;
    int numFaces = 0;
    int numNormals = 0;
    int numUVs = 0;

    for (int i = 0; i < lines.Length; i++)
    {
      string line = lines[i].Trim();

      if (line.StartsWith("v "))
      {
        numVertices++;
      }
      else if (line.StartsWith("f "))
      {
        numFaces++;
      }
      else if (line.StartsWith("vn "))
      {
        numNormals++;
      }
      else if (line.StartsWith("vt "))
      {
        numUVs++;
      }
    }

    Vector3[] vertices = new Vector3[numVertices];
    Vector3[] normals = new Vector3[numNormals];
    Vector2[] uv = new Vector2[numUVs];
    int[] triangles = new int[numFaces * 3];

    int vertexIndex = 0;
    int normalIndex = 0;
    int uvIndex = 0;
    int triangleIndex = 0;

    for (int i = 0; i < lines.Length; i++)
    {
      string line = lines[i].Trim();

      if (line.StartsWith("v "))
      {
        string[] vertexComponents = line.Split(' ');
        float x = float.Parse(vertexComponents[1]);
        float y = float.Parse(vertexComponents[2]);
        float z = float.Parse(vertexComponents[3]);
        vertices[vertexIndex] = new Vector3(x, y, z);
        vertexIndex++;
      }
      else if (line.StartsWith("vn "))
      {
        string[] normalComponents = line.Split(' ');
        float x = float.Parse(normalComponents[1]);
        float y = float.Parse(normalComponents[2]);
        float z = float.Parse(normalComponents[3]);
        normals[normalIndex] = new Vector3(x, y, z);
        normalIndex++;
      }
      else if (line.StartsWith("vt "))
      {
        string[] uvComponents = line.Split(' ');
        float u = float.Parse(uvComponents[1]);
        float v = float.Parse(uvComponents[2]);
        uv[uvIndex] = new Vector2(u, v);
        uvIndex++;
      }
      else if (line.StartsWith("f "))
      {
        string[] faceComponents = line.Split(' ');
        int v1 = int.Parse(faceComponents[1].Split('/')[0]) - 1;
        int v2 = int.Parse(faceComponents[2].Split('/')[0]) - 1;
        int v3 = int.Parse(faceComponents[3].Split('/')[0]) - 1;
        triangles[triangleIndex] = v1;
        triangles[triangleIndex + 1] = v2;
        triangles[triangleIndex + 2] = v3;
        triangleIndex += 3;
      }
    }

    mesh.vertices = vertices;
    mesh.normals = normals;
    mesh.uv = uv;
    mesh.triangles = triangles;

    return mesh;
  }

  private static void SaveObjFile(string objFilePath, Mesh mesh)
  {
    StreamWriter objWriter = new StreamWriter(objFilePath);

    for (int i = 0; i < mesh.vertices.Length; i++)
    {
      Vector3 vertex = mesh.vertices[i];
      objWriter.WriteLine("v " + vertex.x.ToString("F6") + " " + vertex.y.ToString("F6") + " " + vertex.z.ToString("F6"));
    }

    for (int i = 0; i < mesh.normals.Length; i++)
    {
      Vector3 normal = mesh.normals[i];
      if (normal != Vector3.zero)
      {
        objWriter.WriteLine("vn " + normal.x.ToString("F6") + " " + normal.y.ToString("F6") + " " + normal.z.ToString("F6"));
      }
    }

    for (int i = 0; i < mesh.uv.Length; i++)
    {
      Vector2 uv = mesh.uv[i];
      objWriter.WriteLine("vt " + uv.x.ToString("F6") + " " + uv.y.ToString("F6"));
    }

    for (int i = 0; i < mesh.triangles.Length; i += 3)
    {
      int v1 = mesh.triangles[i] + 1;
      int v2 = mesh.triangles[i + 1] + 1;
      int v3 = mesh.triangles[i + 2] + 1;
      objWriter.WriteLine("f " + v1 + "/" + v1 + "/" + v1 + " " + v2 + "/" + v2 + "/" + v2 + " " + v3 + "/" + v3 + "/" + v3);
    }

    objWriter.Close();
  }




}

