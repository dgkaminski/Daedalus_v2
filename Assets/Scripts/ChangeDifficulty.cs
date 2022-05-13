using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDifficulty : MonoBehaviour
{
    float time;

    MazeRenderer mazeRenderer;
    /*void Start()
    {
       mazeRenderer = GameObject.Find("MazeRenderer").GetComponent<MazeRenderer>();
        time = 0;
    }*/
    // set height and width of maze to 20 when user selects easy difficulty level
    public void SetEasy()
    {
        // mazeRenderer = GetComponent<MazeRenderer>();
        mazeRenderer = GameObject.Find("MazeRenderer").GetComponent<MazeRenderer>();
        mazeRenderer.height = 20;
        mazeRenderer.width = 20;
        Debug.Log("Set difficulty to \"Easy\".");
    }

    // set height and width of maze to 30 when user selects medium difficulty level
    public void SetMedium()
    {
        //mazeRenderer = GameObject.Find("MazeRenderer").GetComponent<MazeRenderer>();
        mazeRenderer.height = 30;
        mazeRenderer.width = 30;
        Debug.Log("Set difficulty to \"Medium\".");
    }

    // set height and width of maze to 40 when user selects hard difficulty level
    public void SetHard()
    {
        //mazeRenderer = GameObject.Find("MazeRenderer").GetComponent<MazeRenderer>();
        mazeRenderer.height = 40;
        mazeRenderer.width = 40;
        Debug.Log("Set difficulty to \"Hard\".");
    }

    // set height and width of maze to 60 when user selects extreme difficulty level
    public void SetExtreme()
    {
        //mazeRenderer = GameObject.Find("MazeRenderer").GetComponent<MazeRenderer>();
        mazeRenderer.height = 60;
        mazeRenderer.width = 60;
        Debug.Log("Set difficulty to \"Extreme\".");
    }

    /*public void Update()
    {
        time += Time.deltaTime;
        if (time > 5)
        {
            GameObject.Find("ChangeScene").GetComponent<ChangeScene>().btn_change_scene("Maze");
        }
    }*/
}