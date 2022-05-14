using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spawns the player and places it in the maze

public class Spawn : MonoBehaviour
{

    [SerializeField]
    private Transform player = null;

    [SerializeField]
    [Range(1, 100)]
    private int width;

    [SerializeField]
    [Range(1, 100)]
    private int height;

    // Start is called before the first frame update
    void Start()
    {
        //player.position = new Vector3(-width / 2, 0, -height / 2); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
