using Unity.Mathematics;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class UiInvenSlotList : MonoBehaviour
{
    public readonly System.Comparison<SaveItemData>[] comparisons =
    {
        (lhs, rhs) => lhs.CreationTime.CompareTo(rhs.CreationTime),
        (lhs, rhs) => rhs.CreationTime.CompareTo(lhs.CreationTime),
        (lhs, rhs) => lhs.ItemData.StringName.CompareTo(rhs.ItemData.StringName),
        (lhs, rhs) => rhs.ItemData.StringName.CompareTo(lhs.ItemData.StringName),
        (lhs, rhs) => rhs.ItemData.StringName.CompareTo(rhs.ItemData.Cost.ToString()),
        (lhs, rhs) => lhs.ItemData.StringName.CompareTo(rhs.ItemData.Cost.ToString())
    };

    public readonly System.Func<SaveItemData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.ItemData.Type == ItemTypes.Weapon,
        (x) => x.ItemData.Type == ItemTypes.Equip,
        (x) => x.ItemData.Type == ItemTypes.Consumable,
        (x) => x.ItemData.Type == ItemTypes.NonConsumable,
    };
    public UiInvenSlot prefab;
    public ScrollRect scrollRect;
    private List<UiInvenSlot> uiSlotList = new List<UiInvenSlot>();

    private List<SaveItemData> saveItemDataList = new List<SaveItemData>();

    private SortingOptions sorting = SortingOptions.CreationTimeAscending;
    private FilteringOptions filtering = FilteringOptions.None;

    //public Button createButton;
    //public Button deleteButton;

    public SortingOptions Sorting
    {
        get => sorting;
        set
        {
            if (sorting != value)
            {
                sorting = value;
                UpdateSlots();
            }
        }
    }

    public FilteringOptions Filtering
    {
        get => filtering;
        set
        {
            if (filtering != value)
            {
                filtering = value;
                UpdateSlots();
            }
        }
    }

    private int selectedSlotIndex = -1;

    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveItemData> onSelectSlot;
/*
    private void Awake()
    {
        createButton.onClick.AddListener(OnCreate);
        deleteButton.onClick.AddListener(OnDelete);

    }
*/

    private void OnSelectSlot(SaveItemData saveItemData)
    {
        Debug.Log(saveItemData);
    }

    private void Start()
    {
        onSelectSlot.AddListener(OnSelectSlot);
    }

    private void OnDisable()
    {
        saveItemDataList = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddRandomItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RemoveItem();
        }
    }

    public void SetSaveItemDataList(List<SaveItemData> source)
    {
        saveItemDataList = source.ToList();
        UpdateSlots();
    }

    public List<SaveItemData> GetSaveItemDataList()
    {
        return saveItemDataList;
    }
/*
    public void OnCreate()
    {
        saveItemDataList.Add(SaveItemData.GetRandomItem());
        UpdateSlots(saveItemDataList);
    }

    public void OnDelete()
    {
        if (saveItemDataList.Count == 0 || saveItemDataList == null)
        {
            return;
        }

        saveItemDataList.RemoveAt(saveItemDataList.Count - 1);
        UpdateSlots(saveItemDataList);
    }
*/

    private void UpdateSlots()
    {
        var list = saveItemDataList.Where(filterings[(int)filtering]).ToList();
        list.Sort(comparisons[(int)sorting]);

        if (uiSlotList.Count < list.Count)
        {
            for (int i = uiSlotList.Count; i < list.Count; ++i)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    onSelectSlot.Invoke(newSlot.SaveItemData);
                });

                uiSlotList.Add(newSlot);
            }
        }

        for (int i = 0; i < uiSlotList.Count; ++i)
        {
            if (i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetItem(list[i]);
            }
            else
            {
                uiSlotList[i].gameObject.SetActive(false);
                uiSlotList[i].SetEmpty();
            }
        }

        selectedSlotIndex = -1;
        onUpdateSlots.Invoke();
    }

    public void AddRandomItem()
    {
        saveItemDataList.Add(SaveItemData.GetRandomItem());
        UpdateSlots();
    }

    public void RemoveItem()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }
        saveItemDataList.Remove(uiSlotList[selectedSlotIndex].SaveItemData);
        UpdateSlots();
    }
}
