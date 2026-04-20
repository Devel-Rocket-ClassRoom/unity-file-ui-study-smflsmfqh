using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UiInvenSlotList : MonoBehaviour
{
    public UiInvenSlot prefab;
    public ScrollRect scrollRect;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < 10; i++)
            {
                var saveItemData = SaveItemData.GetRandomItem();
                var newInven = Instantiate(prefab, scrollRect.content); // 프리팹을 스크롤 렉트 컨텐트 자식으로 생성
                newInven.SetItem(saveItemData);
                Debug.Log($"{newInven.SaveItemData.ItemData.StringName}");
            }
        }
    }
}
