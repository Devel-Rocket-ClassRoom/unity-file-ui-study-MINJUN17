using UnityEngine;
using UnityEngine.UI;

public class CharacterTableTest1 : MonoBehaviour
{
    public string characterId;

    public Image icon;
    public LocalizationText textName;

    public CharacterTableTest2 characterInfo;
    private void OnEnable()
    {
        OnChangeItemId();
    }
    public void OnValidate()
    {
        OnChangeItemId();
    }
    public void OnChangeItemId()
    {
        CharacterData data = DataTableManager.CharacterTable.Get(characterId);
        if (data != null)
        {
            icon.sprite = data.SpriteIcon;
            textName.id = data.Name;
            textName.OnChangedId();
        }
    }
    public void OnClick()
    {
        characterInfo.SetItemData(characterId);
    }
}
