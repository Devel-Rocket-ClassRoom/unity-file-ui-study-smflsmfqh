using System.Collections.Generic;

[System.Serializable]
public abstract class SaveData
{
    public int Version { get; protected set; } // Version
    public abstract SaveData VersionUp();
}

[System.Serializable]
public class SaveDataV1 : SaveData
{
    public string PlayerName { get; set; } = string.Empty;

    public SaveDataV1() { Version = 1; }

    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV2();
        saveData.Name = PlayerName;
        return saveData;
    }
}

[System.Serializable]
public class SaveDataV2 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold = 0;

    public SaveDataV2() { Version = 2; }

    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV3();
        saveData.Name = Name;
        saveData.Gold = Gold;
        // 마이그레이션 시 아이템은 빈 리스트로 시작
        return saveData;
    }
}

[System.Serializable]
public class SaveDataV3 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold = 0;
    public List<string> ItemIds = new List<string>();  // ItemTable의 ItemId만 저장

    // saveItemData 클래스를 새로 만들어서, 멤버로 어떤 아이템 테이블과 연결될지 
    // 공유, 아이템들을 구분할 수 있는 인스턴스 아이디, 아이템을 최초로 획득한 시간도 데이터에 포함
    // 아이템 슬롯부터 한 개 한 개 올라가는 형태  
    public SaveDataV3() { Version = 3; }

    public override SaveData VersionUp()
    {
        SaveDataV4 data = new SaveDataV4();
        data.Name = Name;
        data.Gold = Gold;
        foreach (var id in ItemIds)
        {
            SaveItemData itemData = new SaveItemData();
            itemData.ItemData = DataTableManager.ItemTable.Get(id);
            data.ItemList.Add(itemData);
        }
        return data;
    }
}

// sorting 옵션이랑 필터링 옵션 추가

[System.Serializable]
public class SaveDataV4 : SaveDataV2
{
    public List<SaveItemData> ItemList = new List<SaveItemData>();  // ItemTable의 ItemId만 저장
    public List<SaveCharData> CharList = new List<SaveCharData>();
    public SortingOptions ItemSorting = SortingOptions.NameAscending;
    public FilteringOptions ItemFiltering = FilteringOptions.None;
    public SortingOptions CharSorting = SortingOptions.NameAscending;
    public FilteringOptions charFiltering = FilteringOptions.None;

    // saveItemData 클래스를 새로 만들어서, 멤버로 어떤 아이템 테이블과 연결될지 
    // 공유, 아이템들을 구분할 수 있는 인스턴스 아이디, 아이템을 최초로 획득한 시간도 데이터에 포함
    // 아이템 슬롯부터 한 개 한 개 올라가는 형태  
    public SaveDataV4() { Version = 4; }

    public override SaveData VersionUp()
    {
        throw new System.NotImplementedException();
    }
}

