using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

    [SerializeField] private int timeLeft;

    //public int timeLeft = 5;
    public Text countdownText;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("LoseTime");
    }

    // Update is called once per frame
    void Update()
    {
        int min = Mathf.FloorToInt(timeLeft / 60);
        int sec = Mathf.FloorToInt(timeLeft % 60);
        countdownText.text = ("Time Left: " + min.ToString("00") + ":" + sec.ToString("00"));

        if (timeLeft <= 0)
        {
            StopCoroutine("LoseTime");
            countdownText.text = ("Time Left: Times Up!");
        }
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}
