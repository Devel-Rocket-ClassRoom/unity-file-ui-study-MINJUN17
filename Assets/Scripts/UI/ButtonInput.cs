using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInput : MonoBehaviour
{
    private string buttonText;
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>().text;
        button.onClick.AddListener(GetKey);
    }
    private void GetKey()
    {
        KeyboardWindow.Instance.OnClickButton(buttonText);
    }
}
