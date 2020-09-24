using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ScoreTextScript.coinAmount += 10;
        }
    }
}
