using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class KeyValueSetting : MonoBehaviour
{
    void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "settings.cfg");
        File.WriteAllText(path, "master_volume=80\nbgm_volume=70\nsfx_volume=90\nlanguage=kr\nshow_damage=true");

        Dictionary<string, string> settings = new Dictionary<string, string>();
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('=');
                settings[parts[0]] = parts[1];
            }
        }
        Debug.Log($"설정 로드 완료 (항목 {settings.Count}개)");

        Debug.Log("--- 변경 전 ---");
        Debug.Log($"bgm_volume = {settings["bgm_volume"]}");
        Debug.Log($"language = {settings["language"]}");

        settings["bgm_volume"] = "50";
        settings["language"] = "en";

        using (StreamWriter writer = new StreamWriter(path))
        {
            foreach (var pair in settings)
            {
                writer.WriteLine($"{pair.Key}={pair.Value}");
            }
        }

        Debug.Log("--- 변경 후 저장 ---");
        Debug.Log($"bgm_volume = {settings["bgm_volume"]}");
        Debug.Log($"language = {settings["language"]}");

        Debug.Log("--- 최종 파일 내용 ---");
        Debug.Log(File.ReadAllText(path));
    }
}
