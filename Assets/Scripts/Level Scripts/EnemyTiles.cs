using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTiles : MonoBehaviour
{
    // gm stands for Game Manager
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("lose");
        gm.Lose();
    }
}
