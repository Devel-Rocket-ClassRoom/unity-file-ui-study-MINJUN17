using UnityEngine;
using System.IO;

public class SaveFileManager : MonoBehaviour
{
    void Start()
    {

        //1
        string path = Path.Combine(Application.persistentDataPath, "SaveData");
        Directory.CreateDirectory(path);

        string textPath1 = Path.Combine(path, "save1.txt");
        string config1 = "dafse1";
        File.WriteAllText(textPath1, config1);

        string textPath2 = Path.Combine(path, "save2.txt");
        string config2 = "dafsewae1";
        File.WriteAllText(textPath2, config2);

        string textPath3 = Path.Combine(path, "save3.txt");
        string config3 = "dafse1asd";
        File.WriteAllText(textPath3, config3);

        //2
        string[] files = Directory.GetFiles(path);
        foreach(string file in files)
        {
            Debug.Log($"파일: {Path.GetFileName(file)}");
        }

        //3
        string copytextPath = Path.Combine(path, "save1_backup.txt");
        File.Copy(textPath1, copytextPath, true);

        //4
        File.Delete(textPath3);

        //5
        files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            Debug.Log($"파일: {Path.GetFileName(file)}");
        }
        Debug.Log(Application.persistentDataPath);
    }
}
