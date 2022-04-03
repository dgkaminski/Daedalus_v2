using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    public enum TypeOfMovement { vertical, horizontal};
    public TypeOfMovement movement;

    public float speed=0.5f;
    public float amplitude;

    public Vector3 originPos;


    void Start()
    {
        originPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (movement == TypeOfMovement.horizontal)
        {
            transform.position= originPos+new Vector3(amplitude*Mathf.Sin(speed*Time.fixedTime),0,0);
        }
       
        if(movement==TypeOfMovement.vertical)
        {
            transform.position=originPos + new Vector3(0, amplitude*Mathf.Sin(speed * Time.fixedTime), 0);
        }


    }
}
