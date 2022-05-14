using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// allows user to quit the game when the button is clicked
public class LeaveGame : MonoBehaviour
{
// exits the app when the user clicks the quit button
  public void quitGame()
    {
        Application.Quit();
    }
}
