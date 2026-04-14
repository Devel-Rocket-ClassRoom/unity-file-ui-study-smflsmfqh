using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CharacterTableTest2 : MonoBehaviour
{
    public Image icon;
    public LocalizationText textName;
    public LocalizationText textDesc;
    public LocalizationText textAttack;
    public LocalizationText textIq;

    private void OnEnable()
    {
        SetEmpty();
    }

    public void SetEmpty()
    {
        icon.sprite = null;
        textName.id = string.Empty;
        textDesc.id = string.Empty;
        textAttack.id = string.Empty;
        textIq.id = string.Empty;

        textName.OnChangedId();
        textDesc.OnChangedId();
        textAttack.OnChangedId();
        textIq.OnChangedId();
    }

    public void SetItemData(string itemId)
    {
        CharacterData data = DataTableManager.CharacterTable.Get(itemId);
        SetItemData(data);
    }
    public void SetItemData(CharacterData data)
    {
        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textDesc.id = data.Desc;
        textAttack.id = data.Attack;
        textIq.id = data.IQ;

        textName.OnChangedId();
        textDesc.OnChangedId();
        textAttack.OnChangedId();
        textIq.OnChangedId();

    }
}
