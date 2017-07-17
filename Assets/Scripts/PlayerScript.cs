using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public Transform weaponPrefab;
    public Transform weapon;
    public Manager manager;
    private Vector3 weaponPosition = new Vector3(0, 8, 25);
    public GameObject gameOverUi;

    public float weaponSpeed = 1f;
    private bool thrown = false;
    private float weaponForce = 2000f;


    private Vector3 screenPoint;
	private Vector3 offset;
    private float t0 = 0f;
    private float longPress = 0.4f;

    private int prevScore = 0;
    private Miss miss = new Miss();
		
	void OnMouseDown(){
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, screenPoint.y, screenPoint.z));
	}
		
	void OnMouseDrag(){
		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, screenPoint.y, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
		transform.position = cursorPosition;
        if (!thrown)
        {
            weapon.position = weaponPosition + new Vector3(cursorPosition.x, 0, 0);
        }
        

    }


    // Use this for initialization
    void Start () {
        weapon = Instantiate(weaponPrefab, weaponPosition, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update() { 
        TapToThrow();
	}

    void TapToThrow()
    {
        if (Input.GetMouseButtonDown(0))
        {
            t0 = Time.time;
        }
        if (Input.GetMouseButtonUp(0) && (Time.time - t0) < longPress)
        {
            if (!thrown)
            {
                weapon.gameObject.GetComponent<Rigidbody>().AddForce(
                    new Vector3(
                        0, 
                        (weaponForce/10 ) * Time.deltaTime, 
                        (weaponForce + 100 * weaponSpeed) * Time.deltaTime
                    )
                );
                thrown = true;
                Invoke("ResetWeapon", 2);
            }
        }
    }

    void ResetWeapon()
    {
        if (thrown)
        {
            if (weapon != null) Destroy(weapon.gameObject);
            weapon = Instantiate(weaponPrefab, weaponPosition + new Vector3(transform.position.x, 0, 0), Quaternion.identity);
            thrown = false;
            if(prevScore == manager.score)
            {
                Debug.Log("Miss");
                miss.count++;
                miss.streak++;
                if(miss.count >= 3)
                {
                    gameOverUi.SetActive(true);
                }
            }
            else
            {
                prevScore = manager.score;
                miss.streak = 0;
            }
        }
    }


}

public class Miss
{
    public int count = 0;
    public int streak = 0;
}