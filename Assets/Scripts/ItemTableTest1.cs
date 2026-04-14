using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTableTest1 : MonoBehaviour
{
    public string itemId;

    public Image icon;
    public Localization textName;

    public ItemTableTest2 itemInfo;
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
        ItemData data = DataTableManager.ItemTable.Get(itemId);
        if(data != null)
        {
            icon.sprite = data.SpriteIcon;
            textName.id = data.Name;
            textName.OnChangedId();
        }
    }
    public void OnClick()
    {
        itemInfo.SetItemData(itemId);
    }
}
