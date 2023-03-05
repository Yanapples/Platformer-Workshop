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
    [SerializeField] private AudioSource win;
    [SerializeField] private AudioSource lose;

    public bool IsPlay { get; set; } = true;
    // Start is called before the first frame update
    void Start()
    {
        SetBGMVolume(defaultVolume);
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
        SetBGMVolume(inactiveVolume);

        if (lose) lose.Play();
    }

    public void Win()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0;
        SetBGMVolume(inactiveVolume);

        if (win) win.Play();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        SetBGMVolume(inactiveVolume);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        SetBGMVolume(defaultVolume);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        SetBGMVolume(defaultVolume);
    }

    private void SetBGMVolume(float volume)
    {
        if (bgm) bgm.volume = volume;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
