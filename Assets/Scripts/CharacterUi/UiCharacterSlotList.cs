using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiCharacterSlotList : MonoBehaviour
{
    public enum SortingOptions
    {
        CreationTimeAscending,
        CreationTimeDescending,
        AttackAscending,
        AttackDescending,
        DefenseAscending,
        DefenseDescending,
    }
    public enum FilteringOptions
    {
        None,
        Warrior,
        Mage,
        Archer,
        Rouge
    }
    public readonly System.Comparison<SaveCharacterData>[] comparison =
    {
        (lhs, rhs) => lhs.CreationTime.CompareTo(rhs.CreationTime),
        (lhs, rhs) => rhs.CreationTime.CompareTo(lhs.CreationTime),
        (lhs, rhs) => lhs.CharacterData.Attack.CompareTo(rhs.CharacterData.Attack),
        (lhs, rhs) => rhs.CharacterData.Attack.CompareTo(lhs.CharacterData.Attack),
        (lhs, rhs) => lhs.CharacterData.Defense.CompareTo(rhs.CharacterData.Defense),
        (lhs, rhs) => rhs.CharacterData.Defense.CompareTo(lhs.CharacterData.Defense),
    };
    public readonly System.Func<SaveCharacterData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.CharacterData.Name == "Character1Name",
        (x) => x.CharacterData.Name == "Character2Name",
        (x) => x.CharacterData.Name == "Character3Name",
        (x) => x.CharacterData.Name == "Character4Name"
    };
    public UiCharacterSlot prefab;
    public ScrollRect scrollRect;

    private List<UiCharacterSlot> uiSlotList = new List<UiCharacterSlot>();
    public int uiSlotMaxCount = 50;

    private List<SaveCharacterData> saveCharacterDataList = new List<SaveCharacterData>();

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
    public UnityEvent<SaveCharacterData> onSelectSlot;
    private void UpdateSlots()
    {
        var list = saveCharacterDataList.Where(filterings[(int)fitering]).ToList();
        list.Sort(comparison[(int)sorting]);
        if (uiSlotList.Count < list.Count)
        {
            for (int i = uiSlotList.Count; i < list.Count; i++)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    seletedSlotIndex = newSlot.slotIndex;
                    onSelectSlot.Invoke(newSlot.SaveCharacterData);
                });

                uiSlotList.Add(newSlot);
            }
        }

        for (int i = 0; i < uiSlotList.Count; ++i)
        {
            if (i < list.Count)
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

    private void OnSelectSlot(SaveCharacterData saveCharacterData)
    {
        Debug.Log(saveCharacterData);
    }
    private void Start()
    {
        onSelectSlot.AddListener(OnSelectSlot);
    }
    public List<SaveCharacterData> GetSaveItemDataList()
    {
        return saveCharacterDataList;
    }
    private void OnDisable()
    {
        saveCharacterDataList = null;
    }
    public void SetSaveItemDataList(List<SaveCharacterData> source)
    {
        saveCharacterDataList = source.ToList();
        UpdateSlots();
    }

    public void AddRandomCharacter()
    {
        saveCharacterDataList.Add(SaveCharacterData.GetRandomCharacter());
        UpdateSlots();
    }
    public void RemoveCharacter()
    {
        if (seletedSlotIndex == -1)
        {
            return;
        }
        saveCharacterDataList.Remove(uiSlotList[seletedSlotIndex].SaveCharacterData);
        UpdateSlots();
    }
}
