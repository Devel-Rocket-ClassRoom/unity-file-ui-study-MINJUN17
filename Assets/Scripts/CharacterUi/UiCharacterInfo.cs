using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCharacterInfo : MonoBehaviour
{
    public static readonly string ForMatCommon = "{0}: {1}";
    public Image imageIcon;
    public Image WeaponIcon;
    public Image EquipIcon;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDesc;
    public TextMeshProUGUI textAttack;
    public TextMeshProUGUI textDefense;

    public Button weaponEquipButton;
    public Button weaponUnequipButton;
    public Button EquipEquipButton;
    public Button EquipUnequipButton;

    public UiInvenSlotList itemSelectList;
    private bool isSelectingWeapon = false;

    private SaveCharacterData currentData;

    private void Awake()
    {
        weaponEquipButton.onClick.AddListener(OnClickWeaponEquip);
        weaponUnequipButton.onClick.AddListener(OnClickWeaponUnequip);
        EquipEquipButton.onClick.AddListener(OnCLickEquipEquip);
        EquipUnequipButton.onClick.AddListener(OnClickEquipUnequip);

        //itemSelectList.onSelectSlot.AddListener(Onsel)
    }
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
        if (currentData != null) SetSaveCharacterData(currentData);
    }

    public void SetEmpty()
    {
        currentData = null;
        imageIcon.sprite = null;
        textName.text = string.Empty;
        textDesc.text = string.Empty;
        textAttack.text = string.Empty;
        textDefense.text = string.Empty;
        WeaponIcon.sprite = null;
        EquipIcon.sprite = null;
    }
    public void SetSaveCharacterData(SaveCharacterData saveCharacterData)
    {
        currentData = saveCharacterData;
        CharacterData data = saveCharacterData.CharacterData;
        imageIcon.sprite = data.SpriteIcon;
        WeaponIcon.sprite = currentData.WeaponItem?.ItemData.SpriteIcon;
        EquipIcon.sprite = currentData.EquipItem?.ItemData.SpriteIcon;
        textName.text = string.Format(ForMatCommon, DataTableManager.StringTable.Get("NAME"), data.StringName);
        textDesc.text = string.Format(ForMatCommon, DataTableManager.StringTable.Get("DESC"), data.StringDesc);
        textAttack.text = string.Format(ForMatCommon, DataTableManager.StringTable.Get("ATTACK"), data.Attack);
        textDefense.text = string.Format(ForMatCommon, DataTableManager.StringTable.Get("DEFENSE"), data.Defense);
    }
    private void OnSelectItem(SaveItemData item)
    {
        if (currentData == null)
        {
            return;
        }
        if (isSelectingWeapon)
        {
            currentData.WeaponItem = item;
        }
        else
        {
            currentData.EquipItem = item;
        }
        SetSaveCharacterData(currentData);
    }

    private void OnClickWeaponEquip()
    {
        if(currentData == null)
        {
            return;
        }
        isSelectingWeapon = true;
        itemSelectList.Filtering = UiInvenSlotList.FilteringOptions.Weapon;
    }
    private void OnClickWeaponUnequip()
    {
        if(currentData == null)
        {
            return;
        }
        currentData.WeaponItem = null;
        SetSaveCharacterData(currentData);
    }

    private void OnCLickEquipEquip()
    {
        if(currentData == null)
        {
            return;
        }
        isSelectingWeapon = false;
        itemSelectList.Filtering = UiInvenSlotList.FilteringOptions.Equip;
    }

    private void OnClickEquipUnequip()
    {
        if(currentData == null)
        {
            return;
        }
        currentData.EquipItem = null;
        SetSaveCharacterData(currentData);
    }
}
