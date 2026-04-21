using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiCharInfo : MonoBehaviour
{
    public static readonly string FormatCommon = "{0} : {1}";
    public Image iconImage;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDesc;
    public TextMeshProUGUI textAttack;
    public TextMeshProUGUI textIQ;

    public void Awake()
    {
        gameObject.SetActive(false);
    }
     public void SetEmpty()
    {
        gameObject.SetActive(false);
        iconImage.sprite = null;
        textName.text = string.Empty;
        textDesc.text = string.Empty;
        textAttack.text = string.Empty;
        textIQ.text = string.Empty;
    }

    public void SetSaveCharData(SaveCharData saveCharData)
    {
        gameObject.SetActive(true);
        CharacterData data = saveCharData.charData;
        iconImage.sprite = data.SpriteIcon;
        textName.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("NAME"), data.StringName);
        textDesc.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("DESC"), data.StringDesc);
        textAttack.text = data.StringAttack;
        textIQ.text = data.StringIQ;
    }
}
