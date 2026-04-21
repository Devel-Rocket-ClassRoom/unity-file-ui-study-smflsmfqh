using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.IO.LowLevel.Unsafe;

public class ItemTableManager : MonoBehaviour
{
    public string itemName;
    public TextMeshProUGUI nameText;
    public Image icon;
    private ItemTable itemTable = new ItemTable();
    protected ItemTable ItemTable {get {return itemTable;}}
    private string fileName = "itemTable";


    public void Enable()
    {
        itemTable.Load(fileName);
    }

    public void ShowItem(string id)
    {
        SetItem(id);
    }

    protected virtual void OnValidate()
    {
        SetItem(itemName);
    }

    protected virtual void SetItem(string id)
    {
#if UNITY_EDITOR
        if (string.IsNullOrEmpty(id)) return;

        itemTable.Load(fileName);
#endif
        ItemData item = itemTable.Get(id);
        if (item == null) return;

        nameText.text = item.StringName;
        icon.sprite = item.SpriteIcon;
        
    }
}
