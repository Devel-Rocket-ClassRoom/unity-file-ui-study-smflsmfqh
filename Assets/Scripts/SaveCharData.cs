using System;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class SaveCharData 
{
    public Guid InstanceId {get; set;}

    [JsonConverter(typeof(CharDataConverter))]
    public CharacterData charData;
    public DateTime CreationTime {get; set;}

    public static SaveCharData GetRandomCharacter()
    {
        SaveCharData newChar = new SaveCharData();
        newChar.charData = DataTableManager.CharacterTable.GetRandom();
        return newChar;
    }

    public SaveCharData()
    {
        InstanceId = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }

    public override string ToString()
    {
        return $"[Character Save Data] {InstanceId}\n{CreationTime}\n{charData.Id}";
    }

}
