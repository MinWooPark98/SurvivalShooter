using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    private static UiManager m_instance;
    public static UiManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<UiManager>();
            return m_instance;
        }
    }

    public TextMeshProUGUI ScoreText;
    public Slider hpBar;
    public GameObject bloodScreen;
    public float hitEffectDuration = 0.3f;
    public GameObject gameOverScreen;
    public float gameOverDuration = 2f;
    public Slider BgmVolume;
    public Slider EffectsVolume;

    public void SetScoreText(int score)
    {
        ScoreText.text = "Score: " + score.ToString();
    }

    public void SetHpBar(float hpPercentage)
    {
        hpBar.value = hpPercentage;
    }

    public IEnumerator HitEffect()
    {
        bloodScreen.SetActive(true);
        yield return new WaitForSeconds(hitEffectDuration);
        bloodScreen.SetActive(false);
    }

    public IEnumerator GameOver()
    {
        gameOverScreen.SetActive(true);
        yield return new WaitForSeconds(gameOverDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
