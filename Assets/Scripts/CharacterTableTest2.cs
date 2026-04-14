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

    public void SetEmpty()
    {
        icon.sprite = null;
        textName.id = string.Empty;
        textDesc.text = string.Empty;
        textName.OnChangedId();
    }
    private void LanguageChange()
    {
        if (currentData != null)
        {
            textDesc.text = currentData.ToLocalizedString();
        }
    }

    public void SetItemData(string itemId)
    {
        CharacterData data = DataTableManager.CharacterTable.Get(itemId);
        SetItemData(data);
    }

    public void SetItemData(CharacterData data)
    {
        currentData = data;
        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textDesc.text = data.ToLocalizedString();
        textName.OnChangedId();
    }
}