using UnityEngine;
using UnityEngine.EventSystems;

public class GenericWindow : MonoBehaviour
{
    public GameObject firstSeleted;

    protected WindowManager windowManager;

    public void Init(WindowManager mgr)
    {
        windowManager = mgr;
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);

        // 게임 오브젝트에 붙어있는 이벤트 컴포넌트
        // 매개변수로 넘긴 게임 오브젝트가 포커스됨
        // null이면 없어짐
        EventSystem.current.SetSelectedGameObject(firstSeleted); 
        
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
        
    }
}
