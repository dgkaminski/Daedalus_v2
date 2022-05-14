using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// saves the user email if the inputField is not null

public class SaveEmail : MonoBehaviour
{
    /* private void Start()
     {
         var input = gameObject.GetComponent<InputField>();
         var se = new InputField.SubmitEvent();
         se.AddListener(saveEmail);
         input.onEndEdit = se;
         //takenIn = input;
         PersistentStorage.emailSave(takenIn);
     }
    private string takenIn;
    public void saveEmail(string input)
     {
         takenIn = input;
         PersistentStorage.emailSave(input);
         Debug.Log("The persistent storage had" + input + "stored in it");
     }*/
    // Saves email that user inputs using persistent storage if inputField is not null
    public void sendToStorage() 
    {
        //string defaultEmail = "daedalus.scripts@gmail.com";
        InputField _inputField = GameObject.Find("Email InputField").GetComponent<InputField>();
        if (_inputField != null)
        { 
                PersistentStorage.emailSave(_inputField.text);
            Debug.Log("Text sent to stoarage, text is: " + _inputField.text);
        }
    }

}
