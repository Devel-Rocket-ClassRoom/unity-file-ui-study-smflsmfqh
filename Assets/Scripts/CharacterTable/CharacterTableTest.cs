using UnityEngine;
using UnityEngine.UI;
using TMPro;
// To do: 수정
// 캐릭터테이블.csv에 공격력 수치를 넣고
// 스트링테이블은 "공격력" 텍스트는 따로 분리

public class CharacterTableTest : MonoBehaviour
{
    public string charId;
    public Image Icon;
    public LocalizationText textName;
    public CharacterTableTest2 itemInfo;

    
    private void OnEnable()
    {
        OnChangeCharacterId();
    }

    private void OnValidate()
    {
        OnChangeCharacterId();
    }

    public void OnClick()
    {
        itemInfo.SetItemData(charId);
    }

    public void OnChangeCharacterId()
    {
        if (string.IsNullOrEmpty(charId)) return;
        CharacterData data = DataTableManager.CharacterTable.Get(charId);
        if (data == null) return;
        Icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textName.OnChangedId();
    }
}
