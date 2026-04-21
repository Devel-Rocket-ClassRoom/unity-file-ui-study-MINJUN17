using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;
using System.ComponentModel;

public class UiInvenSlotList : MonoBehaviour
{
    public enum SortingOptions
    {
        CreationTimeAscending,
        CreationTimeDescending,
        NameAscending,
        NameDescending,
        CostAscending,
        CostDescending
    }

    public enum FilteringOptions
    {
        None,
        Weapon,
        Equip,
        Consumable,
        NonConsumable
    }

    public readonly System.Comparison<SaveItemData>[] comparison =
    {
        (lhs, rhs) => lhs.CreationTime.CompareTo(rhs.CreationTime),
        (lhs, rhs) => rhs.CreationTime.CompareTo(lhs.CreationTime),
        (lhs, rhs) => lhs.ItemData.StringName.CompareTo(rhs.ItemData.StringName),
        (lhs, rhs) => rhs.ItemData.StringName.CompareTo(lhs.ItemData.StringName),
        (lhs, rhs) => lhs.ItemData.Cost.CompareTo(rhs.ItemData.Cost),
        (lhs, rhs) => rhs.ItemData.Cost.CompareTo(lhs.ItemData.Cost),
    };

    public readonly System.Func<SaveItemData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.ItemData.Type == ItemType.Weapon,
        (x) => x.ItemData.Type == ItemType.Equip,
        (x) => x.ItemData.Type == ItemType.Consumable,
        (x) => x.ItemData.Type != ItemType.Consumable,
    };

    public UiInvenSlot prefab;
    public ScrollRect scrollRect;

    private List<UiInvenSlot> uiSlotList = new List<UiInvenSlot>();
    public int uiSlotMaxCount = 50;

    private List<SaveItemData> saveItemDataList = new List<SaveItemData>();

    private SortingOptions sorting = SortingOptions.CreationTimeAscending;
    private FilteringOptions fitering = FilteringOptions.None;

    public SortingOptions Sorting
    {
        get => sorting;
        set
        {
            if (sorting != value)
            {
                sorting = value;
                UpdateSlots();
            }
        }
    }
    public FilteringOptions Filtering
    {
        get => fitering;
        set
        {
            if (fitering != value)
            {
                fitering = value;
                UpdateSlots();
            }
        }
    }
    private int seletedSlotIndex = -1;

    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveItemData> onSelectSlot;
    private void UpdateSlots()
    {
        var list = saveItemDataList.Where(filterings[(int)fitering]).ToList();
        list.Sort(comparison[(int)sorting]);
        if (uiSlotList.Count < list.Count)
        {
            for(int i = uiSlotList.Count; i < list.Count; i++)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    seletedSlotIndex = newSlot.slotIndex;
                    onSelectSlot.Invoke(newSlot.SaveItemData);
                });

                uiSlotList.Add(newSlot);
            }
        }

        for(int i = 0; i < uiSlotList.Count; ++i)
        {
            if(i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetItem(list[i]);
            }
            else
            {
                uiSlotList[i].gameObject.SetActive(false);
                uiSlotList[i].SetEmpty();
            }
        }
        seletedSlotIndex = -1;
        onUpdateSlots.Invoke();
    }
    private void OnSelectSlot(SaveItemData saveItemData)
    {
        Debug.Log(saveItemData);
    }
    private void Start()
    {
        onSelectSlot.AddListener(OnSelectSlot);
    }
    public List<SaveItemData> GetSaveItemDataList()
    {
        return saveItemDataList;
    }
    private void OnDisable()
    {
        saveItemDataList = null;
    }
    public void SetSaveItemDataList(List<SaveItemData> source)
    {
        saveItemDataList = source.ToList();
        UpdateSlots();
    }
    
    public void AddRandomItem()
    {
        saveItemDataList.Add(SaveItemData.GetRandomItem());
        UpdateSlots();
    }
    public void RemoveItem()
    {
        if(seletedSlotIndex == -1)
        {
            return;
        }
        saveItemDataList.Remove(uiSlotList[seletedSlotIndex].SaveItemData);
        UpdateSlots();
    }
}
