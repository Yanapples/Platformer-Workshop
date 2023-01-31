using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCircles : MonoBehaviour
{
    public GameObject circle1;
    public GameObject circle2;
    public GameObject circle3;

    private Vector3 startPos1;
    private Vector3 startPos2;
    private Vector3 startPos3;

    private float timePassed;
    public float interval = 5;
    // Start is called before the first frame update
    void Start()
    {
        startPos1 = circle1.transform.position;
        startPos2 = circle2.transform.position;
        startPos3 = circle3.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > interval)
        {
            Debug.Log("time passed!");
            circle1.transform.position = startPos1;
            circle2.transform.position = startPos2;
            circle3.transform.position = startPos3;
            circle2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            circle3.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            timePassed -= interval;
        }
    }
}
