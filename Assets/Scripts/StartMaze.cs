using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMaze : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        MazeRenderer mazeRenderer = GameObject.Find("MazeRenderer").GetComponent<MazeRenderer>();
        Debug.Log("The object was found");
        mazeRenderer.StartMaze();
        Debug.Log("The maze was attempted to be generated");
    }
}
