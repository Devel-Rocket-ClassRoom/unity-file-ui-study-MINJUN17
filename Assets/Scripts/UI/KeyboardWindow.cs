using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardWindow : GenericWindow
{
    public static KeyboardWindow Instance;

    public TextMeshProUGUI headerText;
    public StringBuilder text;
    public Button deleteButton;
    public Button cancelButton;
    public Button acceptButton;

    private int maxLength = 7;
    private float cursorEffectInterval = 0.5f;
    private float cursorEffectTimer;
    private string cursor = "_";
    private bool isCursorActive;


    private void Awake()
    {
        Instance = this;
        text = new StringBuilder();
        deleteButton.onClick.AddListener(OnClickDeleteButton);
        cancelButton.onClick.AddListener(OnClickCancelButton);
        acceptButton.onClick.AddListener(OnClickAcceptButton);
    }
    public override void Open()
    {
        isCursorActive = true;
        text.Clear();
        base.Open();
    }
    public override void Close()
    {
        base.Close();
    }
    public void OnClickButton(string buttonText)
    {
        if (text.Length < maxLength)
        {
            text.Append(buttonText);
            headerText.text = text.ToString();
            if(text.Length == maxLength)
            {
                cursor = " ";
                isCursorActive = false;
            }
        }
    }
    private void Update()
    {
        cursorEffectTimer += Time.deltaTime;

        if (cursorEffectTimer > cursorEffectInterval && text.Length < maxLength)
        {
            isCursorActive = !isCursorActive;
            
            cursor = isCursorActive ? "_" : " ";
            cursorEffectTimer = 0;
        }
        headerText.text = $"{text.ToString()}{cursor}";
    }
    public void OnClickDeleteButton()
    {
        if (text.Length > 0)
        {
            text.Remove(text.Length - 1, 1);
        }
        headerText.text = text.ToString();
    }
    public void OnClickCancelButton()
    {
        text.Clear();
        headerText.text = text.ToString();
    }
    public void OnClickAcceptButton()
    {
        windowManager.Open(0);
    }

}
