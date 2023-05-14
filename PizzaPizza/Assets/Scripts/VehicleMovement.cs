using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.Animations;
using System.Runtime.CompilerServices;

public class VehicleMovement : MonoBehaviour
{
    //the input system
    private NewControls inputs;
    //the acceleration/decceleration from input
    public float speed;
    //the flipping from input
    public float flippingSpeed;
    //the turning from input
    public float turn;
    [Header("movement variables")]
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
    [Header("drag")]
    [Tooltip("grounded drag")]
    public float groundDrag = 3f;
    [Tooltip("aerial drag")]
    [Header("stunt variables")]
    public bool stunting = false;
    public float airDrag = .1f;
    [Tooltip("the rate at which the car tilts forward when in the air. Should be small")]
    public float airtilt = 5f;
    [Tooltip("the low point at which the player will respawn")]
    public float resetDepth = -100f;
    [Tooltip("The height to respawn the player above the ground")]
    public Vector3 resetPos = new Vector3(0, 10, 0);

    [Tooltip("previous up")]
    public Vector3 prevUp;
    [Tooltip("made half a flip")]
    public float turnAngle;
    [Tooltip("made a full flip")]
    public bool flipped = false;
    [Tooltip("degrees of freedom for good landing")]
    public float landAngles = 20;

    [Header("jump settings")]
    [SerializeField, Tooltip("if the vespa is able to jump or not")]
    private bool canJump = false;
    [SerializeField,Tooltip("the number of in-air jumps the vespa can perform")]
    private int numAirJumps = 0;
    private int airJumpsLeft = 0;
    [SerializeField,Tooltip("the force that a grounded jump pushes up with")]
    private float groundJumpForce = 1000f;
    [SerializeField,Tooltip("the force that an aerial jump pushes up with")]
    private float airJumpForce = 500f;
    private bool jumpPressed = false;

    [Header("other")]
    [Tooltip("camera script")]
    public CameraFollow cam;

    //Animation Controls
    public Animator scootAnimator;
    public Animator playerAnimator;

    //VFX
    public ParticleSystem scootForwardDust;
    public ParticleSystem scootBackwardDust;
    public ParticleSystem scootLandDust;

    //SFX
    public AudioSource scootIdle;
    public AudioSource scootBump;
    public AudioSource scootSkrt;
    public AudioSource scootRev;

    //In air tracking
    [Tooltip("if the car was in the air last frame")]
    private bool wasInAir = false;
    private float landTime = 1f;
    private float landTimer = 0f;

    //Turn Tilting stuff
    public GameObject tiltObj;
    public float targetTiltAngle;


    //slowdown stuff
    public bool slowed = false;
    public float slowTime = 2f;
    public float slowTimer = 0f;

    //boost stuff
    public bool boost = false;
    public float boostTime = 1f;
    public float boostTimer = 0f;
    public float boostForce = 8f;



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
        // inputs.Vehicle.Stunt.performed += Stunt;
        // inputs.Vehicle.Stunt.performed += ctx => stunting = true;
        // inputs.Vehicle.Stunt.performed += ctx => stunting = false;
        //get rigidbody
        //rb = gameObject.GetComponent<Rigidbody>();
        rb.transform.parent = null;

        prevHeading = new Vector3(transform.forward.x, 0, transform.forward.z);

        scootAnimator.Play("Blend Tree");
        //playerAnimator.Play("Blend Tree");
        prevUp = transform.up;
        ResetJumps();
    }

    //tries to jump
    public void TryJump(CallbackContext ctx){
        if(ctx.ReadValue<float>() < 0.5f){
            //Debug.Log("let go of jump");
            jumpPressed = false;
            return;
        }
        if(canJump && !jumpPressed){
            jumpPressed = true;
            if(grounded){
                Jump(groundJumpForce);
                grounded = false;
            }
            else if(airJumpsLeft > 0){
                Jump(airJumpForce);
                airJumpsLeft--;
                grounded = false;
            }
            
        }
    }

    //applies the jump force
    public void Jump(float jumpForce){
        Debug.Log("jump");
        ApplyJumpForce(jumpForce);
    }

    //resets the number of in-air jumps
    public void ResetJumps(){
        airJumpsLeft = numAirJumps;
    }

    public void Stunt(CallbackContext ctx){
        //Debug.Log("stunting");
        stunting = ctx.ReadValue<float>() > 0.5f;
    }

    //get good stuff
    public void setSpeed(InputAction.CallbackContext newSpeed)
    {
        
        speed = newSpeed.ReadValue<float>();
        //Debug.Log("speed " + speed);
    }

    public void setFlipSpeed(InputAction.CallbackContext newSpeed)
    {
        if(flippingSpeed != newSpeed.ReadValue<float>()){
            flippingSpeed = newSpeed.ReadValue<float>();
            //Debug.Log("flipping speed " + flippingSpeed);
        }
        
    }

    //get good stuff
    public void setTurn(InputAction.CallbackContext newTurn)
    {
        if(turn != newTurn.ReadValue<float>()){
            turn = newTurn.ReadValue<float>();
            //Debug.Log("TURN " + turn);
        }
    }

    //jump
    

    // Update is called once per frame
    private void FixedUpdate()
    {
        //properly adjust the slowdown this frame
        float slowMod = 1;
        if (slowed)
        {
            slowMod = 0.5f;
        }
        if (GameManager.instance && GameManager.instance.IsPaused())
        {
            return;
        }
        transform.position = rb.transform.position;

        if (slowTimer <= 0f)
        {
            slowed = false;
        }
        else {
            slowTimer -= Time.fixedDeltaTime;
        }

        if (boost)
        {
            boostTimer -= Time.fixedDeltaTime;
            if(boostTimer <= 0f)
            {
                boost = false;
                boostTimer = 0f;
            }
            ApplyForce(boostForce);
        }
        


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


        
        if (grounded)
        {
            prevHeading = new Vector3(transform.forward.x, 0, transform.forward.z);
            rb.drag = groundDrag;
            if (speed > 0)
            {
                ApplyForce(speed * slowMod * forwardAcceleration);
            }
            else
            {
                ApplyForce(speed * slowMod * backwardAcceleration);
            }
        }
        else
        {
            rb.drag = airDrag;
            rb.AddForce(-Vector3.up * gravityMagnitude * 100f);
        }
        
        

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

        if (!grounded && stunting)
        {
            transform.RotateAround(transform.position, transform.right, airtilt * flippingSpeed);
        }
        //apply extra gravity to car

        //Rev sfx when going forward
        if (speed > 0 && !scootRev.isPlaying)
        {
            scootRev.Play();
        }
        else 
        {
            scootRev.Stop();
        }

        //Do tilt and sfx when turning left/right
        if ( turn > 0f )
        {
            targetTiltAngle = -15f;
            if (!scootSkrt.isPlaying && grounded)
            {
                scootSkrt.Play();
            }
            if (!grounded)
            {
                scootSkrt.Stop();
            }
        }
        else if ( turn < 0f )
        {
            targetTiltAngle = 15f;
            if (!scootSkrt.isPlaying && grounded)
            {
                scootSkrt.Play();
            }
            if (!grounded) {
                scootSkrt.Stop();
            }
        }
        else {
            targetTiltAngle = 0f;
            if (scootSkrt.isPlaying) {
                scootSkrt.Stop();
            }
        }

        tiltObj.transform.localRotation = Quaternion.RotateTowards(tiltObj.transform.localRotation, Quaternion.Euler(tiltObj.transform.localRotation.x, tiltObj.transform.localRotation.y, targetTiltAngle), 2f);

        //Update Animation & FX Values
        UpdateAnimation();
        UpdateVFX();

        if (!grounded)
        {
            wasInAir = true;
            if(Mathf.Abs(turnAngle) >= 360)
            {
                flipped = true;
            }
            if (flippingSpeed > 0f)
            {
                turnAngle += Vector3.Angle(transform.up, prevUp);
            }
            else if(flippingSpeed < 0f)
            {
                turnAngle -= Vector3.Angle(transform.up, prevUp);
            }

        }
        else {
            landTimer -= Time.deltaTime;
            if (landTimer < 0) {
                wasInAir = false;
                landTimer = 1f;
            }
        }

        if (rb.transform.position.y <= resetDepth) {
            //reset player to ground
            rb.velocity = Vector3.zero;
            rb.transform.position = resetPos;
        }
        

        prevUp = transform.up;
    }
    //cancels vertical momentum, then applies force straight up
    public void ApplyJumpForce(float force){
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        ApplyForce(force, Vector3.up);
    }

    //applies a force to the vehicle with the given strength and direction. positive is forward, negative is backward.
    public void ApplyForce(float force, Vector3 direction)
    {
        rb.AddForce(direction * force * 1000f);
    }
    //applies a force to the vehicel with the given strength in teh direciton of transform forward
    public void ApplyForce(float force)
    {
        ApplyForce(force, transform.forward);
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

    //get the vespa's current speed as a scalar
    public float GetSpeed()
    {
        return rb.velocity.magnitude;
    }

    //gets the speed value provided from input, along with considerations for boost and 
    public float GetNormalSpeed()
    {
        float result = speed;
        if (slowed)
        {
            result /= 2;
        }
        if (boost)
        {
            result += 1;
        }
        return result;
    }


    //get the direction the vespa is facing
    public Vector3 GetHeading()
    {
        //if (grounded)
        //{
        //return new Vector3(transform.forward.x, 0, transform.forward.z);
        //}
        //return prevHeading;
        return Vector3.Cross(transform.right, Vector3.up);
    }

    //update the vespa's animation
    private void UpdateAnimation() { 

        scootAnimator.SetFloat("TurnDir", turn);
        scootAnimator.SetFloat("SpeedDir", speed);
        playerAnimator.SetFloat("TurnDir", turn);
    }

    //update the dust clouds kicked up
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
            
            //Debug.Log("flipped: " + flipped + ", turn angle: " + turnAngle + " / " + (360 - landAngles) + ", land angle: " + Vector3.Angle(prevUp, Vector3.up) + ", " + (flipped || Mathf.Abs(turnAngle) >= (360 - landAngles)) + ", " + (Vector3.Angle(prevUp, Vector3.up) <= landAngles));
            if ((flipped || Mathf.Abs(turnAngle) >= (360 - landAngles)) && (Vector3.Angle(prevUp, Vector3.up) <= landAngles))
            {
                LandBoost();
            }
            scootLandDust.Play();
            wasInAir = false;
            flipped = false;
            turnAngle = 0f;
            ResetJumps();
        }
    }

    //slow down the car temporarily
    public void SlowDown(float thisSlowDown = -1f)
    {   
        if(thisSlowDown < 0f){
            thisSlowDown = slowTime;
        }
        slowed = true;
        if(slowTimer < 0){
            slowTimer = 0;
        }
        slowTimer += thisSlowDown;
    }

    //apply the speed boost for landing a flip
    public void LandBoost()
    {
        Debug.Log("BOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOST");
        //rb.AddForce(transform.forward * speed * 3 * forwardAcceleration * 1000f);
        boost = true;
        boostTimer += boostTime;
        if (cam)
        {
            cam.Boost();
        }
    }

    //if this colliders with something, play the bump noise
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") && other.gameObject.layer != 6 && !scootBump.isPlaying)
        {
            //Debug.Log(other.gameObject);
            scootBump.Play();
        }
    }

}
