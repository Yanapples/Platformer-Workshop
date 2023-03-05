using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent onPlayerTouch;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") onPlayerTouch.Invoke();
    }
}
