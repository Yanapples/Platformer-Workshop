using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float walkSpeed = 10;
    public float sprintSpeed = 20;
    public float jumpHeight = 5;
    private float currentSpeed = 10;

    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.IsPlay)
        {
            InputHandler();
        }
    }

    void InputHandler()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = walkSpeed;
        }
    }

    // How to craft better jumping

    //  1. Increase fall speed
    //  2. Jump height based on how long we hold down jump button (velocity cut/gravity increase)
    //  3. Max speed (Clamping)
    //  4. Air Time
    //  5. Jump hang (lower gravity at peak of jump) 
    //  6. Bonus peak speed (increased acceleration at peak of jump)
    //  7. visuals (squash and stretch, impact particles)
    //  8. Jump force 
    //  9. Coyote time (Grace duration after leaving platform)
    // 10. Jump buffer (Grace period when you press jump before getting grounded)
    // 11. Wall jumps -> reduce run force right after jumping (lerp)
    // 12. Slide 
    // 13. Double jumps
    // 14. Ability synergy -> gives better movement without raising complexity
    // 15. Dash
    // 16. Reduce acceleration when airborne
    private void Jumping()
    {
        // When you press space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Jump up
        }
        // When you release space
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // Add downwards gravity

        }
        // If player is falling
        if (rb.velocity.y < 0)
        {
            // Cap downwards velocity

        }
    }
}
