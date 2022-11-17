using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FastCar : MonoBehaviour
{

    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = rb.velocity;

        velocity.x = 4;

        rb.velocity = velocity;

        if (transform.position.x >= pointA.transform.position.x)
        {
            transform.position = new Vector2(pointB.transform.position.x, transform.position.y);
        }
    }


}
