using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private GameObject player;
    private GameManager gameManager;
    [SerializeField] private CollisionType collisionType;

    private enum CollisionType { LOSE, WIN };

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Debug.Log(GameObject.FindGameObjectWithTag("Player").name);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameManager) return;
        if (collision.gameObject != player) return;
        if (collisionType == CollisionType.LOSE) gameManager.Lose();
        else if (collisionType == CollisionType.WIN) gameManager.Win();
    }
}
