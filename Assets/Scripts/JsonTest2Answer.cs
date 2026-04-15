using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

[System.Serializable]
public class ObjectSaveData
{
    public string prefabName;
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;

    public override string ToString()
    {
        return $"{pos} / {rot} / {scale} / {color}";
    }
}

public class JsonTest2Answer : MonoBehaviour
{
    public string fileName = "someClass.json";
    public string FileFullPath => Path.Combine(
                Application.persistentDataPath,
                "JsonTest",
                fileName
            );

    public JsonSerializerSettings jsonSettings;

    public string[] prefabNames =
    {
        "Cube",
        "Sphere",
        "Capsule",
        "Cylinder"
    };

    private void Awake()
    {
        jsonSettings = new JsonSerializerSettings();
        jsonSettings.Formatting = Formatting.Indented;
        jsonSettings.Converters.Add(new Vector3Converter());
        jsonSettings.Converters.Add(new QuaternionConverter());
        jsonSettings.Converters.Add(new ColorConverter());
    }

    private void CreateRandomObject()
    {
        var prefabName = prefabNames[Random.Range(0, prefabNames.Length)];
        var prefab = Resources.Load<JsonTestObject>($"JsonTestObject/{prefabName}");
        var obj = Instantiate(prefab);
        obj.transform.position = Random.insideUnitSphere * 10f;
        obj.transform.rotation = Random.rotation;
        obj.transform.localScale = Vector3.one * Random.Range(3f, 9f);
        obj.GetComponent<Renderer>().material.color = Random.ColorHSV();
    } 

    public void OnCreate()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateRandomObject();
        }
    }

    public void OnClear()
    {
        var objs = GameObject.FindGameObjectsWithTag("TargetObject");
        foreach (var obj in objs)
        {
            Destroy(obj);
        }
    }

    public void OnSave()
    {
        var saveList = new List<ObjectSaveData>();
        var objs = GameObject.FindGameObjectsWithTag("TargetObject");
        
        foreach (var obj in objs)
        {
            var jsonTestObj = obj.GetComponent<JsonTestObject>();
            saveList.Add(jsonTestObj.GetSaveData());
        }

        var json = JsonConvert.SerializeObject(saveList, jsonSettings);
        File.WriteAllText(FileFullPath, json);
    }

    public void OnLoad()
    {
        OnClear();

        var json = File.ReadAllText(FileFullPath);
        var saveList = JsonConvert.DeserializeObject<List<ObjectSaveData>>(json, jsonSettings);
        foreach (var saveData in saveList)
        {
            var prefab = Resources.Load<JsonTestObject>($"JsonTestObject/{saveData.prefabName}");
            var jsonTestObj = Instantiate(prefab);
            jsonTestObj.Set(saveData);
        }
        
    }
}
