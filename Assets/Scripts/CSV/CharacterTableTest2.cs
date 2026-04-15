using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterTableTest2 : MonoBehaviour
{
    public Image icon;
    public LocalizationText textName;
    public TextMeshProUGUI textDesc;

    private CharacterData currentData;

    private void OnEnable()
    {
        SetEmpty();
        Variables.OnLanguageChanged += LanguageChange;
    }
    private void OnDisable()
    {
        Variables.OnLanguageChanged -= LanguageChange;
    }

    public void SetEmpty()
    {
        currentData = null;
        icon.sprite = null;
        textName.id = string.Empty;
        textDesc.text = string.Empty;
        textName.OnChangedId();
    }
    private void LanguageChange()
    {
        if (currentData != null)
        {
            textDesc.text = currentData.GetStatText();
        }
    }

    public void SetItemData(string characterId)
    {
        CharacterData data = DataTableManager.CharacterTable.Get(characterId);
        SetItemData(data);
    }

    public void SetItemData(CharacterData data)
    {
        currentData = data;
        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textDesc.text = data.GetStatText();
        textName.OnChangedId();
    }
}