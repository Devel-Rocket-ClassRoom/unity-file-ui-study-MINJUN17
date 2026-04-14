using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTableTest2 : MonoBehaviour
{
    public Image icon;
    public Localization textName;
    public Localization textDesc;
    public Localization textAttack;
    private void OnEnable()
    {
        SetEmpty();
    }
    public void SetEmpty()
    {
        icon.sprite = null;
        textName.id = string.Empty;
        textDesc.id = string.Empty;
        textName.OnChangedId();
        textDesc.OnChangedId();
    }
    public void SetItemData(string itemId)
    {
        CharacterData data = DataTableManager.CharacterTable.Get(itemId);
        SetItemData(data);
    }

    public void SetItemData(CharacterData data)
    {
        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textDesc.id = data.Desc;
        textAttack.id = data.Attack.ToString();
        textName.OnChangedId();
        textDesc.OnChangedId();
        textAttack.OnChangedId();

    }
}
