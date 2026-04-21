using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.UI;

public class ItemTableTest2 : MonoBehaviour
{
    public string itemId;
    public Image icon;
    public LocalizationText textName;

    public ItemTableTest3 itemInfo;

    private void OnEnable()
    {
        OnChangeItemId();
    }

    private void OnValidate()
    {
        OnChangeItemId();
        
    }

    public void OnClick()
    {
        itemInfo.SetItemData(itemId);
    }

    public void OnChangeItemId()
    {
        ItemData data = DataTableManager.ItemTable.Get(itemId);
        if (data == null) return;
        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textName.OnChangedId();
    }
}
