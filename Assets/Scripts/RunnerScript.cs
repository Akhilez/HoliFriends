using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerScript : MonoBehaviour {

    public float speed = 100f;
    private float speedThreshold = 40f;
    private bool direction = true;
    private float boundary = 16f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        MoveLeftOrRight();
	}

    void MoveLeftOrRight()
    {

        if (transform.position.x < -boundary || transform.position.x > boundary)
        {
            direction = !direction;
            Flip();
        }

        Vector3 directionVector = direction ? Vector3.right : Vector3.left;

        transform.Translate(directionVector * speed * Time.deltaTime);


    }

    private void Flip()
    { 
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

}
