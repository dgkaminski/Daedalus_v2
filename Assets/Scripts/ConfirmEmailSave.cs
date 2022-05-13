using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmEmailSave : MonoBehaviour
{
    public string emailString;
    public GameObject inputField;
    // Saves the email that the user inputs using the toString method and stores the email using persistent storage
    public void StoreEmail(GameObject input)
    {
        emailString = input.GetComponent<Text>().ToString();
        PersistentStorage.emailSave(emailString);
    }
}
