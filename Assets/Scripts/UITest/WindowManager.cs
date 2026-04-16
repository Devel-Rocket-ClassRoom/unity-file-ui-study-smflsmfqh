using UnityEngine;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
{
    public GenericWindow[] windows;

    // --- id는 윈도우 배열의 인덱스로 사용 ---
    public int currentWindowId; // 현재 활성화된 윈도우의 아이디
    public int defaultWindowId; // 시작하면 활성화할 윈도우의 아이디

    private void Awake()
    {
        foreach (var window in windows)
        {
            window.gameObject.SetActive(false);
            window.Init(this);
        }
        currentWindowId = defaultWindowId;
        windows[currentWindowId].Open();
    }

    public GenericWindow Open(int id)
    {
        windows[currentWindowId].Close();
        currentWindowId = id;
        windows[currentWindowId].Open();

        return windows[currentWindowId];
    }
}
