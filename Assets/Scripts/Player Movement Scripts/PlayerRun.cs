using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerRun : MonoBehaviour
{
    public TMP_Text debugText;

    private Rigidbody2D rb;

    [SerializeField] private float walkSpeed = 10;
    [SerializeField] private float runAccelAmount = 10;
    [SerializeField] private float runDeccelAmount = 10;

    private enum MoveType { Translate, Forces, Velocity, MovePosition, AdjustedForces };
    [SerializeField] private MoveType moveType;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            case MoveType.AdjustedForces:
                AdjustedForcesMovement();
                break;
        }
    }

    void TransformMovement()
    {
        int moveInput = (int)Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector2.right * walkSpeed * moveInput * Time.deltaTime);
        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.Translate(Vector2.left * walkSpeed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
        //}
    }

    void ForcesMovement()
    {
        int moveInput = (int)Input.GetAxisRaw("Horizontal"); 
        rb.AddForce(Vector2.right * walkSpeed * moveInput);
    }

    void VelocityMovement()
    {
        int moveInput = (int)Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(walkSpeed * moveInput, rb.velocity.y);
    }

    void MovePositionMovement()
    {
        int moveInput = (int)Input.GetAxisRaw("Horizontal");
        rb.MovePosition((Vector2)transform.position + new Vector2(moveInput, 0) * walkSpeed * Time.deltaTime);
    }

    void AdjustedForcesMovement()
    {
        int moveInput = (int)Input.GetAxisRaw("Horizontal");
        float currentSpeed = rb.velocity.x;
        float targetSpeed = moveInput * walkSpeed;
        float speedDif = targetSpeed - currentSpeed;
        float accelRate = (Mathf.Abs(targetSpeed) > Mathf.Abs(currentSpeed)) ? runAccelAmount : runDeccelAmount ;
        float movement = speedDif * accelRate;
        rb.AddForce(movement * Vector2.right);

        debugText.text = moveInput + "\n" + targetSpeed + "\n" + speedDif + "\n" + accelRate + "\n" + movement;
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
