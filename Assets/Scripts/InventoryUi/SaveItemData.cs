using Newtonsoft.Json;
using System;
using UnityEngine;

public class SaveItemData
{
    public Guid InstanceId { get; set; }
    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData ItemData { get; set; }
    public DateTime CreationTime { get; set; }
    public static SaveItemData GetRandomItem()
    {
        SaveItemData newItem = new SaveItemData();
        newItem.ItemData = DataTableManager.ItemTable.GetRandom();
        return newItem;
    }
    public SaveItemData()
    {
        InstanceId = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }
    public override string ToString()
    {
        return $"{InstanceId}\n{CreationTime}\n{ItemData.Id}";
    }
}
