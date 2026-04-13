using UnityEngine;

public class DataTableTest : MonoBehaviour
{
    private StringTable stringTable;

    public string NameStringTableKr = "StringTableKr3";
    public string NameStringTableEn = "StringTableEn";
    public string NameStringTableJp = "StringTableJp";

    private string[] keys = { "HELLO", "I AM", "SKY" };

    private void Awake()
    {
        stringTable = new StringTable();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Variables.Language = Languages.Korean;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Variables.Language = Languages.English;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Variables.Language = Languages.Japanese;
        }
    }

    public void OnClickButtonStringTableKr()
    {
        foreach (string key in keys)
        {
            var table = DataTableManager.StringTable.Get(key);
            Debug.Log($"{key} : {table}");
        }
    }

    public void OnClickButtonStringTableEn()
    {
        foreach (string key in keys)
        {
            var table = DataTableManager.StringTable.Get(key);
            Debug.Log($"{key} : {table}");
        }
    }

    public void OnClickButtonStringTableJp()
    {
        foreach (string key in keys)
        {
            var table = DataTableManager.StringTable.Get(key);
            Debug.Log($"{key} : {table}");
        }
    }

    
}
