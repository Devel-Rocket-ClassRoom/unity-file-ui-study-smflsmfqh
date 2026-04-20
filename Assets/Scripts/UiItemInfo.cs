using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data.Common;

public class UiItemInfo : MonoBehaviour
{
    public static readonly string FormatCommon = "{0} : {1}";
    public Image iconImage;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDescription;
    public TextMeshProUGUI textType;
    public TextMeshProUGUI textValue;
    public TextMeshProUGUI textCost;
    public void SetEmpty()
    {
        iconImage.sprite = null;
        textName.text = string.Empty;
        textDescription.text = string.Empty;
        textType.text = string.Empty;
        textValue.text = string.Empty;
        textCost.text = string.Empty;

    }

    public void SetSaveItemData(SaveItemData saveItemData)
    {
        ItemData data = saveItemData.ItemData;
        iconImage.sprite = data.SpriteIcon;
        textName.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("NAME"), data.StringName);
        textDescription.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("DESC"), data.StringDesc);
        string id = data.Type.ToString().ToUpper();
        textType.text = string.Format(
            FormatCommon, 
            DataTableManager.StringTable.Get("TYPE"), 
            DataTableManager.StringTable.Get(id)
            );
        textValue.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("VALUE"), data.Value);
        textCost.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("COST"), data.Cost);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetEmpty();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var saveItemData = SaveItemData.GetRandomItem();
            SetSaveItemData(saveItemData);
        }
    }
}
