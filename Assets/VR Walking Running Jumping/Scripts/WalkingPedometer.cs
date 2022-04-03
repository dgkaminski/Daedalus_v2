using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class WalkingPedometer : MonoBehaviour
{

    [Header("Walking parameters")]
    [TooltipAttribute("This is used for the low-pass filter")]
    public float fLowFilter = 0.12f;
    [TooltipAttribute("This is used for the high-pass filter")]
    public float fHighFilter = 18.0f;
    [TooltipAttribute("Used to know if we change state from low-high")]
    public float lowThreshold = 0.006f; 
    [TooltipAttribute("Used to know if we change state from low-high")]
    public float walkingThreshold = 0.1f;
    [TooltipAttribute("Used in the corrutine to move the player")]
    public float timeWalk = 0.5f;

    [Header("Speed factors")]
    [TooltipAttribute("The speed factor of the player during movement")]
    public float speedFactor=2.2f;
    [TooltipAttribute("The parameter used in the function Ae^(Bx)")]
    public float A=9.0f;
    [TooltipAttribute("The parameter used in the function Ae^(Bx)")]
    public float B=-3.0f;

    [Header("stepsCount")]
    [TooltipAttribute("The number of steps")]
    public int stepsCount = 0;


    [Header("Running parameters")]
    private float t0, t1;
    private float timeBetweenSteps;
    [TooltipAttribute("Clamp time of the steps between these two parameters")]
    public  float minTimeStep = 0.001f, maxTimeStep = 4;


    [Header("Jumping parameters")]
    [TooltipAttribute("It is compared with the value of acceleration")]
    public float jumpingThreshold = 0.17f;
    [TooltipAttribute("It is the vertical speed")]
    public float jumpSpeed = 0.6f;
    [TooltipAttribute("Set to true=allows forwad movement for jumping")]
    public float jumpForward = 4;
    [TooltipAttribute("It is the time while the player jumps")]
    public float timeJump = 0.25f;
           

    [Header("HUD parameters")]
    [TooltipAttribute("Where the steps will be displayed")]
    public Text acc;


    [Header("Condition parameters (used for animations) ")]
    public float transitionSpeed = 2;
    public bool isWalking;
    public bool isRunning;
    public bool isJumping;
    public bool isGrounded;

    //Accelerations used to trigger the steps 
    // --> current acceleration VS average acceleration
    private float currentAcceleration = 0f;
    private float averageAcceleration = 0f;
    // used to know if a step has been triggered
    bool isStepTriggered = false;

   
    //character controller used for moving the player
    private CharacterController cc;
    //the head of the player == main camera
    private  Transform head;
   
    //used to know where the player wants to go
    Vector3 objective;

    Coroutine corrWalk, corrJump;


    void Awake()
    {
        // initialize the filters and the objective
        averageAcceleration = Input.acceleration.magnitude;
        objective = transform.position;

        //set the head transform
        head = Camera.main.transform;

        //initialize the character controller
        cc = GetComponent<CharacterController>();

       
    }

    private void Update()
    {
        isGrounded = cc.isGrounded;
    }

    void FixedUpdate()
    { 
        
        // filter input.acceleration using Lerp errasing the low and high frequencies
        currentAcceleration = Mathf.Lerp(currentAcceleration, Input.acceleration.magnitude, Time.deltaTime * fHighFilter);
        averageAcceleration = Mathf.Lerp(averageAcceleration, Input.acceleration.magnitude, Time.deltaTime * fLowFilter);
        
        //delta is the value used to know the different jumps/impulses in the acceleration
        float delta = currentAcceleration - averageAcceleration;

        //in the case of editor mode, override the values in a 10% over the threshold
#if UNITY_EDITOR
        if (Input.GetKey("w"))
        {
            delta += walkingThreshold*1.1f;
        }
        if (Input.GetKey("q"))
        {
            delta += jumpingThreshold*1.1f;
        }
#endif

        // In case that is not jumping
        if (!isStepTriggered && isJumping==false)
        { 
            // when the impulse value is higher than the threshold                                    
            if (delta > walkingThreshold && delta<jumpingThreshold)
            { 


                //walk once
                isStepTriggered = true;
                stepsCount++; // count step when comp goes high 

                if (acc != null)
                {
                    acc.text = "" + stepsCount;
                }


                //get the times between steps to set the speed of walk/running
                t1 = Time.fixedTime;
                timeBetweenSteps = t1 - t0;
                timeBetweenSteps = Mathf.Clamp(timeBetweenSteps, minTimeStep, maxTimeStep);
                t0 = t1;

                //start corrutine for walking/running after checking if already exists.
                if(corrWalk != null)
                {
                    StopCoroutine(corrWalk);
                }

                corrWalk = StartCoroutine(MoveStep());


            }
            //jump if it is higher than 
            else if(delta > jumpingThreshold)
            {
                //verify jumping conditions
                if (corrJump!=null)
                {
                    if(isJumping==false)
                    {
                        corrJump = null;
                    }
                }
                else if(isGrounded==true)
                {
                    corrJump= StartCoroutine(jump());
                }


            }
        }
        else
        {
            // not triggered
            if (delta < lowThreshold)
            {
                isStepTriggered = false;

                isWalking = false;
                isRunning = false;
            }
        }

        //move player always down considering gravity if not jumping
        if (head != null && cc != null && isJumping==false)
        {
            cc.Move(-Physics.gravity.magnitude * Time.fixedDeltaTime * new Vector3(0, 1, 0));
        }
    }


    public IEnumerator MoveStep()
    {
        Vector3 headProyection = new Vector3(0, 0, 0);
        if (head != null && cc != null)
        {
            headProyection = new Vector3(head.transform.forward.x, 0.15f, head.transform.forward.z);
        }

        for (float ii=0;ii< timeWalk; ii+=Time.fixedDeltaTime) 
        {

            float displacement;

            //exponential experimental function used to estimate the displacement   
            displacement = A * Mathf.Exp(B * timeBetweenSteps);
            
            if(displacement>transitionSpeed)
            {
                isWalking = false;
                isRunning = true;
            }
            else
            {
                isWalking = true;
                isRunning = false;
            }

            // MOVE PLAYER
            if (head != null && cc != null)
            {

                cc.Move(headProyection * speedFactor* displacement * Time.fixedDeltaTime);
            }
            //RB.MovePosition(interpolate);
            yield return new WaitForFixedUpdate();

               
        }
      
    }

    public IEnumerator jump()
    {

            
        //perform jump
        isJumping = true;
        for (float ii = 0; ii < timeJump; ii += Time.fixedDeltaTime)
        {

            cc.Move(Vector3.up * jumpSpeed * ii+ head.forward * speedFactor * Time.fixedDeltaTime * jumpForward);
  
            yield return new WaitForFixedUpdate();

        }

        while(!cc.isGrounded)
        {
            cc.Move(head.forward * speedFactor * Time.fixedDeltaTime * jumpForward-Vector3.up*Physics.gravity.magnitude*Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
            
        }

        //stop jump
        isJumping = false;

    }


}