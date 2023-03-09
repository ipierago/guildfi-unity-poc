using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Loader : MonoBehaviour
{
  public AssetReference[] assetReferences;
  public BrowserInterop browserInterop;

  private AsyncOperationHandle<GameObject> asyncOpHandle;

  public async Task InstantiateAsync(int index)
  {
    if (asyncOpHandle.IsValid())
    {
      Addressables.ReleaseInstance(asyncOpHandle);
    }

    if (index < assetReferences.Length)
    {
      asyncOpHandle = assetReferences[index].InstantiateAsync(transform);
      asyncOpHandle.Completed += op =>
      {
        if (op.Status == AsyncOperationStatus.Succeeded)
        {
          Debug.Log("Successfully loaded and instantiated object.");
          browserInterop.CallSetModelLoadPercentage(100);
        }
        else
        {
          Debug.LogError("Failed to load and instantiate object.");
          browserInterop.CallSetModelLoadPercentage(-1);

        }
      };

      while (!asyncOpHandle.IsDone)
      {
        float downloadPercentage = asyncOpHandle.PercentComplete * 100f;
        Debug.Log($"Downloading: {downloadPercentage:F1}%");
        browserInterop.CallSetModelLoadPercentage((int)downloadPercentage);
        await Task.Yield();
      }

    }
  }

  void OnDestroy()
  {
    if (asyncOpHandle.IsValid())
    {
      Addressables.ReleaseInstance(asyncOpHandle);
    }

  }
}
