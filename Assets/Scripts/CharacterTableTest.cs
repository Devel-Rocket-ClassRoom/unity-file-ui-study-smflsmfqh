using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
