using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    Image timerBar;
    public float timerInterval;
    private float timeLeft;
    [SerializeField] private TMP_Text sec;
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
            timerBar.fillAmount = timeLeft / timerInterval;
            sec.text = timeLeft.ToString("#.#") + "s";
        }
        if(timeLeft <= 0)
        {
            sec.text = "";
        }
    }

    public void resetTimer()
    {
        timeLeft = timerInterval;
        timerBar.fillAmount = 1;
    }
}
