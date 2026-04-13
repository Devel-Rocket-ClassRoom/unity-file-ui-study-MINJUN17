using System.Collections.Generic;
using UnityEngine;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();
    public static StringTable StringTable => Get<StringTable>(DataTableIds.String);

    static DataTableManager()
    {
        Init();
    }
#if UNITY_EDITOR
    public static StringTable GetStringTable(Languages lang)
    {
        return Get<StringTable>(DataTableIds.StringTableIds[(int)lang]);
    }
#endif
    private static void Init()
    {
#if !UNITY_EDITOR
        var stringTable = new StringTable();
        stringTable.Load(DataTableIds.String);
        tables.Add(DataTableIds.String, stringTable);
#else
        foreach (var id in DataTableIds.StringTableIds)
        {
            var stringTable = new StringTable();
            stringTable.Load(id);
            tables.Add(id, stringTable);

        }
#endif
    }
    public static void ChangeLanguage(Languages lang)
    {
        if (tables.ContainsKey(DataTableIds.StringTableIds[(int)lang]))
            return;
        string oldId = string.Empty;
        foreach(var id in DataTableIds.StringTableIds)
        {
            if (tables.ContainsKey(id))
            {
                oldId = id;
                break;
            }
        }
        var stringTable = tables[oldId];
        stringTable.Load(DataTableIds.StringTableIds[(int)lang]);
        tables.Remove(oldId);
        tables.Add(DataTableIds.String, stringTable);
    }

    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Debug.Log("테이블 없음");
        }
        return tables[id] as T;
    }
}
