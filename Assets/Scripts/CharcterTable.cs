using UnityEngine;
using System.Collections.Generic;
// 1. CSV 파일 (ID/이름/설명/공격력 / 초상화 or 아이콘)
// 2. DataTable 상속 
// 3. DataTableManager에 등록
// 4. 테스트 패널 만들기

// Id,Type,Name,Desc,Attack,HP,Icon

public class CharacterData 
{
    public string Id { get; set; }
    public CharacterTypes Type {get; set;}
    public string Name { get; set; }
    public string Desc { get; set; }
    public string Attack { get; set; }
    public string IQ { get; set; }
    public string Icon { get; set; }

    public override string ToString()
    {
        return $"{Id} / {Type} / {Name} / {Desc} / {Attack} / {IQ} / {Icon}";
    }

    public string StringName => DataTableManager.StringTable.Get(Name);
    public string StringDesc => DataTableManager.StringTable.Get(Desc);
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");
}

public class CharacterTable : DataTable
{
    private readonly Dictionary<string, CharacterData> table = new Dictionary<string, CharacterData>();
    public override void Load(string filename)
    {
        table.Clear();

        string path = string.Format(FormatPath, filename);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        if (textAsset == null)
        {
            Debug.LogError($"CSV 파일 로드 실패: {path}");
            return;
        }
        List<CharacterData> list = LoadCSV<CharacterData>(textAsset.text);

        foreach (var character in list)
        {
            if (!table.ContainsKey(character.Id))
            {
                Debug.Log($"{character.Id} {character} 딕셔너리에 추가");
                table.Add(character.Id, character);
            }
            else
            {
                Debug.LogError("캐릭터 아이디 중복");
            }
        }
    }
    public CharacterData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            Debug.LogError("캐릭터 아이디 없음");
            return null;
        }
        return table[id];
    }
}
