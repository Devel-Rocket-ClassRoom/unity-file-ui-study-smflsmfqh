using UnityEngine.UI;
using UnityEngine;

public class ItemTableTest3 : MonoBehaviour
{
    public Image icon;
    public LocalizationText textName;
    public LocalizationText textDesc;

    private void OnEnable()
    {
        SetEmpty();
    }

    public void SetEmpty()
    {
        icon.sprite = null;
        textName.id = string.Empty;
        textDesc.id = string.Empty;

        textName.OnChangedId();
        textDesc.OnChangedId();
    }

    public void SetItemData(string itemId)
    {
        ItemData data = DataTableManager.ItemTable.Get(itemId);
        SetItemData(data);
    }
    public void SetItemData(ItemData data)
    {
        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textDesc.id = data.Desc;

        textName.OnChangedId();
        textDesc.OnChangedId();
    }
}
