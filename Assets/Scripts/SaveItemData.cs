using System;
using Newtonsoft.Json;

[Serializable]
public class SaveItemData 
{
    public Guid InstanceId { get; set; }

    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData ItemData; // 아이템 데이터는 해당 필드의 데이터 정보를 가지고 있게 구조 설계
    public DateTime creationIime { get; set; }

    public static SaveItemData GetRandomItem()
    {
        SaveItemData newItem = new SaveItemData();
        newItem.ItemData = DataTableManager.ItemTable.GetRandom();
        return newItem;    
    }

    public SaveItemData()
    {
        InstanceId = Guid.NewGuid(); // 같은 게 나올 수 있지만 확률 희박
        creationIime = DateTime.Now;
    }


}
