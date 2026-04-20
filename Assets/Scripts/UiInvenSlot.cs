using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiInvenSlot : MonoBehaviour
{
    public Image imageIcon;
    public TextMeshProUGUI textName;

    public SaveItemData SaveItemData { get; private set; }

    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        SaveItemData = null;
    }

    public void SetItem(SaveItemData data)
    {
        SaveItemData = data;
        imageIcon.sprite = SaveItemData.ItemData.SpriteIcon;
        textName.text = SaveItemData.ItemData.StringName;
    }

    
}
