using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    private Rigidbody2D rb;

    public float walkSpeed = 10;
    public float sprintSpeed = 20;
    public float jumpHeight = 5;

    private GameManager gm;

    private enum MoveType { Translate, Forces, Velocity, MovePosition };
    [SerializeField]
    private MoveType moveType;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (moveType)
        {
            case MoveType.Translate: 
                TransformMovement();
                break;
            case MoveType.Forces:
                ForcesMovement();
                break;
            case MoveType.Velocity:
                VelocityMovement();
                break;
            case MoveType.MovePosition:
                MovePositionMovement();
                break;
            //case MoveType.AdjustedVelocity:
            //    AdjustedVelocityMovement();
            //    break;
        }
    }

    void TransformMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * walkSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
        }
    }

    void ForcesMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * walkSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * walkSpeed);
        }
    }

    void VelocityMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = Vector2.left * walkSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = Vector2.right * walkSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        } 
            
    }

    void MovePositionMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.MovePosition((Vector2)transform.position + Vector2.left * walkSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.MovePosition((Vector2)transform.position + Vector2.right * walkSpeed * Time.deltaTime);
        }
    }

    void AdjustedVelocityMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            float targetSpeed = Mathf.Lerp(rb.velocity.x, walkSpeed, 1);
            rb.velocity = new Vector2(-targetSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            float targetSpeed = Mathf.Lerp(rb.velocity.x, walkSpeed, 1);
            rb.velocity = new Vector2(targetSpeed, rb.velocity.y);
        }
        else
        {
            float targetSpeed = Mathf.Lerp(rb.velocity.x, 0, 1);
            rb.velocity = new Vector2(targetSpeed, rb.velocity.y);
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

}
