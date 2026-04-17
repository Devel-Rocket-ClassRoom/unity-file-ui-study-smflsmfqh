using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System.Reflection.Emit;

public class GameOverWindow : GenericWindow
{
    enum SelectedSide
    {
        Left,
        Right,
    }
    public TextMeshProUGUI leftStatLabel;
    public TextMeshProUGUI leftStatValue;
    public TextMeshProUGUI rightStatLabel;
    public TextMeshProUGUI rightStatValue;
    public TextMeshProUGUI scoreValue;
    public Button nextButton;

    public float statsDelay = 1f;
    public float scoreDuration = 2f;

    //private SelectedSide side;
    //private string[] leftValues = new string[3];
    //private string[] rightValues = new string[3];

    private TextMeshProUGUI[] statsLabels;
    private TextMeshProUGUI[] statsValues;
    private const int totalStats = 6;
    private const int statsPerColumn = 3;
    private int[] statsRolls = new int[totalStats];
    private int finalScore;
    private Coroutine routine;

    private void Awake()
    {
        statsLabels = new TextMeshProUGUI[] {leftStatLabel, rightStatLabel};
        statsValues = new TextMeshProUGUI[] {leftStatValue, rightStatValue};

        nextButton.onClick.AddListener(OnNext);
        /*
        leftStatValue.text = string.Format("{0}\n{1}\n{2}", leftValues[0], leftValues[1], leftValues[2]);
        rightStatValue.text = string.Format("{0}\n{1}\n{2}", rightValues[0], rightValues[1], rightValues[2]);

         for (int i = 0; i < leftValues.Length; i++)
        {
            leftValues[i] = string.Empty;
            rightValues[i] = string.Empty;
        }
       */
    } 
/*

    private IEnumerator SetScoreCoroutine()
    {
        string[] values = new string[9];
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = "0";
        }

        scoreValue.text = string.Format("{8}{7}{6}{5}{4}{3}{2}{1}{0}", values[0], values[1], values[2], values[3], values[4],values[5], values[6], values[7], values[8]);
        int score = Random.Range(0, 999999999);

        for (int i = 0; i < values.Length; i++)
        {
            int digit = score %10;
            values[i] = digit.ToString();
            score /= 10;
            scoreValue.text = string.Format("{8}{7}{6}{5}{4}{3}{2}{1}{0}", values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8]);
            yield return new WaitForSeconds(0.3f);
        }
    }
*/

/*
    private IEnumerator SetLeftValueCoroutine(SelectedSide side)
    {   
        switch (side)
        {
            case SelectedSide.Left:
                {
                    for (int i = 0; i < leftValues.Length; i++)
                    {
                        leftValues[i] = Random.Range(0, 9999).ToString();
                        leftStatValue.text = string.Format("{0}\n{1}\n{2}", leftValues[0], leftValues[1], leftValues[2]);
                        yield return new WaitForSeconds(0.5f);
                    }
                    
                break;
                }
            case SelectedSide.Right:
                {
                    for (int i = 0; i < rightValues.Length; i++)
                    {
                        rightValues[i] = Random.Range(0, 9999).ToString();
                        rightStatValue.text = string.Format("{0}\n{1}\n{2}", rightValues[0], rightValues[1], rightValues[2]);
                        yield return new WaitForSeconds(0.5f);
                    }
                }
                    
                break;
        }
       
    }
    */

/*
    private IEnumerator RunCoroutine()
    {
        side = SelectedSide.Left;
        yield return StartCoroutine(SetLeftValueCoroutine(side));
        side = SelectedSide.Right;
        yield return StartCoroutine(SetLeftValueCoroutine(side));
        yield return StartCoroutine(SetScoreCoroutine());
    }
*/

    public override void Open()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
        base.Open();

        ResetStats();
        routine = StartCoroutine(CoPlayGameOverRoutine());

        //StartCoroutine(RunCoroutine());
    }

    public override void Close()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }

        base.Close();
    }

    public void OnNext()
    {
        windowManager.Open(0);
    }

    private void ResetStats()
    {
        for (int i = 0; i < totalStats; ++i)
        {
            statsRolls[i] = Random.Range(0, 1000);
        }

        finalScore = Random.Range(0, 1000000000);

        for (int i = 0; i < statsLabels.Length; ++i)
        {
            statsLabels[i].text = string.Empty;
            statsValues[i].text = string.Empty;
        }

        scoreValue.text = $"{0:D9}";
    }

    private IEnumerator CoPlayGameOverRoutine()
    {
        for (int i = 0; i < totalStats; ++i)
        {
            yield return new WaitForSeconds(statsDelay);

            int column = i / statsPerColumn;
            var labelText = statsLabels[column];
            var valueText = statsValues[column];
            string newline = (i % statsPerColumn == 0) ? string.Empty : "\n";
            labelText.text = $"{labelText.text}{newline}Stat {i}";
            valueText.text = $"{valueText.text}{newline}{statsRolls[i]:D4}";
        }

       
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / scoreDuration;
            int current = Mathf.FloorToInt(Mathf.Lerp(0, finalScore, t));
            scoreValue.text = $"{current:D9}";
            yield return null; // 한 프레임 대기
        }

        scoreValue.text = $"{finalScore:D9}";
        routine = null;
    }
}
