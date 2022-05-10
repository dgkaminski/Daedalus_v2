using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEmail : MonoBehaviour
{
    private string takenIn;
   public void saveEmail(string input)
    {
        takenIn = input;
        EmailFactory emailFactory = new EmailFactory();
        ;
        Debug.Log(takenIn);
    }
}
