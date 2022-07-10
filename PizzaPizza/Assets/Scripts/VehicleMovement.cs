using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using System.Runtime.CompilerServices;

public class VehicleMovement : MonoBehaviour
{
    //the input system
    private NewControls inputs;
    //the acceleration/decceleration from input
    public float speed;
    //the turning from input
    public float turn;
    [Tooltip("the vehicle's max speed")]
    public float maxSpeed;
    //[Tooltip("the vehicle's min speed. 0 if can't back up, otherwise make it negative.")]
    //public float minSpeed;
    [Tooltip("the vehicle's max forward acceleration per frame")]
    public float forwardAcceleration;
    [Tooltip("the vehicle's max backward acceleration per frame")]
    public float backwardAcceleration;
    [Tooltip("the vehicle's max turn speed per frame while moving")]
    public float movingTurnSpeed;
    [Tooltip("the vehicle's max turn speed per frame while not moving")]
    public float standingTurnSpeed;
    //the vehicle's rigidbody to follow
    public Rigidbody rb;
    [Tooltip("the force for a jump")]
    public float jumpForce;

    [Header("gravity/grounded variables")]
    [Tooltip("gravity on the car")]
    public float gravityMagnitude;
    [Tooltip("If the car is grounded or not")]
    private bool grounded;
    [Tooltip("The layer(s) which constitute as ground")]
    public LayerMask groundLayer;
    [Tooltip("the length of the ground ray layer")]
    public float groundRayLength;
    [Tooltip("The point where the raycast is cast from")]
    public Transform groundRayPoint;
    [Tooltip("grounded drag")]
    public float groundDrag = 3f;
    [Tooltip("aerial drag")]
    public float airDrag = .1f;
    [Tooltip("the rate at which the car tilts forward when in the air. Should be small")]
    public float airtilt = 5;

    //Animation Controls
    public Animator scootAnimator;
    public Animator playerAnimator;

    //VFX
    public ParticleSystem scootForwardDust;
    public ParticleSystem scootBackwardDust;
    public ParticleSystem scootLandDust;

    //In air tracking
    private bool wasInAir = false;
    private float landTime = 1f;
    private float landTimer = 0f;

    //Turn Tilting stuff
    public GameObject tiltObj;
    public float targetTiltAngle;



    //previous Quaternion heading. used for aerial camera.
    public Vector3 prevHeading;
    // Start is called before the first frame update
    void Start()
    {
        //set up inputs
        inputs = new NewControls();
        inputs.Vehicle.AccelerateBrake.performed += ctx => speed = ctx.ReadValue<float>();
        
        inputs.Vehicle.AccelerateBrake.canceled += ctx => speed = 0;
        inputs.Vehicle.Turn.performed += ctx => turn = ctx.ReadValue<float>();
        inputs.Vehicle.Turn.canceled += ctx => turn = 0;
        //get rigidbody
        //rb = gameObject.GetComponent<Rigidbody>();
        rb.transform.parent = null;

        prevHeading = new Vector3(transform.forward.x, 0, transform.forward.z);

        scootAnimator.Play("Blend Tree");
        //playerAnimator.Play("Blend Tree");
    }

    //get good stuff
    public void setSpeed(InputAction.CallbackContext newSpeed)
    {
        
        speed = newSpeed.ReadValue<float>();
        Debug.Log("speed " + speed);
    }

    //get good stuff
    public void setTurn(InputAction.CallbackContext newTurn)
    {
        //Debug.Log("turn");
        turn = newTurn.ReadValue<float>();
    }

    //jump
    

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = rb.transform.position;

        grounded = false;
        RaycastHit hit;
        if (Physics.Raycast(groundRayPoint.position, -Vector3.up, out hit, groundRayLength, groundLayer))
        {
            grounded = true;
            //if(Vector3.Angle(transform.rotation,hit.normal > 160))
            //transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal));
        }

        //Debug.Log(grounded);


        //update speed with input and friction
        //Debug.Log("speed " + speed + " mag " + rb.velocity.magnitude + " acc " + forwardAcceleration * 1000f);
        //float currSpeed = rb.velocity.magnitude;
        //if(Mathf.Abs(currSpeed) > 0f && Vector3.Angle(rb.velocity, transform.forward) > 90)
        //{
        //    currSpeed *= -1;
        //}
        //float newSpeed = Mathf.Clamp(currSpeed + (speed * forwardAcceleration), -maxSpeed, maxSpeed);
        if (grounded)
        {
            prevHeading = new Vector3(transform.forward.x, 0, transform.forward.z);
            rb.drag = groundDrag;
            if (speed > 0)
            {
                rb.AddForce(transform.forward * speed * forwardAcceleration * 1000f);
            }
            else
            {
                rb.AddForce(transform.forward * speed * backwardAcceleration * 1000f);
            }
        }
        else
        {
            rb.drag = airDrag;
            rb.AddForce(-Vector3.up * gravityMagnitude * 100f);
        }
        
        //float friction = GetFriction();
        //if(friction > Mathf.Abs(rb.velocity))
        //{
        //    friction = rb.velocity;
        //}
        //friction *= -Mathf.Sign(speed);

        //turn car with input
        if (Mathf.Abs(turn) > 0f)
        {
            Vector3 upLine = transform.up;
            if (!grounded)
            {
                upLine = Vector3.up;
            }
            float speedFactor = speed;
            if (!grounded)
            {
                speedFactor = 1;
            }
            if (Mathf.Abs(speedFactor) > 0f)
            {
                transform.RotateAround(transform.position, upLine, turn * movingTurnSpeed * speedFactor);
            }
            else
            {
                transform.RotateAround(transform.position, upLine, turn * standingTurnSpeed);
            }
            //rb.angularVelocity = Vector3.zero;

            //rb.velocity = transform.forward.normalized * newSpeed;
        }

        if (!grounded)
        {
            transform.RotateAround(transform.position, transform.right, airtilt * speed);
        }
        //apply extra gravity to car
        //rb.AddForce()

        //Tilt scooter if turning
        if (turn > 0f)
        {
            targetTiltAngle = -15f;
        }
        else if (turn < 0f)
        {
            targetTiltAngle = 15f;
        }
        else {
            targetTiltAngle = 0f;
        }

        tiltObj.transform.localRotation = Quaternion.RotateTowards(tiltObj.transform.localRotation, Quaternion.Euler(tiltObj.transform.localRotation.x, tiltObj.transform.localRotation.y, targetTiltAngle), 2f);

        //Update Animation & FX Values
        UpdateAnimation();
        UpdateVFX();

        if (!grounded)
        {
            wasInAir = true;
        }
        else {
            landTimer -= Time.deltaTime;
            if (landTimer < 0) {
                wasInAir = false;
                landTimer = 1f;
            }
        }

    }

    //gets the current ground friction that the vehicle is experiencing
    private float GetFriction()
    {
        return 0.1f;
    }

    //gets the rb
    private Rigidbody getRB()
    {
        return rb;
    }

    public float GetSpeed()
    {
        return rb.velocity.magnitude;
    }


    public Vector3 GetHeading()
    {
        //if (grounded)
        //{
        //return new Vector3(transform.forward.x, 0, transform.forward.z);
        //}
        //return prevHeading;
        return Vector3.Cross(transform.right, Vector3.up);
    }

    private void UpdateAnimation() { 

        scootAnimator.SetFloat("TurnDir", turn);
        scootAnimator.SetFloat("SpeedDir", speed);
        playerAnimator.SetFloat("TurnDir", turn);
    }

    private void UpdateVFX() {
        
        if (grounded && GetSpeed() > 0.3f)
        {
            scootBackwardDust.Stop();
            scootForwardDust.Play();
        }
        else if (grounded && GetSpeed() < -0.3f)
        {
            scootForwardDust.Stop();
            scootBackwardDust.Play();
        }
        else {
            scootForwardDust.Stop();
            scootBackwardDust.Stop();
        }
        if (grounded && wasInAir)
        {
            scootLandDust.Play();
            wasInAir = false;
        }
    }
}
