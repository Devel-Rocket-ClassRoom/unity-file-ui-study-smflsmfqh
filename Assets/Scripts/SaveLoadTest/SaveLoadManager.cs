using UnityEngine;
using SaveDataVC = SaveDataV3;
using Newtonsoft.Json;
using System.IO;

public static class SaveLoadManager
{
    public enum SaveMode
    {
        Text,               // JSON 텍스트(.json) - 개발용
        Encrypted,          // AES 암호화 바이너리(.dat) - 릴리즈용
    }

    public static SaveMode Mode {  get; set; } = SaveMode.Encrypted;

    private static readonly string SaveDirectory = $"{Application.persistentDataPath}/Save";

    private static readonly string[] SaveFileNames =
    {
        "SaveAuto.json",
        "Save1.json",
        "Save2.json",
        "Save3.json",
    };

    public static int SaveDataVersion { get; } = 3;

    public static SaveDataVC Data { get; set; } = new SaveDataVC();

    private static string GetSaveFilePath(int slot = 0)
    {
        return GetSaveFilePath(slot, Mode);
    }

    private static string GetSaveFilePath(int slot, SaveMode mode)
    {
        var ext = mode == SaveMode.Text ? ".json" : ".dat";
        return Path.Combine(SaveDirectory, $"{SaveFileNames[slot]}{ext}");
    }

    private static JsonSerializerSettings settings = new JsonSerializerSettings()
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All,
    };

    public static bool Save(int slot = 0)
    {
        return Save(slot, Mode);
    }

    public static bool Save(int slot, SaveMode mode)
    {
        if (Data == null || slot < 0 || slot >= SaveFileNames.Length) return false;

        try
        {
            if (!Directory.Exists(SaveDirectory))
                Directory.CreateDirectory(SaveDirectory);

            var json = JsonConvert.SerializeObject(Data, settings);
            string path = GetSaveFilePath(slot, mode);

            switch (mode)
            {
                case SaveMode.Text:
                    File.WriteAllText(path, json);
                    break;
                case SaveMode.Encrypted:
                    File.WriteAllBytes(path, CryptoUtil.Encrypt(json));
                    Debug.Log("세이브 파일 암호화 완료");
                    break;
            }
            
            return true;
        }
        catch
        {
            Debug.LogError("Save 예외");
            return false;
        }
    }

    public static bool Load(int slot = 0)
    {
        return Load(slot, Mode);
    }


    public static bool Load(int slot, SaveMode mode)
    {
        string path = GetSaveFilePath(slot, mode);

        if (!File.Exists(path)) return false;

        try
        {
            string json = string.Empty;
            switch (mode)
            {
                case SaveMode.Text:
                    json = File.ReadAllText(path);
                    break;
                case SaveMode.Encrypted:
                    json = CryptoUtil.Decrypt(File.ReadAllBytes(path));
                    Debug.Log("세이브 파일 복호화 완료");
                    break;
            }
            
            var saveData = JsonConvert.DeserializeObject<SaveData>(json, settings);

            // V1→V2→V3 자동 체인 마이그레이션
            while (saveData.Version < SaveDataVersion)
            {
                Debug.Log($"마이그레이션: V{saveData.Version} → V{saveData.Version + 1}");
                saveData = saveData.VersionUp();
            }

            Data = saveData as SaveDataVC;
            return true;
        }
        catch
        {
            Debug.LogError("Load 예외");
            return false;
        }
    }
}