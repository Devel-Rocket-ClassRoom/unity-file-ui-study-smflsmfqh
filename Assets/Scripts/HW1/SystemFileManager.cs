using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SystemFileManager : MonoBehaviour
{
    private string path;
    private Dictionary<string, string> settingsFile;

    private void Awake()
    {
        settingsFile = new Dictionary<string, string>();
    }
    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "UserData");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        else
        {
            Directory.Delete(path, true);
            Directory.CreateDirectory(path);
        }
        
    }

    public void OnClickButtonSettingExample()
    {
        string fileName = "settings.cfg";

        CreateFile(path, fileName);
        LoadFile(path, fileName);

        Debug.Log("--- 변경 전 ---");
        string key1 = "bgm_volume";
        string key2 = "language";
        foreach(var pair in settingsFile)
        {
            string content = $"{pair.Key}={pair.Value}";

            if (pair.Key == key1)
            {
                Debug.Log(content);
            }
            if (pair.Key == key2)
            {
                Debug.Log(content);
            }
        }

        string content1 = "50";
        string content2 = "en";

        ChangeTheValue(key1, content1);
        ChangeTheValue(key2, content2);
        Debug.Log("--- 변경 후 저장 ---");
        WriteFile(path, fileName, settingsFile);

        foreach(var pair in settingsFile)
        {
            string content = $"{pair.Key}={pair.Value}";
            if (pair.Key == key1)
            {
                Debug.Log(content);
            }
            if (pair.Key == key2)
            {
                Debug.Log(content);
            }
        }
        Debug.Log("--- 최종 파일 내용 ---");
        Debug.Log(ReadAllFileContent(path, fileName));
    }

    private void CreateFile(string path, string fileName)
    {
        using (FileStream fs = File.Create(Path.Combine(path, fileName)))
        using (StreamWriter sw = new StreamWriter(fs))
        {
            sw.WriteLine("master_volume=80");
            sw.WriteLine("bgm_volume=70");
            sw.WriteLine("sfx_volume=90");
            sw.WriteLine("language=kr");
            sw.WriteLine("show_damage=true");
        }
        Debug.Log($"{fileName} 파일 생성 완료");
    }

    private void LoadFile(string path, string fileName)
    {
        using (StreamReader sr = File.OpenText(Path.Combine(path, fileName)))
        {
            string readLine;
            while ((readLine = sr.ReadLine()) != null)
            {
                string[] parts = readLine.Split("=");
                settingsFile[parts[0]] = parts[1];
            }
        }
        Debug.Log($"설정 로드 완료 (항목 {settingsFile.Count}개)");
    }

    private void ChangeTheValue(string key, string value)
    {
        settingsFile[key] = value;
    }

    private void WriteFile(string path, string fileName, Dictionary<string, string> contents)
    {
        using (StreamWriter sw = File.CreateText(Path.Combine(path, fileName)))
        {
            foreach (var c in contents)
            {
                sw.WriteLine($"{c.Key}={c.Value}");
            }
        }
    }

    private string ReadAllFileContent(string path, string fileName)
    {
        string entireContent;
        using (StreamReader sr = File.OpenText(Path.Combine(path, fileName)))
        {
            entireContent = sr.ReadToEnd();
        }
        return entireContent;
    }
}
