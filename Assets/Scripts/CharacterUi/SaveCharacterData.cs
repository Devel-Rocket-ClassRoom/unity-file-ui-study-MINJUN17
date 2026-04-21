using Newtonsoft.Json;
using System;
using UnityEngine;

public class SaveCharacterData
{
    public Guid InstanceId { get; set; }
    [JsonConverter(typeof(CharacterDataConverter))]
    public CharacterData CharacterData { get; set; }
    public DateTime CreationTime {  get; set; }
    public SaveItemData WeaponItem { get; set; } = null;
    public SaveItemData EquipItem { get; set; } = null;
    public int TotalAttack
    {
        get
        {
            if (WeaponItem == null)
            {
                return CharacterData.Attack;
            }
            else
            {
                return CharacterData.Attack + WeaponItem.ItemData.Value;
            }
        }
    }
    public int TotalDefense
    {
        get
        {
            if (EquipItem == null)
            {
                return CharacterData.Defense;
            }
            else
            {
                return CharacterData.Defense + EquipItem.ItemData.Value;
            }
        }
    }
    public static SaveCharacterData GetRandomCharacter()
    {
        SaveCharacterData newCharacter = new SaveCharacterData();
        newCharacter.CharacterData = DataTableManager.CharacterTable.GetRandom();
        return newCharacter;
    }
    public SaveCharacterData()
    {
        InstanceId = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }
    public override string ToString()
    {
        return $"{InstanceId}\n{CreationTime}\n{CharacterData.Id}";
    }
}
