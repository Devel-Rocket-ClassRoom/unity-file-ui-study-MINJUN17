using UnityEngine;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;


public class SaveLoadTest1 : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SaveLoadManager.Data.Name = "adfa";
            SaveLoadManager.Data.Gold = 123;
            SaveLoadManager.Save(0);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if (SaveLoadManager.Load(0))
            {
                Debug.Log(SaveLoadManager.Data.Name);
                Debug.Log(SaveLoadManager.Data.Gold);
                foreach (var item in SaveLoadManager.Data.items)
                {
                    Debug.Log(item);
                }
            }
            else
            {
                Debug.Log("세이브 파일 없음");
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            int index = Random.Range(0, DataTableManager.ItemTable.table.Count);
            string randomItem = DataTableManager.ItemTable.table.Keys.ToList()[index];
            SaveLoadManager.Data.items.Add(randomItem);
        }
    }
}
