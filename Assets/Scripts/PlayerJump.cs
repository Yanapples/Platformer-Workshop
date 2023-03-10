using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEditor;

public class PlayerJump : MonoBehaviour
{

    [HideInInspector] [SerializeField] private float jumpForce = 2;
    [HideInInspector] [SerializeField] private bool enableGroundChecks;
    [HideInInspector] [SerializeField] private Transform groundCheck;
    [HideInInspector] [SerializeField] private LayerMask groundLayer;
    [HideInInspector] [SerializeField] private bool enableJumpCut;
    [HideInInspector] [SerializeField] private bool increaseGWhenFalling;
    [HideInInspector] [SerializeField] private float fallingGravity = 5;
    [HideInInspector] [SerializeField] private float maxFallSpeed = 10;
    [HideInInspector] [SerializeField] private bool enableCoyoteTime;
    [HideInInspector] [SerializeField] private float coyoteTime = 0.1f;
    [HideInInspector] [SerializeField] private bool enableJumpTimeCooldown;
    [HideInInspector] [SerializeField] private float jumpInputBuffer = 0.1f;
    [HideInInspector] [SerializeField] private float defaultGravity = 1;

    [SerializeField] private AudioSource jump;

    private Rigidbody2D rb;
    private float timeSinceLastGrounded = 0;
    private float timeJumpCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timeJumpCooldown -= Time.deltaTime;
        timeSinceLastGrounded += Time.deltaTime;
        // If the player presses the spacebar we jump
        CheckGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && CanJump()) Jump();
        if (Input.GetKeyUp(KeyCode.Space) && enableJumpCut) ReleasedJump();
        RegulatePlayerVelocity();
    }

    private void RegulatePlayerVelocity()
    {
        // If falling, increase gravity
        if (increaseGWhenFalling)
            if (rb.velocity.y < 0) rb.gravityScale = fallingGravity;
        // Clamp the falling velocity so player doesn't supersonic into ground
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
    }

    private void ReleasedJump()
    {
        if (enableJumpCut && rb.velocity.y > 0) rb.velocity = new Vector2(rb.velocity.x, 0);
    }

    private bool CanJump()
    {
        // Check jump cooldown over
        if (timeJumpCooldown > 0) return false;

        // If coyote time enabled, grace window to jump
        float graceWindow = enableCoyoteTime ? coyoteTime : 0;

        // Check within the grace window
        if (timeSinceLastGrounded <= graceWindow) return true;

        // If checks fail, cannot jump
        return false;
    }

    private void Jump()
    {
        // Add jumping input buffer
        if (enableJumpTimeCooldown) timeJumpCooldown = jumpInputBuffer;
        float force = jumpForce;

        // Increase jump force when falling to offset downwards acceleration
        if (rb.velocity.y < 0)
            force -= rb.velocity.y;

        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        if (jump) jump.Play();
    }

    private void CheckGrounded()
    {
        Vector2 groundCheckSize = groundCheck.transform.localScale;
        if (Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer)) //checks if set box overlaps with ground
        {
            rb.gravityScale = defaultGravity;
            timeSinceLastGrounded = 0;
        }

        if (!enableGroundChecks) timeSinceLastGrounded = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (groundCheck) Gizmos.DrawWireCube(groundCheck.position, groundCheck.localScale);
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(PlayerJump)), CanEditMultipleObjects]
    public class PlayerJumpEditor : Editor
    {
        public PlayerJump playerJump;
        public void Awake()
        {
            playerJump = (PlayerJump)target;

        }
        public override void OnInspectorGUI()
        {

            playerJump.jumpForce = EditorGUILayout.FloatField("Jump Force", playerJump.jumpForce);

            playerJump.enableGroundChecks = EditorGUILayout.Toggle("Enable Ground Checks?", playerJump.enableGroundChecks);
            if (playerJump.enableGroundChecks)
            {
                playerJump.groundCheck = EditorGUILayout.ObjectField("Ground Check", playerJump.groundCheck, typeof(Transform)) as Transform;
                // playerJump.groundLayer = EditorGUILayout.LayerField("Ground Layer", playerJump.groundLayer);
                playerJump.groundLayer = LayerMaskField("Ground Layer", playerJump.groundLayer);
            }

            playerJump.enableJumpCut = EditorGUILayout.Toggle("Enable Jump Cut?", playerJump.enableJumpCut);

            playerJump.increaseGWhenFalling = EditorGUILayout.Toggle("Enable Increased Gravity When Falling?", playerJump.increaseGWhenFalling);
            if (playerJump.increaseGWhenFalling)
            {
                playerJump.defaultGravity = EditorGUILayout.FloatField("Default Gravity", playerJump.defaultGravity);
                playerJump.fallingGravity = EditorGUILayout.FloatField("Falling Gravity", playerJump.fallingGravity);
                playerJump.maxFallSpeed = EditorGUILayout.FloatField("Max Fall Speed", playerJump.maxFallSpeed);
            }

            playerJump.enableCoyoteTime = EditorGUILayout.Toggle("Enable Coyote Time?", playerJump.enableCoyoteTime);
            if (playerJump.enableCoyoteTime)
                playerJump.coyoteTime = EditorGUILayout.FloatField("Coyote Time Duration", playerJump.coyoteTime);

            playerJump.enableJumpTimeCooldown = EditorGUILayout.Toggle("Enable Jump Time Cooldown?", playerJump.enableJumpTimeCooldown);
            if (playerJump.enableJumpTimeCooldown)
                playerJump.jumpInputBuffer = EditorGUILayout.FloatField("Jump Time Cooldown", playerJump.jumpInputBuffer);

            DrawDefaultInspector();

            EditorUtility.SetDirty(playerJump);
        }

        static LayerMask LayerMaskField(string label, LayerMask layerMask)
        {
            List<string> layers = new List<string>();
            List<int> layerNumbers = new List<int>();

            for (int i = 0; i < 32; i++)
            {
                string layerName = LayerMask.LayerToName(i);
                if (layerName != "")
                {
                    layers.Add(layerName);
                    layerNumbers.Add(i);
                }
            }
            int maskWithoutEmpty = 0;
            for (int i = 0; i < layerNumbers.Count; i++)
            {
                if (((1 << layerNumbers[i]) & layerMask.value) > 0)
                    maskWithoutEmpty |= (1 << i);
            }
            maskWithoutEmpty = EditorGUILayout.MaskField(label, maskWithoutEmpty, layers.ToArray());
            int mask = 0;
            for (int i = 0; i < layerNumbers.Count; i++)
            {
                if ((maskWithoutEmpty & (1 << i)) > 0)
                    mask |= (1 << layerNumbers[i]);
            }
            layerMask.value = mask;
            return layerMask;
        }
    }
#endif
}

// Note to self: How to craft better jumping

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