using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour {

    [SerializeField] private int timeLeft;

    //public int timeLeft = 5;
    public Text countdownText;
    public Text GameOver;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("LoseTime");
    }

    // Game Over
    IEnumerator GameOverWait()
    {
        GameOver.text = ("GAME OVER");
        Time.timeScale = 0.00001f;
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    // Update is called once per frame
    void Update()
    {
        int min = Mathf.FloorToInt(timeLeft / 60);
        int sec = Mathf.FloorToInt(timeLeft % 60);
        countdownText.text = ("Time Left: " + min.ToString("00") + ":" + sec.ToString("00"));

        if (timeLeft <= 0)
        {
            countdownText.text = ("Time Left: 00:00");
            StartCoroutine(GameOverWait());
        }
    }

    // Lose Time
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}
