using Unity.VisualScripting;

public enum Languages
{
    Korean,
    English,
    Japanese, 
}

public static class Variables
{
    public static Languages Language = Languages.Korean;
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




