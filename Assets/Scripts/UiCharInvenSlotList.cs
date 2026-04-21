using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiCharInvenSlotList : MonoBehaviour
{
    public readonly System.Comparison<SaveCharData>[] comparisons =
    {
        (lhs, rhs) => lhs.CreationTime.CompareTo(rhs.CreationTime),
        (lhs, rhs) => rhs.CreationTime.CompareTo(lhs.CreationTime),
        (lhs, rhs) => lhs.charData.StringName.CompareTo(rhs.charData.StringName),
        (lhs, rhs) => rhs.charData.StringName.CompareTo(lhs.charData.StringName),
        (lhs, rhs) => lhs.charData.Attack.CompareTo(rhs.charData.Attack),
        (lhs, rhs) => rhs.charData.Attack.CompareTo(lhs.charData.Attack),
        (lhs, rhs) => lhs.charData.IQ.CompareTo(rhs.charData.IQ),
        (lhs, rhs) => rhs.charData.IQ.CompareTo(lhs.charData.IQ)
    };

    public readonly System.Func<SaveCharData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.charData.Type == CharacterTypes.Warrior,
        (x) => x.charData.Type == CharacterTypes.Magician,
        (x) => x.charData.Type == CharacterTypes.Healer,
        (x) => x.charData.Type == CharacterTypes.Elf,
    };

    public UiCharInvenSlot prefab;
    public ScrollRect scrollRect;
    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveCharData> onSelectSlot;

    private List<UiCharInvenSlot> uiCharInvenSlotList = new List<UiCharInvenSlot>();
    private List<SaveCharData> saveCharDataList = new List<SaveCharData>();

    private SortingOptions sorting = SortingOptions.CreationTimeAscending;
    private FilteringOptions filtering = FilteringOptions.None;

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

    private void OnSelectSlot(SaveCharData saveCharData)
    {
        Debug.Log(saveCharData);
    }

    private void Start()
    {
        onSelectSlot.AddListener(OnSelectSlot);
    }

    private void OnDisable()
    {
        saveCharDataList = null;
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

    public void SetSaveCharDataList(List<SaveCharData> source)
    {
        saveCharDataList = source.ToList();
        UpdateSlots();
    }

    public List<SaveCharData> GetSaveCharDataList()
    {
        return saveCharDataList;
    }

    private void UpdateSlots()
    {
        var list = saveCharDataList.Where(filterings[(int)filtering]).ToList();
        list.Sort(comparisons[(int)sorting]);
        if (uiCharInvenSlotList.Count < list.Count)
        {
            for (int i = uiCharInvenSlotList.Count; i < list.Count; ++i)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    onSelectSlot.Invoke(newSlot.SaveCharData);
                });

                uiCharInvenSlotList.Add(newSlot);
                
            }
        }

        for (int i = 0; i < uiCharInvenSlotList.Count; i++)
        {
            if (i < list.Count)
            {
                uiCharInvenSlotList[i].gameObject.SetActive(true);
                uiCharInvenSlotList[i].SetCharacter(list[i]);
            }
            else
            {
                uiCharInvenSlotList[i].gameObject.SetActive(false);
                uiCharInvenSlotList[i].SetEmpty();
            }
        }

        selectedSlotIndex = -1;
        onUpdateSlots.Invoke();
    }

    public void AddRandomItem()
    {
        saveCharDataList.Add(SaveCharData.GetRandomCharacter());
        UpdateSlots();
    }

    public void RemoveItem()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }
        saveCharDataList.Remove(uiCharInvenSlotList[selectedSlotIndex].SaveCharData);
        UpdateSlots();
    }
}
