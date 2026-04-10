using UnityEngine;

public class FilePathExample : MonoBehaviour
{
    private void Start()
    {
        Debug.Log($"persistentDataPath: {Application.persistentDataPath}");
        Debug.Log($"dataPath: {Application.dataPath}"); 
        Debug.Log($"streamingAssetsPath: {Application.streamingAssetsPath}");
    }
}
