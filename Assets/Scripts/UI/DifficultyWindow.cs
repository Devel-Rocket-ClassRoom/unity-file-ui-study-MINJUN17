using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyWindow : GenericWindow
{
    public Toggle[] toggles;
    public Button cancelButton;
    public Button applyButton;

    private string folderPath;
    private string filePath;

    public int selected;

    private void Awake()
    {
        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);
        cancelButton.onClick.AddListener(OnClickCancel);
        applyButton.onClick.AddListener(OnClickApply);
    }

    public override void Open()
    {
        if (folderPath == null)
        {
            folderPath = Path.Combine(Application.persistentDataPath, "SaveDifficulty");
            filePath = Path.Combine(folderPath, "Difficulty.txt");
        }

        if (File.Exists(filePath))
        {
            selected = int.Parse(File.ReadAllText(filePath));
        }
        else
        {
            selected = 0;
        }
        base.Open();
        toggles[selected].isOn = true;
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnEasy(bool active)
    {
        if (active)
        {
            Debug.Log("OnEasy");
            selected = 0;
        }
    }

    public void OnNormal(bool active)
    {
        if (active)
        {
            Debug.Log("OnNormal");
            selected = 1;
        }
    }

    public void OnHard(bool active)
    {
        if (active)
        {
            Debug.Log("OnHard");
            selected = 2;
        }
    }

    public void OnClickCancel()
    {
        windowManager.Open(0);
    }

    public void OnClickApply()
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        File.WriteAllText(filePath, selected.ToString());
        windowManager.Open(0);
    }
}