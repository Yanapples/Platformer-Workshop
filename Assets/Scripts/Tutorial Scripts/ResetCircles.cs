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
    private float interval = 3;

    // Start is called before the first frame update
    void Start()
    {
        if (circle1) startPos1 = circle1.transform.position;
        if (circle2) startPos2 = circle2.transform.position;
        if (circle3) startPos3 = circle3.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > interval)
        {
            if (circle1) circle1.transform.position = startPos1;
            if (circle2) circle2.transform.position = startPos2;
            if (circle3) circle3.transform.position = startPos3;
            if (circle2) circle2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if (circle3) circle3.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            timePassed -= interval;
        }
    }
}
