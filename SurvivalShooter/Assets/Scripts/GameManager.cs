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
        isPause = !isPause;
        if (isPause)
            Pause();
        else
            Unpause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
    }
}
