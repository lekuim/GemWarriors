using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    Image timerBar;
    public float timerInterval = 6f;
    float timeLeft;
    [SerializeField] TMP_Text sec;
    void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = timerInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerBar.enabled)
        {
            timeLeft -= Time.deltaTime;
            sec.text = timeLeft.ToString("#.###") + "s";
            timerBar.fillAmount = timeLeft / timerInterval;
        }
    }

    public void resetTimer()
    {
        timeLeft = timerInterval;
        timerBar.fillAmount = 1;
    }
}
