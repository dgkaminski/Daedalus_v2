﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// persistent storage using the user inputed email

public class GetAndSetInputField : MonoBehaviour
{
    public InputField email;
// saves the email that the user inputed using persistent storage
 public void setGet() 
    {
        email = GameObject.Find("InputField").GetComponent<InputField>();
        PersistentStorage.emailSave(email.text);
    }
}
