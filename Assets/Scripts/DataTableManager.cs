using UnityEngine;
using System.Collections.Generic;
public static class DataTableManager
{
    // 분기해서 처리
    // 에디터에서 로드할 때
    // 플레이에서 로드할 때
    private static readonly Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();

    public static StringTable StringTable => Get<StringTable>(DataTableIds.String);

#if UNITY_EDITOR
    public static StringTable GetStringTable(Languages language)
    {
        return Get<StringTable>(DataTableIds.StringTableIds[(int)language]);
    }
#endif

    static DataTableManager()
    {
        Init();
    }

    private static void Init()
    {
#if !UNITY_EDITOR // 에디터에서 실행할 때는 됨, 런타임 때는 X, 컴파일 전에 define에 맞춰서 코드 자체를 수정하는 기능
        var stringTable = new StringTable();
        stringTable.Load(DataTableIds.String);
        tables.Add(DataTableIds.String, stringTable);
#else
        foreach (var id in DataTableIds.StringTableIds)
        {
            var stringTable = new StringTable();
            stringTable.Load(id);
            tables.Add(id, stringTable);
        }
#endif
    }

    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Init();
        }
        return tables[id] as T; 
    }
}
