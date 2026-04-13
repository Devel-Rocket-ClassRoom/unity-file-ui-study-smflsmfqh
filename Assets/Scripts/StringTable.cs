using UnityEngine;
using System.Collections.Generic;


public class StringTable : DataTable
{ 
    public class Data
    {
        public string Id {  get; set; }
        public string String { get; set; }
    }

    // Dictionary 형태로 관리
    private readonly Dictionary<string, string> table = new Dictionary<string, string>(); // 한 번 로드하고 계속 사용할 거기 때문에 readonly

    public static readonly string Unknown = "키 없음";

    public override void Load(string fileName) // 한 번 데이터 파일을 로드하고 계속 사용할 거임
    {
        table.Clear(); // 활성화된 언어만 관리하고자 함 -> 로드되는 시점에서 교체

        string path = string.Format(FormatPath, fileName);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<Data>(textAsset.text); // 데이터를 리스트로 받아옴

        foreach (Data data in list)
        {
            if (!table.ContainsKey(data.Id))
            {
                table.Add(data.Id, data.String); // Dictionary의 Add 메서드는 key값의 중복 허용하지 않음 -> 에러남 => 검사해줘야 함
            }
            else
            {
                Debug.LogError($"키 중복: {data.Id}");
            }
        }
    }

    public string Get(string key) // 문자열을 쓸 때 사용할 메서드
    {
        if (!table.ContainsKey(key))
        {
            return Unknown;
        }
        return table[key];
    }
}
