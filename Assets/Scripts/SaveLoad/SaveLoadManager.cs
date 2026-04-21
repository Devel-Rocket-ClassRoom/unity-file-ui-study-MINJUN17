using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using SaveDataVC = SaveDataV5;


public static class SaveLoadManager
{
    public enum SaveMode
    {
        Text, //(.json)
        Encrypted // (.dat)
    }
    public static SaveMode Mode { get; set; } = SaveMode.Encrypted;
    public static int SaveDataVersion { get; } = 5;

    private static readonly string SaveDirectory = $"{Application.persistentDataPath}/Save";

    private static readonly string[] SaveFileNames =
    {
        "SaveAuto",
        "Save1",
        "Save2",
        "Save3",
    };
    public static SaveDataVC Data { get; set; } = new SaveDataVC();

    static SaveLoadManager()
    {
        if (!Load())
        {
            Debug.LogError("세이브파일 로드 실패");
        }
    }

    private static string GetSaveFilePath(int slot, SaveMode mode)
    {
        var ext = mode == SaveMode.Text ? ".json" : ".dat";
        return Path.Combine(SaveDirectory, $"{SaveFileNames[slot]}{ext}");
    }
    private static string GetSaveFilePath(int slot)
    {
        return GetSaveFilePath(slot, Mode);
    }

    private static JsonSerializerSettings settings = new JsonSerializerSettings()
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All,
    };
    public static bool Save(int slot = 0)
    {
        return Save(slot, Mode);
    }

    public static bool Save(int slot, SaveMode mode)
    {
        if (Data == null || slot < 0 || slot >= SaveFileNames.Length)
        {
            return false;
        }
        try
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }
            var json = JsonConvert.SerializeObject(Data, settings);
            string path = GetSaveFilePath(slot, mode);
            switch (Mode)
            {
                case SaveMode.Text:
                    File.WriteAllText(path, json);
                    break;
                case SaveMode.Encrypted:
                    File.WriteAllBytes(path, CryptoUtil.Encrypt(json));
                    break;
            }
            return true;
        }
        catch
        {
            Debug.LogError("Save 예외");
            return false;
        }
    }
    public static bool Load(int slot = 0)
    {
        return Load(slot, Mode);
    }
    public static bool Load(int slot, SaveMode mode)
    {
        if (slot < 0 || slot >= SaveFileNames.Length)
        {
            return false;
        }
        string path = GetSaveFilePath(slot, mode);
        if (!File.Exists(path))
        {
            return Save();
        }
        try
        {
            string json = string.Empty;
            switch (Mode)
            {
                case SaveMode.Text:
                    json = File.ReadAllText(path);
                    break;
                case SaveMode.Encrypted:
                    json = CryptoUtil.Decrypt(File.ReadAllBytes(path));
                    break;
            }
            var saveData = JsonConvert.DeserializeObject<SaveData>(json, settings);
            while (saveData.Version < SaveDataVersion)
            {
                Debug.Log(saveData.Version);
                saveData = saveData.VersionUp();
                Debug.Log(saveData.Version);
            }
            Data = saveData as SaveDataVC;
            Debug.Log(path);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Load 예외: {e.Message}\n{e.StackTrace}");
            return false;
        }
    }

}
