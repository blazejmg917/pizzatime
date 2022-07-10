using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheezyBlitzsOfCheezyness : MonoBehaviour
{

    public GameObject player;
    public GameObject GarlicKnotWorm;
    public GameObject TargetPreFab;
    public GameObject PizzaProjectile;
    public GameObject firingLocation;




    public int numProjectiles = 8;
    public float shotDelay = 2;
    public float radiusOfAttack = 5;
    public float leadDistance = 5;

    private GameObject[] attackPos;

    private void Update()
    {
        
        GarlicKnotWorm.transform.LookAt(player.transform);
    }

    public void attack()
    {
        RaycastHit hit;
        GameObject target = null;
        Debug.DrawRay(firingLocation.transform.position, firingLocation.transform.forward * 1000, Color.red, 10);
        if (Physics.Raycast(firingLocation.transform.position, firingLocation.transform.forward, out hit, Mathf.Infinity))
        {
            target = Instantiate(TargetPreFab, player.transform.position, player.transform.rotation);
            GameObject pizza = Instantiate(PizzaProjectile, firingLocation.transform.position, firingLocation.transform.rotation);
            pizza.GetComponent<PizzaSlice>().target = target.transform.position;
        }

        
        //start coroutine to fire projectiles
        //StartCoroutine(fireSlices());
    }
    
    IEnumerator fireSlices()
    {
        yield return new WaitForSeconds(shotDelay);
        for (int i = 0; i < numProjectiles; i++)
        {
            //Instantiate Slice
            GameObject temp = Instantiate(PizzaProjectile, this.transform);
            temp.GetComponent<PizzaSlice>().target = attackPos[i].transform.position;
            temp.GetComponent<PizzaSlice>().player = player;
        }
    }
}
