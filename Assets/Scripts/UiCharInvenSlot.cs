using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UiCharInvenSlot : MonoBehaviour
{
    public int slotIndex = -1;
    public Image imageIcon;
    public TextMeshProUGUI textName;

    public SaveCharData SaveCharData {get; private set;}
    public Button button;

    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        SaveCharData = null;
    }

    public void SetCharacter(SaveCharData data)
    {
        SaveCharData = data;
        imageIcon.sprite = SaveCharData.charData.SpriteIcon;
        textName.text = SaveCharData.charData.StringName;
    }
}
