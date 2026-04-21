using UnityEngine;
using TMPro;
using UnityEditor;

public class UiInventory : MonoBehaviour
{
     public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UiInvenSlotList uiInvenSlotList;
    public UiItemInfo uiItemInfo;


    private void OnEnable()
    {

        OnLoad();
        //uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);

        OnChangeFiltering((int)SaveLoadManager.Data.ItemFiltering);
        OnchangeSorting((int)SaveLoadManager.Data.ItemSorting);
    }

    private void OnDisable()
    {
        uiInvenSlotList.onSelectSlot.RemoveListener(uiItemInfo.SetSaveItemData);
    }

    public void OnchangeSorting(int idx)
    {
        uiInvenSlotList.Sorting = (UiInvenSlotList.SortingOptions)idx;
    }

    public void OnChangeFiltering(int idx)
    {
        uiInvenSlotList.Filtering = (UiInvenSlotList.FilteringOptions)idx;
    }

    // 각 버튼들에 해당하는 함수들 (Save, Load, Create, Remove)

    public void OnSave()
    {
        SaveLoadManager.Data.ItemList = uiInvenSlotList.GetSaveItemDataList();
        SaveLoadManager.Data.ItemSorting = (UiInvenSlotList.SortingOptions)sorting.value;
        SaveLoadManager.Data.ItemFiltering = (UiInvenSlotList.FilteringOptions)filtering.value;
        SaveLoadManager.Save();

    }
    public void OnLoad()
    {
        SaveLoadManager.Load();
        uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);
    }
    public void OnCreateItem()
    {
        uiInvenSlotList.AddRandomItem();
    }
    public void OnRemoveItem()
    {
        uiInvenSlotList.RemoveItem();
    }
    
}
