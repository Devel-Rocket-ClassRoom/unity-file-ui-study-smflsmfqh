using UnityEngine;
using UnityEngine.UI;


public class StartWindow : GenericWindow
{
    public Button continueButton;
    public Button startButton;
    public Button optionButton;

    public bool canContinue;

    private void Awake()
    {
        // 코드로 버튼 연결하는 방법
        continueButton.onClick.AddListener(OnContinue);
        startButton.onClick.AddListener(OnNewGame);
        optionButton.onClick.AddListener(OnOption);

    }
    public override void Open()
    {
        continueButton.gameObject.SetActive(canContinue);

        if (!canContinue)
        {
            firstSeleted = startButton.gameObject;
        }

        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnContinue()
    {
        Debug.Log("OnContinue()");
    }

    public void OnNewGame()
    {
        Debug.Log("OnNewGame()");
        
    }

    public void OnOption()
    {
        Debug.Log("OnOption()");
        
    }
}
