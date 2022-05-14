using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroys and prevents destroying the game object when needed

public class DontDestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
   {
        for (int i = 0; i < FindObjectsOfType<DontDestroyOnLoad>().Length; i++)
        {
            if(Object.FindObjectsOfType<DontDestroyOnLoad>()[i] != this)
            {
                if (Object.FindObjectsOfType<DontDestroyOnLoad>()[i].name == gameObject.name)
                {
                    Destroy(gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
