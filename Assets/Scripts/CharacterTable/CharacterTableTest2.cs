using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CharacterTableTest2 : MonoBehaviour
{
    public Image icon;
    public LocalizationText textName;
    public LocalizationText textDesc;
    public TextMeshProUGUI textAttack;
    public TextMeshProUGUI textIq;

    private CharacterData currentData;

    private void OnEnable()
    {
        Variables.OnLanguageChanged += OnChangedLanguage;
        SetEmpty();
    }

    private void OnDisable()
    {
        Variables.OnLanguageChanged -= OnChangedLanguage;
    }

    private void OnChangedLanguage()
    {
        if (currentData != null)
        {
            textAttack.text = currentData.StringAttack;
            textIq.text = currentData.StringIQ;
        }
    }

    public void SetEmpty()
    {
        icon.sprite = null;
        textName.id = string.Empty;
        textDesc.id = string.Empty;
        textAttack.text = string.Empty;
        textIq.text = string.Empty;

        textName.OnChangedId();
        textDesc.OnChangedId();
    }

    public void SetCharacterData(string itemId)
    {
        CharacterData data = DataTableManager.CharacterTable.Get(itemId);
        SetCharacterData(data);
    }

    public void SetCharacterData(CharacterData data)
    {
        currentData = data;
        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textDesc.id = data.Desc;
        textAttack.text = data.StringAttack;
        textIq.text = data.StringIQ;

        textName.OnChangedId();
        textDesc.OnChangedId();
    }
}
