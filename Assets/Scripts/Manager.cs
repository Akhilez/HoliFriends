using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public Transform surroundingsPrefab;
    private Transform cur, prev;
    public RunnerScript runner;
    public int score = 0;
    private float minimumCuzPosition;
    public Text scoreText;

	// Use this for initialization
	void Start () {
        cur = Instantiate(surroundingsPrefab, new Vector3(0,0,0), Quaternion.identity);
        prev = Instantiate(surroundingsPrefab, new Vector3(0, 0, 500), Quaternion.identity);
        minimumCuzPosition = cur.GetChild(1).GetChild(1).localScale.z;
    }

	// Update is called once per frame
	void Update () {
        CreateOrDestroy();
        MoveBothBack();
    }

    void CreateOrDestroy()
    {
        if (cur.position.z <= -minimumCuzPosition)
        {
            Destroy(cur.gameObject);
            cur = prev;
            prev = Instantiate(surroundingsPrefab, new Vector3(0, 0, prev.position.z + minimumCuzPosition), Quaternion.identity);
        }
    }

    void MoveBothBack()
    {
        float initialSpeed = 5f;
        Vector3 vector = Vector3.back * (initialSpeed * runner.speed) * Time.deltaTime;
        cur.position = cur.position + vector;
        prev.position = prev.position + vector;
    }

    public void IncrementScore()
    {
        score++;
        runner.speed++;
        scoreText.text = score.ToString();
    }

}
