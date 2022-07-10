using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GarlicWyrmManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject wyrmHeadPrefab;
    [SerializeField] private GameObject wyrmBodyPrefab;

    [Header("Target")]
    [SerializeField]
    private GameObject target;

    [Header("Spawner Variables")]
    [SerializeField]
    private float wyrmSimulationArea = 50f;
    [SerializeField, Range(0f, 10f)]
    private float targetAggressionWeight;
    [SerializeField] private float localAreaRadius = 10f;

    // Speed
    [SerializeField] private float speed = 10f;
    [SerializeField] private float steeringSpeed = 100f;

    [SerializeField] private GarlicWyrmController wyrm;

    [Header("Wyrm Body Variables")]
    [SerializeField] private List<GameObject> snakeBody = new List<GameObject>();
    [SerializeField] private int segmentsToSpawn = 8;
    [SerializeField] private float distanceBetween = .2f;
    [SerializeField] private float countUp = 0f;

    [Header("Wyrm State Variables")]
    [SerializeField] private float attackDist;
    [SerializeField] private WyrmState state;
    [SerializeField] private float patrolTime = 150f;
    [SerializeField] private float chaseTime = 30f;
    public List<GameObject> patrolPoints;
    public int patrolIndex = 0;
    private float patrolTimer = 0f;
    private float chaseTimer = 0f;
    private bool canShoot;
    private float shootTime = 2f;
    private float shootTimer = 0f;
    private CheezyBlits cb; 

    public enum WyrmState { 
        Patrolling,
        Chasing,
        Attacking
    };

    //private void Awake()
    //{
    //    target = GameObject.FindGameObjectWithTag("Player");
    //}

    private void Start()
    {

        if (wyrmHeadPrefab != null && wyrmBodyPrefab != null)
        {
            SpawnWyrm();
        }

        state = WyrmState.Patrolling;
    }

    private void Update()
    {
        /*if (GameManager.instance.IsPaused())
        {
            return;
        }*/
        if (segmentsToSpawn > 0) {
            CreateBodyParts();
        }


        if (state == WyrmState.Patrolling) { 
            if (target == null || target != patrolPoints[patrolIndex])
                target = patrolPoints[patrolIndex];

            wyrm.SetFlockingVariables(speed, steeringSpeed, localAreaRadius, target);

            patrolTimer += Time.deltaTime;

            if (patrolTimer > patrolTime) {
                state = WyrmState.Chasing;
                patrolTimer = 0f;
            }
                
        }
        if (state == WyrmState.Chasing) {

            shootTimer += Time.deltaTime;
            if (shootTimer > shootTime) {
                canShoot = true;
                shootTimer = 0f;
            }

                if (target == null || !target.gameObject.CompareTag("Player"))
                target = GameObject.FindGameObjectWithTag("Player");

            wyrm.SetFlockingVariables(speed, steeringSpeed, localAreaRadius, target);

            chaseTimer += Time.deltaTime;

            if (chaseTimer > chaseTime) {
                state = WyrmState.Patrolling;
                chaseTimer = 0f;
                shootTimer = 0f;
            }

            if ((Vector3.Magnitude(target.transform.position - wyrm.transform.position) < attackDist) && canShoot ) {
                state = WyrmState.Attacking;
                shootTimer = 0f;
            }
        }

        

        if (state == WyrmState.Attacking) {
            cb = wyrm.gameObject.GetComponent<CheezyBlits>();
            cb.player = target;
            cb.GarlicKnotWorm = wyrm.gameObject;
            cb.firingLocation = wyrm.firingLocation;

            cb.Attack();
            canShoot = false;
            state = WyrmState.Chasing;
        }

        SnakeMovement();

    }

    private void SnakeMovement() {
        if (wyrm != null && wyrm.GetComponent<GarlicWyrmController>() != null && wyrm.gameObject.activeInHierarchy)
        {
            wyrm.GetComponent<GarlicWyrmController>().SimulateMovement(wyrm, Time.deltaTime, targetAggressionWeight); //separationWeight, alignmentWeight, cohesionWeight,

            //var wyrmPos = wyrm.transform.position;

            //if (wyrmPos.x > wyrmSimulationArea)
            //    wyrmPos.x -= wyrmSimulationArea * 2;
            //else if (wyrmPos.x < -wyrmSimulationArea)
            //    wyrmPos.x += wyrmSimulationArea * 2;

            //if (wyrmPos.y > wyrmSimulationArea)
            //    wyrmPos.y -= wyrmSimulationArea * 2;
            //else if (wyrmPos.y < -wyrmSimulationArea)
            //    wyrmPos.y += wyrmSimulationArea * 2;

            //if (wyrmPos.z > wyrmSimulationArea)
            //    wyrmPos.z -= wyrmSimulationArea * 2;
            //else if (wyrmPos.z < -wyrmSimulationArea)
            //    wyrmPos.z += wyrmSimulationArea * 2;

            //wyrm.transform.position = wyrmPos;
        }

        if (snakeBody.Count > 1) {
            for (int i = 1; i < snakeBody.Count; i++) {
                WyrmMarkerManager markM = snakeBody[i - 1].GetComponent<WyrmMarkerManager>();
                snakeBody[i].transform.position = markM.markerList[0].position;
                snakeBody[i].transform.rotation = markM.markerList[0].rotation;
                markM.markerList.RemoveAt(0);
            }
        }
    }

    private void SpawnWyrm() // int swarmIndex, List<BoidController> list
    {
        var wyrmInstance = Instantiate(wyrmHeadPrefab, transform);
        wyrmInstance.GetComponent<GarlicWyrmController>().SetFlockingVariables(speed, steeringSpeed, localAreaRadius, target);
        wyrmInstance.transform.localPosition += new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        wyrmInstance.transform.localRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

        var wyrmController = wyrmInstance.GetComponent<GarlicWyrmController>();
        this.wyrm = wyrmController;

        if (!wyrmInstance.GetComponent<WyrmMarkerManager>())
        { 
            wyrmInstance.AddComponent<WyrmMarkerManager>();
        }
        if (!wyrmInstance.GetComponent<Rigidbody>())
        {
            wyrmInstance.AddComponent<Rigidbody>();
            wyrmInstance.GetComponent<Rigidbody>().useGravity = false;
        }
        snakeBody.Add(wyrmInstance);
        countUp += Time.deltaTime;
    }

    private void CreateBodyParts()
    {
        WyrmMarkerManager markM = snakeBody[snakeBody.Count - 1].GetComponent<WyrmMarkerManager>();
        if (countUp == 0)
        {
            markM.ClearMarkerList();
            Debug.Log("Marker List Cleared");
        }
        countUp += Time.deltaTime;
        if (countUp >= distanceBetween) {
            //Your snake body parts need marker managers and rigidbody components, I add them here if you don't manually add them
            GameObject temp = Instantiate(wyrmBodyPrefab, markM.markerList[0].position, markM.markerList[0].rotation, transform);
            if (!temp.GetComponent<WyrmMarkerManager>())
            {
                temp.AddComponent<WyrmMarkerManager>();
            }
            if (!temp.GetComponent<Rigidbody>())
            {
                temp.AddComponent<Rigidbody>();
                temp.GetComponent<Rigidbody>().useGravity = false;
            }
            snakeBody.Add(temp);
            temp.GetComponent<WyrmMarkerManager>().ClearMarkerList();
            countUp = 0f;
            segmentsToSpawn--;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3
            (wyrmSimulationArea * 2, wyrmSimulationArea * 2, wyrmSimulationArea * 2));
    }

}
