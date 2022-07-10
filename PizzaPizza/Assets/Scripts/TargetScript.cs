using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            other.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (this.gameObject.activeSelf == false)
        {
            Destroy(this);
        }
        {

        }
    }
}
