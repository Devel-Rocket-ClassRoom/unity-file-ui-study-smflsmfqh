using UnityEngine;
using TMPro;
/*
 * HW
 * 일괄 변경하는 기능 추가하기 -> 에디팅 중에 (플레이 중이 아닐 때), 모든 언어를 일괄 적용
 * Context menu? property 이용
 */

[ExecuteInEditMode]
public class LocalizationText : MonoBehaviour
{
    // Start해보기 전까지 텍스트가 안보임 -> 불편함
    // 목적: 게임을 스타트하지 않아도 연결된 ui text 갱신해주기, inspector에서 enum 연결
    // 언어는 define에서 만들어놨던 언어 설정과 연결
    // 키보드 연결해서 언어 설정 변경 -> 키보드 누르면 키 바꿔가면서 value text ui 갱신되게
    // 에디터에서 내가 연결한 언어 설정으로 되도록
    // 실행할 때는 define에서 연결한 언어 설정이 찍히는데 실행 중일 때 에디터에서 언어 변경하면 바뀌도록
    // 숫자 키 받아서 연결 이런 식으로
    /*
    [SerializeField]
    private Languages languages;

    private StringTable stringTable;
    private string[] keys = { "HELLO", "I AM", "SKY" };

    public TextMeshProUGUI text;

    private void Awake()
    {
        stringTable = DataTableManager.StringTable;
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetLanguage(Languages.Korean); 
    }

   private void OnValidate()
    {
        ApplyLanguage();
    }
   
    public void OnClickButton()
    {
        foreach (var key in keys)
        {
            string value = GetValue(key);
            Debug.Log($"{key} : {value}");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetLanguage(Languages.Korean);
            Debug.Log($"언어 설정: {Languages.Korean}으로 변경 완료");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetLanguage(Languages.English);
            Debug.Log($"언어 설정: {Languages.English}으로 변경 완료");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) 
        {
            SetLanguage(Languages.Japanese);
            Debug.Log($"언어 설정: {Languages.Japanese}으로 변경 완료");
        }
    }

    private string GetValue(string key)
    {
        string value = stringTable.Get(key);
        return value;
    }

    private void SetLanguage(Languages lang)
    {
        languages = lang;
        ApplyLanguage();
    }

    private void ApplyLanguage()
    {
        if (text == null) return;
        Variables.Language = languages;
        stringTable = DataTableManager.StringTable;
        text.text = languages.ToString();
    }
    */
#if UNITY_EDITOR
    public Languages editorLang;
#endif
    public string id;
    public TextMeshProUGUI text;

    
    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            Variables.OnLanguageChanged += OnChangedLanguage;
            OnChangedId();  

        }
#if UNITY_EDITOR
        else
        {
            OnChangedLanguage(editorLang);
        }
#endif
        OnChangedId();
    }

    private void OnDisable()
    {
        if (Application.isPlaying)
        {
            Variables.OnLanguageChanged -= OnChangedLanguage;
        }

    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        OnChangedLanguage(editorLang);
#endif
    }
    private void OnChangedId()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }

    private void OnChangedLanguage()
    {
        text.text = DataTableManager.StringTable.Get(id);

    }
#if UNITY_EDITOR
    private void OnChangedLanguage(Languages lang)
    {
        var stringTable = DataTableManager.GetStringTable(lang);
        text.text = stringTable.Get(id);
    }
#endif

    [ContextMenu("Apply All Menu")]
    private void ApplyAllLang()
    {
        Variables.OnLanguageChangedEditor += OnChangedLanguage;
        OnChangedId();
    }
    
}
