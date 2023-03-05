using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
