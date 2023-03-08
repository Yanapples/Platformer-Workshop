using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateTextBox : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Mass = " + rb.mass + "\nGravity = " + rb.gravityScale;
    }
}
