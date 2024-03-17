using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    Image timerBar;
    public float timerInterval = 6f;
    float timeLeft;
    void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = timerInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(timeLeft > 0))
        {
            timeLeft = timerInterval;
            timerBar.fillAmount = 1;
        }
        if (timerBar.enabled)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / timerInterval;
        }
    }
}
