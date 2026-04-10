using UnityEngine;
using System.IO;
public class PathCombineExample : MonoBehaviour
{
    private void Start()
    {
        string badPath = Application.persistentDataPath + "/Save/" + "data.txt";
        string goodPath = Path.Combine();
    }
}
