using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyrmPatrolPoint : MonoBehaviour
{
    public float cooldownTime = 5f;
    private float cooldownTimer = 0f;
    private bool cooledDown = true;

    private void Update()
    {
        if (GameManager.instance.IsPaused())
        {
            return;
        }
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer > cooldownTime) {
            cooledDown = true;
            cooldownTimer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GarlicWyrmManager wyrmManager = other.gameObject.GetComponentInParent<GarlicWyrmManager>();
        if (wyrmManager != null && cooledDown) {
            wyrmManager.patrolIndex = (wyrmManager.patrolIndex + 1) % wyrmManager.patrolPoints.Count;
            cooledDown = false;
        } 
    }
}
