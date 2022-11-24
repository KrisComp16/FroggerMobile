using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private Rigidbody rb;
    public int speed;
    public GameObject targetPos;
    Animator anim;
    public PointsManager pm;
    string currentScene = SceneManager.GetActiveScene().name;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
        Movement();
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos.transform.position, step);
        //CheckForMovement();



    }

    

    void Movement()
    {
        RaycastHit RayUp, RayDown, RayLeft, RayRight;
        Vector2 velocity = rb.velocity;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            float touchDistanceX = endTouchPosition.x - startTouchPosition.x;
            float touchDistanceY = endTouchPosition.y - startTouchPosition.y;

            print(touchDistanceX);
            print(touchDistanceY);
            if (touchDistanceX < -200f)
            {
                print("Left");

                if (Physics.Raycast(new Vector3 (transform.position.x - 0.2f, transform.position.y, 0), transform.TransformDirection(Vector3.left), out RayLeft, Mathf.Infinity))
                {
                    Debug.DrawRay(new Vector3(transform.position.x - 0.2f, transform.position.y, 0), transform.TransformDirection(Vector3.left) * RayLeft.distance, Color.green);

                    if (RayLeft.collider != null)
                    {
                        if (RayLeft.collider.tag == "gridPoint")
                        {
                            targetPos = RayLeft.transform.gameObject;
                            anim.Play("leftjump", -1, 0f);
                        }
                    }
                    
                }

            }

            else if (touchDistanceX > 200f)
            {
                print("Right");

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out RayRight, Mathf.Infinity))
                {
                    if (RayRight.collider != null)
                    {
                        if (RayRight.collider.tag == "gridPoint")
                        {
                            targetPos = RayRight.transform.gameObject;
                            anim.Play("rightjump", -1, 0f);
                        }
                    }
                }

            }

            else if (touchDistanceY < -0.1f)
            {
                print("Down");

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RayDown, Mathf.Infinity))
                {
                    if (RayDown.collider != null)
                    {
                        if (RayDown.collider.tag == "gridPoint")
                        {
                            targetPos = RayDown.transform.gameObject;
                            anim.Play("downjump", -1, 0f);
                            pm.playerscore -= 10;
                        }
                    }
                }
            }

            else if (touchDistanceY > 0.1f)
            {
                print("Up");

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out RayUp, Mathf.Infinity))
                {
                    if (RayUp.collider != null)
                    {
                        if (RayUp.collider.tag == "gridPoint")
                        {
                            targetPos = RayUp.transform.gameObject;
                            anim.Play("jump", -1, 0f);
                            pm.playerscore += 10;
                        }
                    }
                }
            }
        }

        rb.velocity = velocity;
    }
    /*
    IEnumerator Wait()
    {
        Vector2 velocity = rb.velocity;
        rb.velocity = velocity;
        yield return new WaitForSeconds(1);
        velocity.y = 0;
        velocity.x = 0;
    }
    */


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "death")
        {
            print("dead");
            anim.Play("death");
        }
    }

    /*
    public void SceneReload()
    {
        SceneManager.LoadScene(currentScene);
    }
    */
}



