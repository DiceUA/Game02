using UnityEngine;
using System.Collections;

public class Brick_Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Destroy brick when collided with ball    
    void OnCollisionEnter ()
    {
        GameObject.Find("Game").GetComponent<Game_Script>().BrickDestroyed();        
        Destroy(gameObject);
    }
}
