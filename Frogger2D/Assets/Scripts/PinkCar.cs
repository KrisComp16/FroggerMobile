using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PinkCar : MonoBehaviour
{

    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = rb.velocity;

        velocity.x = -0.7f;

        rb.velocity = velocity;

        if (transform.position.x <= pointA.transform.position.x)
        {
            transform.position = new Vector2(pointB.transform.position.x, transform.position.y);
        }
    }


}
