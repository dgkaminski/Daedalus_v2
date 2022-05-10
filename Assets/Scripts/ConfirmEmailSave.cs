using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmEmailSave : MonoBehaviour
{
    public string emailString;
    public GameObject inputField;
    public void StoreEmail(GameObject input)
    {
        emailString = input.GetComponent<Text>().ToString();
        PersistentStorage.emailSave(emailString);
    }
}
