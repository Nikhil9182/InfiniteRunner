using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rb3d;

    public static float speed = 5f;
    public float jump_distance = 4f;
    private int flag = 0;
    public float sidedist = 0.842f;
    private Vector3 targetpos;

    private float swipeStartTime;
    private float swipeEndTime;

    public float maxSwipetime;
    public float minSwipeDistance;
    private float swipeTime;

    private Vector2 startSwipePosition;
    private Vector2 endSwipePosition;
    private float swipeLength;
    public float speedoftrans = 50;
    public Transform groundcheck; // groundcheck object variable
    public float groundDistance = 0.4f; //distance from ground
    public LayerMask groundMask; // ground mask for assuming which is ground to jump or fall
    bool isgrounded;
    // Start is called before the first frame update
    void Start()
    {
        rb3d = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, 0f, speed * Time.deltaTime);
        anim.SetBool("is_sliding", false);
        anim.SetBool("is_jumping", false);
        isgrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask); // taking parameters of position , distance from ground , and the mask or object from which to jump

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                swipeStartTime = Time.time; ;
                startSwipePosition = touch.position;
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                swipeEndTime = Time.time;
                endSwipePosition = touch.position;
                swipeTime = swipeEndTime - swipeStartTime;
                swipeLength = (endSwipePosition - startSwipePosition).magnitude;
                if (swipeTime < maxSwipetime && swipeLength > minSwipeDistance)
                {
                    SwipeControl();
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) && flag >= -1)
        {
            targetpos = new Vector3(transform.position.x - sidedist, transform.position.y, transform.position.z);
            transform.position = targetpos;
            flag -= 2;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && flag <= 1)
        {
            targetpos = new Vector3(transform.position.x + sidedist, transform.position.y, transform.position.z);
            transform.position = targetpos;
            flag += 2;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("is_sliding", true);
        }
        else
        {
            anim.SetBool("is_sliding", false);
        }
        if(Input.GetKey(KeyCode.Space) && isgrounded)
        {
            anim.SetBool("is_jumping", true);
            //rb3d.velocity = Vector2.up * jump_distance * Time.deltaTime;
            rb3d.AddForce(0f, jump_distance * 5 * Time.deltaTime, 0, ForceMode.Impulse);
        }
        else
        {
            anim.SetBool("is_jumping", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coins"))
        {
            Destroy(other.gameObject);
        }
    }

    void SwipeControl()
    {
        Vector2 Distance = endSwipePosition - startSwipePosition;
        float xDistance = Mathf.Abs(Distance.x);
        float yDistance = Mathf.Abs(Distance.y);
        if (xDistance > yDistance)
        {
            if (Distance.x > 0 && flag <= 1)
            {
                targetpos = new Vector3(transform.position.x + sidedist, transform.position.y, transform.position.z);
                transform.position = targetpos;
                flag += 2;
            }
            if (Distance.x < 0 && flag >= -1)
            {
                targetpos = new Vector3(transform.position.x - sidedist, transform.position.y, transform.position.z);
                transform.position = targetpos;
                flag -= 2;
            }
        }
        else if (yDistance > xDistance)
        {
            if (Distance.y > 0 && isgrounded)
            {
                anim.SetBool("is_jumping", true);
                rb3d.AddForce(0f, jump_distance * 5 * Time.deltaTime, 0, ForceMode.Impulse);
            }
            if (Distance.y < 0)
            {
                anim.SetBool("is_sliding", true);
            }
        }
    }
}
