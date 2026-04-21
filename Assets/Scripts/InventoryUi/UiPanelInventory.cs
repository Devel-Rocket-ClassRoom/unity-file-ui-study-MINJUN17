using TMPro;
using UnityEngine;

public class UiPanelInventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;
    public UiInvenSlotList uiInvenSlotList;
    private readonly string[] sortingIds = { "Sort_TimeAsc", "Sort_TimeDesc", "Sort_NameAsc", "Sort_NameDesc", "Sort_CostAsc", "Sort_CostDesc" };
    private readonly string[] filteringIds = { "Filter_None", "Filter_Weapon", "Filter_Equip", "Filter_Consumable", "Filter_NonConsumable"};

    private void Awake()
    {
        uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);
    }
    private void OnEnable()
    {
        Variables.OnLanguageChanged += RefreshDropdowns;
        RefreshDropdowns();
        OnLoad();
        sorting.value = (int)SaveLoadManager.Data.Sorting;
        filtering.value = (int)SaveLoadManager.Data.Filtering;
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
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(DataTableManager.StringTable.Get(id)));
        }
        dropdown.value = current;
        dropdown.RefreshShownValue();
    }
    public void OnChangeSorting(int index)
    {
        uiInvenSlotList.Sorting = (UiInvenSlotList.SortingOptions)index;
        SaveLoadManager.Data.Sorting = (UiInvenSlotList.SortingOptions)index;
    }
    public void OnChangeFiltering(int index)
    {
        uiInvenSlotList.Filtering = (UiInvenSlotList.FilteringOptions)index;
        SaveLoadManager.Data.Filtering = (UiInvenSlotList.FilteringOptions)index;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.ItemList = uiInvenSlotList.GetSaveItemDataList();
        SaveLoadManager.Save();
    }
    public void OnLoad()
    {
        SaveLoadManager.Load();
        uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);
    }
    public void OnCreateItem()
    {
        uiInvenSlotList.AddRandomItem();
    }
    public void OnRemoveItem()
    {
        uiInvenSlotList.RemoveItem();
    }
   
}
