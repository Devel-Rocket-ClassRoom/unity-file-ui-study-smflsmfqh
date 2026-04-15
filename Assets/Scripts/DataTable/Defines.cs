public enum Languages
{
    Korean,
    English,
    Japanese, 
}

public enum ItemTypes
{
    Weapon,
    Equip,
    Consumable,
}

public enum CharacterTypes
{
    Warrior,
    Magician,
    Elf,
    Healer,
}

public static class Variables
{
    public static event System.Action OnLanguageChanged;
    public static event System.Action<Languages> OnLanguageChangedEditor;

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
        "StringTableKr",
        "StringTableEn",
        "StringTableJp"
    };
    public static string String => StringTableIds[(int)Variables.Language];
    public static string Item => "ItemTable";
    public static string Character => "CharacterTable";
}




