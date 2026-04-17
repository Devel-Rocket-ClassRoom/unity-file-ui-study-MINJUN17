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
        folderPath = Path.Combine(Application.persistentDataPath, "SaveDifficulty");
        filePath = Path.Combine(Application.persistentDataPath, "SaveDifficulty", "Difficulty.txt");
        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);
        cancelButton.onClick.AddListener(OnClickCancel);
        applyButton.onClick.AddListener(OnClickApply);
    }
    public override void Open()
    {
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
        }
    }
    public void OnNormal(bool active)
    {
        Debug.Log("OnNormal");
    }
    public void OnHard(bool active)
    {
        Debug.Log("OnHard");
    }
    public void OnClickCancel()
    {
        windowManager.Open(0);
    }
    public void OnClickApply()
    {
        folderPath = Path.Combine(Application.persistentDataPath, "SaveDifficulty");
        string filePath = Path.Combine(folderPath, "Difficulty.txt");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        File.WriteAllText(filePath, selected.ToString());
        windowManager.Open(0);
    }

}
