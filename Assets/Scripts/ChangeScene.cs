using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Takes a string parameter and changes the displayed screen
    public void btn_change_scene(string sceneName)
    {
        Debug.Log($"Attempted to load scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
        Debug.Log($"Loaded scene: {sceneName}");
    }
    // Loads the main menu screen
    public void mazeChange()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
