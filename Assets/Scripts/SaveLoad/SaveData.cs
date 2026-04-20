using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsLayers;

[System.Serializable]
public abstract class SaveData
{
    public int Version { get; protected set; }
    public abstract SaveData VersionUp();
}

[System.Serializable]
public class SaveDataV1 : SaveData
{
    public string PlayerName {  get; set; } = string.Empty;
    public SaveDataV1()
    {
        Version = 1;
    }
    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV2();
        saveData.Name = PlayerName;
        saveData.Gold = 0;
        return saveData;
    }
}
[System.Serializable]
public class SaveDataV2 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold { get; set; } = 0;
    public SaveDataV2()
    {
        Version = 2;
    }
    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV3();
        saveData.Name = Name;
        saveData.Gold = Gold;
        return saveData;
    }
}
[System.Serializable]
public class SaveDataV3 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold { get; set; } = 0;
    public List<string> items { get; set; } = new List<string>();
    public SaveDataV3()
    {
        Version = 3;
    }
    public override SaveData VersionUp()
    {
        SaveDataV4 data = new SaveDataV4();
        //data.Name = Name;
        //data.Gold = Gold;
        foreach (string id in items)
        {
            SaveItemData itemData = new SaveItemData();
            itemData.ItemData = DataTableManager.ItemTable.Get(id);
            data.ItemList.Add(itemData);
        }
        return data;
    }

}
[System.Serializable]
public class SaveDataV4 : SaveDataV2
{
    public List<SaveItemData> ItemList = new List<SaveItemData>();
    public SaveDataV4()
    {
        Version = 4;
    }
    public override SaveData VersionUp()
    {
        throw new System.NotImplementedException();
    }
}


