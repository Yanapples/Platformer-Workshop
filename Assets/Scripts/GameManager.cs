using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private AudioSource bgm;
    [Range(0f, 1f)]
    [SerializeField] private float defaultVolume;
    [Range(0f, 1f)]
    [SerializeField] private float inactiveVolume;

    public bool IsPlay { get; set; } = true;
    // Start is called before the first frame update
    void Start()
    {
        bgm.volume = defaultVolume;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
    }

    public void Lose()
    {
        loseScreen.SetActive(true);
        IsPlay = false;
        Time.timeScale = 0;
        bgm.volume = inactiveVolume;
    }

    public void Win()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0;
        bgm.volume = inactiveVolume;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        bgm.volume = inactiveVolume;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        bgm.volume = defaultVolume;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        bgm.volume = defaultVolume;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
