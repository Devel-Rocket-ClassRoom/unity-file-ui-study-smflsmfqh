using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class SomeClass
{
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;

    public override string ToString()
    {
        return $"{pos} / {rot} / {scale} / {color}";
    }
}

public class JsonTest2 : MonoBehaviour
{
    public string fileName = "someClass.json";
    public string FileFullPath => Path.Combine(
                Application.persistentDataPath,
                "JsonTest",
                fileName
            );

    public JsonSerializerSettings jsonSettings;
    public GameObject target;

    private PrimitiveType[] primitives;
    private List<GameObject> createdObjects;
    private bool isClear;

    [SerializeField]
    private int count = 10;

    private void Awake()
    {
        jsonSettings = new JsonSerializerSettings();
        jsonSettings.Formatting = Formatting.Indented;
        jsonSettings.Converters.Add(new Vector3Converter());
        jsonSettings.Converters.Add(new QuaternionConverter());
        jsonSettings.Converters.Add(new ColorConverter());
        primitives = new PrimitiveType[] { PrimitiveType.Capsule, PrimitiveType.Cube, PrimitiveType.Cylinder, PrimitiveType.Quad, PrimitiveType.Sphere };
        createdObjects = new List<GameObject>(count);
        isClear = false;
    }
    
    public void OnClickClear()
    {
        Clear();
    }

    public void OnClickCreate()
    {
        Create();
    }

    public void OnClickSave()
    {
        if (target == null)
        {
            Debug.LogError("타겟이 없음");
            return;
        }

        Save();
        Debug.Log("타겟 정보 저장됨");
    }

    public void OnClickLoad()
    {
        Load();
        Debug.Log("타겟 정보 로드됨");
    }

    public void Clear()
    {
        for (int i = 0; i < createdObjects.Count; i++)
        {
            createdObjects[i].SetActive(false);
        }
        isClear = true;
    }

    public void Create()
    {
        for (int i = 0; i < count; i ++)
        {
            float randomZ = Random.Range(330f, 400f);
            float randomX = Random.Range(150f, 330f);
            float randomY = Random.Range(50f, 150f);

            float randomSize = Random.Range(5f, 10f);

            //float step = 10f;
            float quatX = Random.Range(0f, 360f);
            float quaty = Random.Range(0f, 360f);
            float quatz = Random.Range(0f, 360f);

            Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            var primRandom = Random.Range(0, primitives.Length);
            GameObject gameObject = GameObject.CreatePrimitive(primitives[primRandom]);
            gameObject.transform.position = new Vector3(randomX, randomY, randomZ);
            gameObject.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
            gameObject.transform.rotation = Quaternion.Euler(quatX, quaty, quatz);
            gameObject.GetComponent<Renderer>().material.color = randomColor;

            createdObjects.Add(gameObject);
        }
    }

    public void Save()
    {
        if (isClear)
        {
            for (int i = 0; i < createdObjects.Count; i++)
            {
                Destroy(createdObjects[i]);
            }
            createdObjects.Clear();
        }
    
        List<SomeClass> dataList = new List<SomeClass>(createdObjects.Count);

        for (int i = 0; i < createdObjects.Count; i++)
        {
            SomeClass obj = new SomeClass();
            obj.pos = createdObjects[i].transform.position;
            obj.rot = createdObjects[i].transform.rotation;
            obj.scale = createdObjects[i].transform.localScale;
            obj.color = createdObjects[i].GetComponent<Renderer>().material.color;

            dataList.Add(obj);
        }

        var json = JsonConvert.SerializeObject(dataList, jsonSettings);
        File.WriteAllText(FileFullPath, json);
        Debug.Log(json);
    }

    public void Load()
    {
        var json = File.ReadAllText(FileFullPath);
        List<SomeClass> obj = JsonConvert.DeserializeObject<List<SomeClass>>(json, jsonSettings);

        if (obj == null)
        {
            Debug.LogError("로드할 데이터가 없습니다.");
            return;
        }

        if (createdObjects.Count > obj.Count)
        {
            for (int i = obj.Count; i < createdObjects.Count; i++)
            {
                Destroy(createdObjects[i]);
            }
            createdObjects.RemoveRange(obj.Count, createdObjects.Count-obj.Count);
        }

        for (int i = 0; i < obj.Count; i++)
        {
            createdObjects[i].transform.position = obj[i].pos;
            createdObjects[i].transform.rotation = obj[i].rot;
            createdObjects[i].transform.localScale = obj[i].scale;
            createdObjects[i].GetComponent<Renderer>().material.color = obj[i].color;

            if (isClear)
            {
                createdObjects[i].SetActive(true);
            }   
            Debug.Log(obj[i]);
        }

        if (isClear) { isClear = false; }
    }
}
