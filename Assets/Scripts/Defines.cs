using Unity.VisualScripting;

public enum Languages
{
    Korean,
    English,
    Japanese, 
}

public static class Variables
{
    public static event System.Action OnLanguageChanged;
    private static Languages language = Languages.Korean;
    public static Languages Language
    {
        get
        {
            return language;
        }
        set
        {
            if (language == value)
            {
                return;
            }
            language = value;
            // set된 language로 StringTable을 교체하도록 함
            DataTableManager.ChangeLanguage(language); // 실제로 출력되는 텍스트가 변경되는 건 아님
            OnLanguageChanged?.Invoke();
        }
    }
}

    public static class DataTableIds
    {
        public static readonly string[] StringTableIds =
        {
        "StringTableKr3",
        "StringTableEn",
        "StringTableJp"
        };
        public static string String => StringTableIds[(int)Variables.Language];
    }




