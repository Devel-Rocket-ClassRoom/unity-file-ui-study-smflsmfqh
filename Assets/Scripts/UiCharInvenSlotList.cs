using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiCharInvenSlotList : MonoBehaviour
{
    public UiCharInvenSlot prefab;
    public ScrollRect scrollRect;
    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveCharData> onSelectSlot;

    private List<UiCharInvenSlot> uiCharInvenSlotList = new List<UiCharInvenSlot>();
    private List<SaveCharData> saveCharDataList = new List<SaveCharData>();

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

    private void UpdateSlots()
    {
        if (uiCharInvenSlotList.Count < saveCharDataList.Count)
        {
            for (int i = uiCharInvenSlotList.Count; i < saveCharDataList.Count; ++i)
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
            if (i < saveCharDataList.Count)
            {
                uiCharInvenSlotList[i].gameObject.SetActive(true);
                uiCharInvenSlotList[i].SetCharacter(saveCharDataList[i]);
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
