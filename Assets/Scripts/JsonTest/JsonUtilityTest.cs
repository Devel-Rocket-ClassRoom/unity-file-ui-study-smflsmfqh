using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerInfo
{
    public string playerName;
    public int lives;
    public float health;
    public Vector3 position;

    // Unity Json 유틸리티는 딕셔너리 지원을 안해줌 -> json에 저장 안됨
    // 최상위로 배열 및 리스트 [] 지원 안됨
    public Dictionary<string, int> scores = new Dictionary<string, int>
    {
        {"stage1", 100},
        {"stage2", 200},   
    };
}

public class JsonUtilityTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Save, 직렬화
            var obj = new PlayerInfo
            {
                playerName = "ABC",
                lives = 10,
                health = 10.999f,
                position = new Vector3(1f, 2f, 3f),  
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
                "player.json"
            );

            string json = JsonUtility.ToJson(obj, prettyPrint: true); // prettyPrint: 줄바꿈 여부
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
                "player.json"
            );
            
            string json = File.ReadAllText(path);
            //PlayerInfo obj = JsonUtility.FromJson<PlayerInfo>(json); // 새로운 객체를 생성할 때 json 데이터로 필드를 생성함
            PlayerInfo obj = new PlayerInfo();
            JsonUtility.FromJsonOverwrite(json, obj); // 이미 생성된 객체에 필드에 json 데이터를 덮어씀
            Debug.Log(json);
            Debug.Log($"{obj.playerName} / {obj.health}");

        }
    }
}
