using TMPro;
using UnityEngine;

public class UiPanelCharacter : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;
    public UiCharacterSlotList uiCharacterSlotList;
    public UiCharacterInfo uiCharacterInfo;

    private readonly string[] sortingIds = { "Sort_Character_TimeAsc", "Sort_Character_TimeDesc", "Sort_Character_AttackAsc", "Sort_Character_AttackDesc", "Sort_Character_DefenseAsc", "Sort_Character_DefenseDesc" };
    private readonly string[] filteringIds = { "Filter_Character_None", "Filter_Character_Warrior", "Filter_Character_Mage", "Filter_Character_Archer", "Filter_Character_Rouge" };


    private void Awake()
    {
        uiCharacterSlotList.SetSaveItemDataList(SaveLoadManager.Data.CharacterList);
        uiCharacterSlotList.onSelectSlot.AddListener(uiCharacterInfo.SetSaveCharacterData);
        uiCharacterSlotList.onUpdateSlots.AddListener(uiCharacterInfo.SetEmpty);
    }
    private void OnEnable()
    {
        Variables.OnLanguageChanged += RefreshDropdowns;
        RefreshDropdowns();
        OnLoad();
        sorting.value = (int)SaveLoadManager.Data.CharacterSorting;
        filtering.value = (int)SaveLoadManager.Data.CharacterFiltering;
    }
    private void OnDisable()
    {
        Variables.OnLanguageChanged -= RefreshDropdowns;
    }
    private void RefreshDropdowns()
    {
        RefreshDropdown(sorting, sortingIds);
        RefreshDropdown(filtering, filteringIds);
    }
    private void RefreshDropdown(TMP_Dropdown dropdown, string[] ids)
    {
        int current = dropdown.value;
        dropdown.options.Clear();
        foreach (var id in ids)
            dropdown.options.Add(new TMP_Dropdown.OptionData(DataTableManager.StringTable.Get(id)));
        dropdown.value = current;
        dropdown.RefreshShownValue();
    }
    public void OnChangeSorting(int index)
    {
        uiCharacterSlotList.Sorting = (UiCharacterSlotList.SortingOptions)index;
        SaveLoadManager.Data.CharacterSorting = (UiCharacterSlotList.SortingOptions)index;
    }
    public void OnChangeFiltering(int index)
    {
        uiCharacterSlotList.Filtering = (UiCharacterSlotList.FilteringOptions)index;
        SaveLoadManager.Data.CharacterFiltering = (UiCharacterSlotList.FilteringOptions)index;
    }
    public void OnSave()
    {
        SaveLoadManager.Data.CharacterList = uiCharacterSlotList.GetSaveItemDataList();
        SaveLoadManager.Save();
    }
    public void OnLoad()
    {
        SaveLoadManager.Load();
        uiCharacterSlotList.SetSaveItemDataList(SaveLoadManager.Data.CharacterList);
    }
    public void OnCreateCharacter()
    {
        uiCharacterSlotList.AddRandomCharacter();
    }
    public void OnRemoveCharacter()
    {
        uiCharacterSlotList.RemoveCharacter();
    }
}