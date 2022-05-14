using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

// saves the user inputed email and stores it as a json file

public class PersistentStorage : MonoBehaviour
{
    public string emailRecepient = "daedalus.scripts@gmail.com";
    public static DirectoryInfo SafeCreateDirectory(string path)
    {
        //Generate if you don't check if the directory exists
        if (Directory.Exists(path))
        {
            return null;
        }
        Debug.Log("The directory does not exist, new one was created");
        return Directory.CreateDirectory(path);
        
    }

    
    public static void emailSave(string email)
    {
        //Data storage
        SafeCreateDirectory($"{Application.persistentDataPath}/Emails");
        string json = JsonUtility.ToJson(email);
        //StreamWriter writer = new StreamWriter($"{Application.persistentDataPath}/Emails/email.json");
        StreamWriter writer = new StreamWriter($"{Application.persistentDataPath}/Emails/email.json");
        writer.Write(email);
        Debug.Log("The email was stored");
        writer.Flush();
        writer.Close();
    }

    public static string emailReturn()
    {
        //Data acquisition
        var reader = new StreamReader($"{Application.persistentDataPath}/Emails/email.json");
        string json = reader.ReadToEnd();
        reader.Close();
        return json;//Convert for ease of use
    }
}
