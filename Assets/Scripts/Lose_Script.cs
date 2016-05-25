using UnityEngine;
using System.Collections;

public class Lose_Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // When ball not catched by player
    // destroy ball
    // spawn new on paddle
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.gameObject);
        Destroy(collider.gameObject);
        GameObject.Find("Paddle").GetComponent<Paddle_Script>().SpawnBall();
        GameObject.Find("Game").GetComponent<Game_Script>().LoseOneLife();
    }
}
