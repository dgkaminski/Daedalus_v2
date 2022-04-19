using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMazeManager : MonoBehaviour
{
    public void LoadNextScene()
    {
        //SceneManager.LoadScene("MazeTest_v1.0.0");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
