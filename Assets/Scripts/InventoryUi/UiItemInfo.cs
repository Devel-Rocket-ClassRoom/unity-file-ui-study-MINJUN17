using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiItemInfo : MonoBehaviour
{
    public static readonly string ForMatCommon = "{0}: {1}";

    public Image imageIcon;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDesc;
    public TextMeshProUGUI textType;
    public TextMeshProUGUI textValue;
    public TextMeshProUGUI textCost;

    private SaveItemData currentData; 

    private void OnEnable()
    {
        Variables.OnLanguageChanged += Refresh;
    }
    private void OnDisable()
    {
        Variables.OnLanguageChanged -= Refresh;
    }
    private void Refresh()
    {
        if (currentData != null) SetSaveItemData(currentData);
    }

    public void SetEmpty()
    {
        currentData = null;
        imageIcon.sprite = null;
        textName.text = string.Empty;
        textDesc.text = string.Empty;
        textType.text = string.Empty;
        textValue.text = string.Empty;
        textCost.text = string.Empty;
    }
    public void SetSaveItemData(SaveItemData saveItemData)
    {
        currentData = saveItemData;
        ItemData data = saveItemData.ItemData;
        imageIcon.sprite = data.SpriteIcon;
        textName.text = string.Format(ForMatCommon, DataTableManager.StringTable.Get("NAME"),data.StringName);
        textDesc.text = string.Format(ForMatCommon, DataTableManager.StringTable.Get("DESC"), data.StringDesc);
        string id = data.Type.ToString().ToUpper();
        textType.text = string.Format(ForMatCommon, DataTableManager.StringTable.Get("TYPE"), DataTableManager.StringTable.Get(id));
        textValue.text = string.Format(ForMatCommon, DataTableManager.StringTable.Get("VALUE"), data.Value);
        textCost.text = string.Format(ForMatCommon, DataTableManager.StringTable.Get("COST"), data.Cost);

    }
}
