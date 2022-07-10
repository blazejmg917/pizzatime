using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionGoal : MonoBehaviour
{
    [Tooltip("name associated with the pizza order")]
    public string pizzaName;
    [Tooltip("Whether this is the active goal or not")]
    public bool isCurrentGoal = false;
    [Tooltip("goal indicator name")]
    public string indName = "ActiveGoalIndicator";
    [Tooltip("player tag")]
    public string playerTag = "Player";

    public void SetActiveGoal()
    {
        isCurrentGoal = true;
        GameObject goalIndicator = GameObject.Find(indName);
        goalIndicator.transform.position = new Vector3(transform.position.x, goalIndicator.transform.position.y, transform.position.z);
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == playerTag && isCurrentGoal)
        {
            GameObject.Find("GoalManager").GetComponent<GoalManager>().FinishGoal();
        }
    }

    
}
