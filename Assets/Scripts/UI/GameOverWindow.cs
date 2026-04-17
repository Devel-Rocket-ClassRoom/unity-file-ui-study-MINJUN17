using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameOverWindow : GenericWindow
{
    public TextMeshProUGUI[] leftStatLabel;
    public TextMeshProUGUI[] leftStatValue;

    public TextMeshProUGUI[] rightStatLabel;
    public TextMeshProUGUI[] rightStatValue;

    public TextMeshProUGUI scoreValue;

    public Color originColor;

    private int totalScore;
    private float duration = 3f;

    public Button nextButton;
    private Coroutine routine;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnNext);
        
    }

    public override void Open()
    {
        base.Open();
        routine = StartCoroutine(ShowStat());
    }
    public override void Close()
    {
        totalScore = 0;
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

    public void PlayGameOverAnimation()
    {
        StartCoroutine(ShowStat());
    }
    public IEnumerator ShowStat()
    {
        originColor = leftStatValue[0].GetComponent<TextMeshProUGUI>().color;
        for (int i = 0; i < leftStatLabel.Length; i++)
        {
            leftStatLabel[i].text = $"stat{i}";
            rightStatLabel[i].text = $"stat{i + leftStatLabel.Length}";
            leftStatValue[i].text = Random.Range(0, 999).ToString();
            rightStatValue[i].text = Random.Range(0, 999).ToString();

            leftStatLabel[i].GetComponent<TextMeshProUGUI>().color = Color.clear;
            rightStatLabel[i].GetComponent<TextMeshProUGUI>().color = Color.clear;

            leftStatValue[i].GetComponent<TextMeshProUGUI>().color = Color.clear;
            rightStatValue[i].GetComponent<TextMeshProUGUI>().color = Color.clear;
        }
        for (int i = 0; i < leftStatLabel.Length + rightStatLabel.Length; i++)
        {
            yield return new WaitForSeconds(1);
            int score = Random.Range(0, 999);
            totalScore += score;
            if ( i < leftStatLabel.Length)
            {
                leftStatLabel[i].GetComponent<TextMeshProUGUI>().color = originColor;
                leftStatValue[i].GetComponent<TextMeshProUGUI>().color = originColor;
            }
            else
            {
                rightStatLabel[i - leftStatLabel.Length].GetComponent<TextMeshProUGUI>().color = originColor;
                rightStatValue[i - leftStatLabel.Length].GetComponent<TextMeshProUGUI>().color = originColor;
            }
        }
        yield return new WaitForSeconds(1);
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            int current = (int)Mathf.Lerp(0, totalScore, t);
            scoreValue.text = current.ToString("D9");
            yield return null;
        }
        scoreValue.text = totalScore.ToString("D9");

    }
}
