using System.Collections;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardWindow : GenericWindow
{
    public Button[] buttons;
    private string input;
    //private bool isVisible = true;
    public TextMeshProUGUI totalInput;
    public Button cancelButton;
    public Button deleteButton;
    public Button acceptButton;

    //private Coroutine routine;
    private const int maxCharacters = 7;
    private float timer = 0f;
    private float cursorDelay = 0.5f;
    private bool blink;

    private readonly StringBuilder sb = new StringBuilder();
    public GameObject rootKeyboard;

    private void Awake()
    {
        /*
        foreach (var button in buttons)
        {
            var capturedButton = button;
            button.onClick.AddListener(() => OnClickKeyboard(capturedButton));
            
        }
        */
        var keys = rootKeyboard.GetComponentsInChildren<Button>();
        
        foreach (var key in keys)
        {
            var text = key.GetComponentInChildren<TextMeshProUGUI>();
            key.onClick.AddListener(() => OnKey(text.text));
        }
        cancelButton.onClick.AddListener(OnCancel);
        deleteButton.onClick.AddListener(OnDelete);
        acceptButton.onClick.AddListener(OnAccept);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cursorDelay)
        {
            timer = 0f;
            blink = !blink;
        }
    }

    public void OnKey(string key)
    {
        if (sb.Length < maxCharacters)
        {
            sb.Append(key);
            UpdateInputField();
        }
    }

    private void UpdateInputField()
    {
        bool showCursor = sb.Length < maxCharacters && !blink;

        if (showCursor)
        {
            sb.Append('_');
        }

        totalInput.SetText(sb);

        if (showCursor)
        {
            sb.Length -= 1;
        }
    }

    public override void Open()
    {
        sb.Clear();
        timer = 0f;
        blink = false;

        base.Open();
        UpdateInputField();


        /*
        if (routine != null)
        {
            StopCoroutine(routine);
        }

        routine = StartCoroutine(CoroutineCursor());
        */
    }

/*
    private IEnumerator CoroutineCursor()
    {
        string subfix = "_";
        while (true)
        {
            if (totalInput.text.Length > 20)
            {
                isVisible = false;
                Debug.Log("더 이상 입력할 수 없습니다.");
                yield break;
            }
            
            if (isVisible)
            {
                yield return new WaitForSeconds(0.2f);
                subfix = "_";
                string input = totalInput.text;
                isVisible = false;
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
                totalInput.text = string.Concat(totalInput.text, "");
                isVisible = true;
            }
        }
    }
*/

    public void OnClickKeyboard(Button button)
    {
        if (totalInput.text.Length > 20)
        {
            Debug.Log("더 이상 입력할 수 없습니다.");
            return;
        }

        input = button.GetComponentInChildren<TextMeshProUGUI>().text;
        totalInput.text += input;
    }

    public void OnDelete()
    {
        //string result = totalInput.text.Substring(0, totalInput.text.Length - 1);
        //totalInput.text = result;
        if (sb.Length > 0)
        {
            sb.Length -= 1;
        }
        UpdateInputField();

    }

    public void OnCancel()
    {
        //totalInput.text = "_";
        sb.Clear();
        UpdateInputField();

    }

    public void OnAccept()
    {
        windowManager.Open(0);
    }

        
}
