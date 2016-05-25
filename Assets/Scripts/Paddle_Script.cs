using UnityEngine;
using System.Collections;

public class Paddle_Script : MonoBehaviour {

    public float paddleSpeed = 0.5f;
    float boundLeft = -7.5f;
    float boundRight = 7.5f;
    public GameObject ball;
    GameObject ballLocal;
    bool ballOnPaddle = false;

    // Use this for initialization
    void Start () {
        SpawnBall();            
	}
	
	// Update is called once per frame
	void Update () {
        //Movement between wall boundaries
        if(transform.position.x <= boundRight && transform.position.x >= boundLeft)
            transform.position += new Vector3(Input.GetAxis("Horizontal")*paddleSpeed,0,0);
        if(transform.position.x > boundRight)
            transform.position = new Vector3(boundRight, transform.position.y, transform.position.z);
        if (transform.position.x < boundLeft)
            transform.position = new Vector3(boundLeft, transform.position.y, transform.position.z);

        // Ball should follow paddle when on paddle
        // until it launched
        if (ballLocal && ballOnPaddle)
        {
            ballLocal.transform.position = new Vector3(transform.position.x, ballLocal.transform.position.y, ballLocal.transform.position.z);
            if (Input.GetButton("Jump"))
            {
                Rigidbody rb = ballLocal.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.AddForce(500f * Input.GetAxis("Horizontal"), 500f, 0);
                ballOnPaddle = false;
            }            
        }           
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            //Calculate where exactly ball collided on paddle
            if (contact.thisCollider == gameObject.GetComponent<Collider>())
            {
                // if key less than 0 - ball collided on left side of paddle
                float key = contact.point.x - transform.position.x;
                Debug.Log("Paddle position key:" + key);
                //Gets ball rigid body and add force to it
                contact.otherCollider.GetComponent<Rigidbody>().AddForce(200f * key, 0, 0);
            }                
        }
    }
    /// <summary>
    /// Spawn ball on paddle and set ballLocal
    /// </summary>
    public void SpawnBall ()
    {
        if (ball.activeInHierarchy == false)
        {
            ballLocal = (GameObject)Instantiate(ball, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            ballOnPaddle = true;
        }
    }
}
