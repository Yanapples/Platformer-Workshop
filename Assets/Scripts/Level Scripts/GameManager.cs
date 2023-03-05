using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    private Transform player;
    private Vector2 startPos;

    public bool IsPlay { get; set; } = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        startPos = player.transform.position;
    }

    public void Lose()
    {
        //loseScreen.SetActive(true);
        //IsPlay = false;
        //Time.timeScale = 0;
        Debug.Log("Dead");
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
            //IsPlay = true;
            //player.position = startPos;
            //winScreen.SetActive(false);
            //loseScreen.SetActive(false);
            //Time.timeScale = 1;

            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
