using System.IO;
using UnityEngine;

public class FileEncryption : MonoBehaviour
{
    void Start()
    {
        //1
        string path = Path.Combine(Application.persistentDataPath, "secret.txt");
        string hello = "helloWorld";
        File.WriteAllText(path, hello);

        //2
        using (FileStream fs = File.OpenRead(Path.Combine(path)))
        {
            int byteData = fs.ReadByte();
            while(byteData > 0)
            {
                byteData ^= 0xAB;
                byteData = fs.ReadByte();
            }
        } 

    }
}
