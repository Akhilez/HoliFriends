using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

    public Manager manager;

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.collider.tag == "runner")
        {
            manager.IncrementScore();
            Debug.Log(manager.score.ToString());
        }
    }
}
