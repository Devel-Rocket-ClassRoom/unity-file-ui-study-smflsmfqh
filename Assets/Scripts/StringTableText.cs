using UnityEngine;
using TMPro;

public class StringTableText : MonoBehaviour
{
    public string id;
    public TextMeshPro text;

    private void Start()
    {
        OnChangeId();
    }

    private void OnChangeId()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }
}
