using UnityEngine;
using CsvHelper;
using System.IO;
using System.Globalization;

public class CSVData
{
    public string Id { get; set; }
    public string String { get; set; }
}

public class CsvTest1 : MonoBehaviour
{
    //public TextAsset textAsset;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // --- 1. csv 데이터 불러오는 방법 ----
            // Unity의 TextAsset으로 인스펙터에 연결되어있으면 이렇게 호출하면 해당 텍스트파일 전체 텍스트가 string형으로 리턴됨
            // 드래그 드롭으로 asset을 연결하면 게임이 실행될 때 or 해당 게임 오브젝트가 생성되는 순간에 메모리에 한 번에 올라감 -> 비효율적
            //string csv = textAsset.text; 

            // --- 2 csv 데이터 불러오는 방법 ---
            // Resources.Load를 활용한 TextAsset 불러오기
            // 파일 경로를 매개변수로 받아서 해당하는 Asset을 로드해서 해당 데이터를 참조하는 변수
            // 규칙: Resources라는 폴더가 있어야 함(약속임) & 파일의 확장자는 스킵 & 일반화 인자에 asset의 데이터형식(<TextAsset>)을 적음
            // 원할 때 로드(로드될 때 메모리에 올라감)하고 원하지 않을 때 언로드(메모리에서 내려감)해서 메모리 관리 효율적으로 할 수 있음
            TextAsset textAsset = Resources.Load<TextAsset>("DataTables/StringTableKr3"); 
            string csv = textAsset.text;
            using (var reader = new StringReader(csv))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecords<CSVData>();  
                foreach (var record in records)
                {
                    Debug.Log($"{record.Id} : {record.String}");        
                }
            }
        }
    }
}
