using UnityEngine;
using TMPro;
using UnityEditor;

public class UiCharInventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;
    public UiCharInvenSlotList charInvenSlotList;
    public UiCharInfo charInfo;

    private void OnEnable()
    {
        OnLoad();

        
    }

    private void OnDisable()
    {
        charInvenSlotList.onSelectSlot.RemoveListener(charInfo.SetSaveCharData);
    }

    public void OnChangeSorting(int idx)
    {
        charInvenSlotList.Sorting = (SortingOptions)idx;
    }

    public void OnChangeFiltering(int idx)
    {
        charInvenSlotList.Filtering = (FilteringOptions)idx;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.CharList = charInvenSlotList.GetSaveCharDataList();
        SaveLoadManager.Data.CharSorting = (SortingOptions)sorting.value;
        SaveLoadManager.Data.charFiltering = (FilteringOptions)filtering.value;
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();
        charInvenSlotList.SetSaveCharDataList(SaveLoadManager.Data.CharList);
    }

    public void OnCreateItem()
    {
        charInvenSlotList.AddRandomItem();
    } 

    public void OnRemoveItem()
    {
        charInvenSlotList.RemoveItem();
    }  

}
