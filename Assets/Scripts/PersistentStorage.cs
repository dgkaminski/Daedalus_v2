using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PersistentStorage : MonoBehaviour
{
    public static DirectoryInfo SafeCreateDirectory(string path)
    {
        //Generate if you don't check if the directory exists
        if (Directory.Exists(path))
        {
            return null;
        }
        return Directory.CreateDirectory(path);
    }

    public void emailSave(string email)
    {
        //Data storage
        SafeCreateDirectory(Application.persistentDataPath + "/" + "DaedalusFiles");
        string json = JsonUtility.ToJson(email);
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/" + "DaedalusFiles" + "/email.json");
        writer.Write(json);
        writer.Flush();
        writer.Close();
    }

    public string emailReturn (string Directory_path)
    {
        //Data acquisition
        var reader = new StreamReader(Application.persistentDataPath + "/" + "DaedalusFiles" + "/email.json");
        string json = reader.ReadToEnd();
        reader.Close();
        return json;//Convert for ease of use
    }
}
