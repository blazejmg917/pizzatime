using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [Header("Mission items")]
    [Tooltip("All possible goals")]
    public List<MissionGoal> goalList;
    [Tooltip("The most recently assigned goals")]
    public List<MissionGoal> prevGoals;
    [Tooltip("all currently valid goals")]
    public List<MissionGoal> validGoals;
    [Tooltip("How many previous goals to hold onto to prevent repeat missions")]
    public int prevGoalMax = 3;
    [Tooltip("the current goal")]
    public MissionGoal currentGoal;
    [Tooltip("the name of the current customer")]
    public string customerName;
    [Tooltip("the name of the customer's pizza order")]
    public string pizzaName;
    [Tooltip("the generator for pizza types and names")]
    public PizzaGen pizzaGen;

    [Header("Timer/Scoring settings")]
    [Tooltip("the base score for the current goal")]
    public int currentGoalScore;
    [Tooltip("mission max time")]
    public float missionMaxTime;
    [Tooltip("time elapsed")]
    public float timeElapsed;
    [Tooltip("average mission score")]
    public int avgScore;
    [Tooltip("mission score variance")]
    public float scoreVar;
    [Tooltip("max tip percentage for time score bonus")]
    public float maxTip;

    [Header("Other objects/scripts")]
    [Tooltip("the timer class")]
    public Timer timer;
    [Tooltip("the order class")]
    public OrderDisplay od;
    [Tooltip("the score class")]
    public ScoreDisplay sd;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Transform child in transform)
        {
            Debug.Log("child");
            goalList.Add(child.gameObject.GetComponent<MissionGoal>());
            validGoals.Add(child.gameObject.GetComponent<MissionGoal>());
        }
    }

    void Start()
    {
        NewGoal();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.IsPaused())
        {
            return;
        }
        timeElapsed += Time.fixedDeltaTime;
        timer.UpdateTimer(timeElapsed, missionMaxTime);
    }

    public void MetGoal()
    {
        Debug.Log("Goal complete");
        NewGoal();
    }

    public void FinishGoal()
    {
        GetPoints(timeElapsed);
        if (prevGoals.Count >= prevGoalMax)
        {
            validGoals.Add(prevGoals[0]);
            prevGoals.RemoveAt(0);
        }
        prevGoals.Add(currentGoal);
        validGoals.Remove(currentGoal);
        sd.UpdateDisplay(currentGoalScore);
        currentGoal.SetActiveGoal(false);
        NewGoal();
    }

    public void NewGoal()
    {
        customerName = pizzaGen.GetOrderName();
        pizzaName = pizzaGen.GetPizzaPopularVote();
        currentGoal = validGoals[Random.Range(0, validGoals.Count)];
        currentGoal.SetActiveGoal(true);
        ResetTimer();
        GenerateScore();
        od.UpdateDisplay(customerName, pizzaName, currentGoalScore);
    }

    public void GetPoints(float timeTaken)
    {
        int points = currentGoalScore;
        float tip = Mathf.Max((missionMaxTime - timeTaken) / missionMaxTime * maxTip, 0f);
        points = (int)((float)points * (1f + tip));
    }

    public void GenerateScore()
    {
        currentGoalScore = (int)((float)avgScore * (1 + Random.Range(-scoreVar / 2, scoreVar / 2)));
    }

    public void ResetTimer()
    {
        timeElapsed = 0f;
    }
}
