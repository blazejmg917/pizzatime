using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaFloat : MonoBehaviour
{
    [Tooltip("How quickly it spins")]
    public float spinSpeed;
    [Tooltip("how violently it floats up and down")]
    public float floatAmp;
    [Tooltip("how quickly it floats up and down")]
    public float floatFreq;
    [Tooltip("the center of it's movement")]
    private float startingHeight;
    private float timeElapsed;
    private float period;

    void Start()
    {
        startingHeight = transform.position.y;
        period = Mathf.PI * 2 / floatFreq;
        
    }

    void FixedUpdate()
    {
        if (GameManager.instance.IsPaused())
        {
            return;
        }
        transform.position = new Vector3(transform.position.x, startingHeight + floatAmp * Mathf.Sin(timeElapsed * floatFreq), transform.position.z);
        timeElapsed += Time.fixedDeltaTime;
        if(timeElapsed >= period)
        {
            timeElapsed = 0f;
        }
        transform.RotateAround(transform.position, Vector3.up, spinSpeed);
    }
}
