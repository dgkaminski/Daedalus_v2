using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtons : MonoBehaviour
{
    // Start is called before the first frame update
    private MazeRenderer mazeRenderer;
    private ChangeScene change;

    
    void Start()
    {
        mazeRenderer = GameObject.Find("MazeRenderer").GetComponent<MazeRenderer>();
        change = GameObject.Find("ChangeScene").GetComponent<ChangeScene>();
    }

    void ChangeScene(string sceneName)
    {
        change.btn_change_scene(sceneName);
    }

    void StartMaze()
    {
        mazeRenderer.StartMaze();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
