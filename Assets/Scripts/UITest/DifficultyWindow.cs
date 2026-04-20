using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

// apply하면 파일을 저장
// 다시 시작할 때 저장한 난이도로 시작되게

public class DifficultyWindow : GenericWindow
{
    public Toggle[] toggles;
    public Button cancelButton;
    public Button acceptButton;
    private int selected;

    private string saveDirectory = string.Empty;
    private string path = string.Empty;

    private void Awake()
    {
        saveDirectory = $"{Application.persistentDataPath}/SaveDifficulty";
        path = Path.Combine(saveDirectory, "selectedDifficulty");

        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);

        acceptButton.onClick.AddListener(OnAccept);
        cancelButton.onClick.AddListener(OnCancel);

    }
    public override void Open()
    {
        base.Open();

        if (!Directory.Exists(saveDirectory))
        {
            Debug.Log("생성된 디렉토리가 없습니다.");
        }
        if (!File.Exists(Path.Combine(saveDirectory, path)))
        {
            Debug.Log("생성된 파일이 없습니다.");
        }
        else
        {
            LoadDifficulty();
        }

        toggles[selected].isOn = true;
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnEasy(bool active)
    {
        if (active)
        {
            selected = 0;
            Debug.Log("OnEasy");
        }
    }

    public void OnNormal(bool active)
    {
        if (active)
        {
            selected = 1;
            Debug.Log("OnNormal");
        }
    }

    public void OnHard(bool active)
    {
        if (active)
        {
            selected = 2;
            Debug.Log("OnHard");
        }
    }

    public void OnCancel()
    {
        windowManager.Open(0);
    }

    public void OnAccept()
    {
        // 실제 세이브 파일에 난이도를 저장할 때는 세이브 데이터 안에 난이도를 저장하는 다른 파일 만들어서 저장하는 게 좋음
        // 세이브 데이터와 옵션 데이터는 분리해서 관리 
        try
        {
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
                Debug.Log("디렉토리 생성 완료");
            }

            var json = JsonConvert.SerializeObject(selected, Formatting.Indented);

            File.WriteAllText(path, json);
            Debug.Log($"{path}에 {selected} 저장 완료");
        }
        catch
        {
            Debug.LogError("save 실패");
        }

        windowManager.Open(0);

    }

    public void LoadDifficulty()
    {
        string json = File.ReadAllText(path);
        if (json == null)
        {
            Debug.LogError("[Load Error] 저장된 파일이 없습니다.");
            return;
        }
        selected = JsonConvert.DeserializeObject<int>(json);
    }


}
