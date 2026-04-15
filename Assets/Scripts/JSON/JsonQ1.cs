using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SomeClass
{
    public int typeIndex;
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;
}

public class JsonQ1 : MonoBehaviour
{
    public string fileName = "test.json";
    public string FileFullPath => Path.Combine(Application.persistentDataPath, "JsonTest", fileName);
    private JsonSerializerSettings jsonSetting;
    private List<GameObject> objects = new List<GameObject>();
    private List<int> types = new List<int>();
    private int RandomCreateCount;
    public GameObject[] prefabs;

    private void Awake()
    {
        jsonSetting = new JsonSerializerSettings();
        jsonSetting.Formatting = Formatting.Indented;
        jsonSetting.Converters.Add(new Vector3Converter());
        jsonSetting.Converters.Add(new ColorConverter());
        jsonSetting.Converters.Add(new QuaternionConverter());
    }

    public void Save()
    {
        List<SomeClass> list = new List<SomeClass>();
        for (int i = 0; i < objects.Count; i++)
        {
            SomeClass data = new SomeClass();
            data.typeIndex = types[i];
            data.pos = objects[i].transform.position;
            data.rot = objects[i].transform.rotation;
            data.scale = objects[i].transform.localScale;
            data.color = objects[i].GetComponent<MeshRenderer>().material.color;
            list.Add(data);
        }
        string json = JsonConvert.SerializeObject(list, jsonSetting);
        File.WriteAllText(FileFullPath, json);
    }

    public void Load()
    {
        ClearAll();
        string json = File.ReadAllText(FileFullPath);
        List<SomeClass> list = JsonConvert.DeserializeObject<List<SomeClass>>(json, jsonSetting);
        foreach (var data in list)
        {
            GameObject obj = Instantiate(prefabs[data.typeIndex]);
            obj.transform.position = data.pos;
            obj.transform.rotation = data.rot;
            obj.transform.localScale = data.scale;
            obj.GetComponent<MeshRenderer>().material.color = data.color;
            objects.Add(obj);
            types.Add(data.typeIndex);
        }
    }

    public void RandomCreate()
    {
        RandomCreateCount = Random.Range(3, 10);
        for (int i = 0; i < RandomCreateCount; i++)
        {
            int typeIndex = Random.Range(0, prefabs.Length);
            GameObject obj = Instantiate(prefabs[typeIndex]);
            obj.transform.position = new Vector3(Random.Range(0f, 10f), Random.Range(0f, 10f), Random.Range(0f, 10f));
            obj.transform.localScale = new Vector3(Random.Range(0f, 2f), Random.Range(0f, 2f), Random.Range(0f, 2f));
            obj.transform.rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
            obj.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            objects.Add(obj);
            types.Add(typeIndex);
        }
    }

    public void ClearAll()
    {
        foreach (var obj in objects)
        {
            Destroy(obj);
        }
        objects.Clear();
        types.Clear();
    }
}