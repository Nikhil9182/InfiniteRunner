using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    public GameObject[] obstacles;
    public float timeBtwSpawn;
    public float starttimeBtwSpawn;
    public float timeBtwDestroy;
    public float starttimeBtwDestroy;
    private List<GameObject> activeobj = new List<GameObject>();
    void Update()
    {
        transform.Translate(0f, 0f, Movement.speed * Time.deltaTime);
        if (timeBtwSpawn <= 0)
        {
            int rand = Random.Range(0, obstacles.Length);
            activeobj.Add(Instantiate(obstacles[rand], transform.position, Quaternion.identity));
            timeBtwSpawn = starttimeBtwSpawn;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
        if (timeBtwDestroy <= 0) 
        {
            DeleteObj();
            timeBtwDestroy = starttimeBtwDestroy;
        }
        else
        {
            timeBtwDestroy -= Time.deltaTime;
        }
    }
    
    public void DeleteObj()
    {
                Destroy(activeobj[0]);
                activeobj.RemoveAt(0);
    }
}
