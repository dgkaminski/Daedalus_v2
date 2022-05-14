using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// displays victory scene once the player passes through the goal

public class Victory : MonoBehaviour
{
    ChangeScene sceneChanger;

    // Start is called before the first frame update
    void Start()
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = true;
        sceneChanger = GameObject.Find("Player").GetComponent<ChangeScene>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Transforms to a victory scene and destroys the game object once the player reaches the goal
    void OnTriggerEnter(Collider collider)
    {
        string name = collider.gameObject.name;
        if (name == "Goal(Clone)" || name == "Goal")
        {
            Debug.Log($"Found {name}");
            transform.parent = null;
            foreach (Transform child in GameObject.Find("MazeRenderer").GetComponent<Transform>())
            {
                Destroy(child.gameObject);
            }
            sceneChanger.btn_change_scene("Victory");
            Destroy(gameObject);
        }
    }
}
