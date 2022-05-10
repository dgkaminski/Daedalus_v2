using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDifficulty : MonoBehaviour
{
    MazeRenderer mazeRenderer;
    void Start()
    {
       mazeRenderer = GameObject.Find("MazeRenderer").GetComponent<MazeRenderer>();

    }
    public void SetEasy()
    {
        // mazeRenderer = GetComponent<MazeRenderer>();
        mazeRenderer = GameObject.Find("MazeRenderer").GetComponent<MazeRenderer>();
        mazeRenderer.height = 20;
        mazeRenderer.width = 20;
        Debug.Log("Set difficulty to \"Easy\".");
    }
    public void SetMedium()
    {
        mazeRenderer = GameObject.Find("MazeRenderer").GetComponent<MazeRenderer>();
        mazeRenderer.height = 30;
        mazeRenderer.width = 30;
        Debug.Log("Set difficulty to \"Medium\".");
    }
    public void SetHard()
    {
        mazeRenderer = GameObject.Find("MazeRenderer").GetComponent<MazeRenderer>();
        mazeRenderer.height = 40;
        mazeRenderer.width = 40;
        Debug.Log("Set difficulty to \"Hard\".");
    }
    public void SetExtreme()
    {
        mazeRenderer = GameObject.Find("MazeRenderer").GetComponent<MazeRenderer>();
        mazeRenderer.height = 60;
        mazeRenderer.width = 60;
        Debug.Log("Set difficulty to \"Extreme\".");
    }
}