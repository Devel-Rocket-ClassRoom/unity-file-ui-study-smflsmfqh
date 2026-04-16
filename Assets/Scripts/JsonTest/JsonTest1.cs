using UnityEngine;
using System;
using Newtonsoft.Json;
using System.IO;


// 이 속성을 들고 있어야 직렬화 됨
[Serializable]

public class PlayerState
{
    public string playerName;
    public int lives;
    public float health;

    // --- JsonConverter를 활용한 직렬화 및 역직렬화 두번째 방법 ---
    // 주로 이 방법 사용을 추천
    // JsonConverter 필드 선언 - converter 객체 타입은 typeof의 인자로 넘겨줘야 함
    //[JsonConverter(typeof(Vector3Converter))]

    // 그냥 직렬화하면 Self referencing loop 에러남
    // => [JsonConverter] 사용해서 커스텀 직렬화 수행
    public Vector3 position;

    public override string ToString()
    {
        return $"{playerName} / {lives} / {health} / {position}";
    }
}

public class JsonTest1 : MonoBehaviour
{
    // --- JsonConverter를 활용한 직렬화 및 역직렬화 세번째 방법 ---
    private JsonSerializerSettings jsonSetting;

    private void Awake()
    {
        jsonSetting = new JsonSerializerSettings();
        jsonSetting.Formatting = Formatting.Indented;
        jsonSetting.Converters.Add(new Vector3Converter());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Save, 직렬화
            var obj = new PlayerState
            {
                playerName = "ABC",
                lives = 10,
                health = 10.999f, 
            };
            string pathFolder = Path.Combine(
                Application.persistentDataPath,
                "JsonTest"
            );

            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }

            string path = Path.Combine(
                pathFolder,
                "player2.json"
            );
            // --- JsonConverter 이용한 직렬화 첫번째 방법 ---
            //string json = JsonConvert.SerializeObject(obj, Formatting.Indented, new Vector3Converter());
            string json = JsonConvert.SerializeObject(
                obj, jsonSetting
            );
            File.WriteAllText(path, json);

            Debug.Log(path);
            Debug.Log(json);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Load, 역직렬화
            string path = Path.Combine(
                Application.persistentDataPath,
                "JsonTest",
                "player2.json"
            );
            
            string json = File.ReadAllText(path);

            // --- JsonConverter 이용한 역직렬화 첫번째 방법 ---
            //PlayerState obj = JsonConvert.DeserializeObject<PlayerState>(json, new Vector3Converter());
            
            PlayerState obj = JsonConvert.DeserializeObject<PlayerState>(
                json,
                jsonSetting
                );

            Debug.Log(json);
            Debug.Log(obj);

        }
    }
}
