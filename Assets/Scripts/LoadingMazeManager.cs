using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMazeManager : MonoBehaviour
{
    // Loads the scene in the next build index by adding 1 to the current buildIndex
    public void LoadNextScene()
    {
        //SceneManager.LoadScene("MazeTest_v1.0.0");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
