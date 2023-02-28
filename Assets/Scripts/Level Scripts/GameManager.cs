using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerRun player;
    private Vector2 startPos;
    public GameObject winScreen;
    public GameObject loseScreen;

    public bool IsPlay { get; set; } = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRun>();
        startPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Restart();
    }

    public void Lose()
    {
        loseScreen.SetActive(true);
        IsPlay = false;
        Time.timeScale = 0;
    }

    public void Win()
    {
        winScreen.SetActive(true);
        IsPlay = false;
        Time.timeScale = 0;
    }

    public void Restart()
    {
        if (IsPlay == false && Input.GetKeyDown(KeyCode.R))
        {
            IsPlay = true;
            player.transform.position = startPos;
            winScreen.SetActive(false);
            loseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
