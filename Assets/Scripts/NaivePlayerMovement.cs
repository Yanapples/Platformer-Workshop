using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaivePlayerMovement : MonoBehaviour
{
    [SerializeField] private int walkSpeed = 10;
    [SerializeField] private int jumpSpeed = 5;

    Transform player;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputManager();
    }

    void InputManager()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * walkSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = (Vector2.up * jumpSpeed);
        }
    }
}
