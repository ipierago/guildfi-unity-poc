using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Loader : MonoBehaviour
{
  public AssetReference[] assetReferences;
  private AsyncOperationHandle<GameObject> asyncOpHandle;
  public void InstantiateAsync(int index)
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
        }
        else
        {
          Debug.LogError("Failed to load and instantiate object.");
        }
      };
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
