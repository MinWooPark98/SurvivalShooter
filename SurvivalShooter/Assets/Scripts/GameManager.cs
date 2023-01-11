using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<GameManager>();
            return m_instance;
        }
    }

    public int score { get; private set; }
    public bool isPause { get; private set; }
    public GameObject pausePanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SwitchPause();
    }

    public void AddScore(int add)
    {
        score += add;
        UiManager.instance.SetScoreText(score);
    }

    public void SwitchPause()
    {
        if (!isPause)
            Pause();
        else
            Unpause();
    }

    public void Pause()
    {
        isPause = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void Unpause()
    {
        isPause = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void EndGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;    // 유니티에서 테스트 할 때
        Application.Quit();                                 // 빌드한 후에 어플에서
    }
}
