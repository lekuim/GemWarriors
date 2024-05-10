using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public int[] atkLoop;
    public int Hp;
    public int curHp;
    public int RectSec;
    public int curState;
    public GameObject[] states;
    [SerializeField]private TMP_Text hpText;
    private void Start()
    {
        ShowHp();
    }
    public void ShowHp()
    {
        Destroy(GameObject.FindWithTag("enemyHp"));
        hpText.text = curHp.ToString() + "/" + Hp.ToString();
        Instantiate(hpText, GameObject.Find("Canvas").transform);
    }

    public void hideState()
    {
        Destroy(GameObject.FindGameObjectWithTag("eState"));
    }
    public void killMe()
    {
        Destroy(GameObject.FindGameObjectWithTag("enemy"));
    }
    public void ShowState(int State)
    {
        switch (State)
        {
            case 1:
                curState = 1;
                Instantiate(states[0], GameObject.FindGameObjectWithTag("Canvas").transform);
                break;
            case 2:
                curState = 2;
                Instantiate(states[1], GameObject.FindGameObjectWithTag("Canvas").transform);
                break;
            case 3:
                curState = 3;
                Instantiate(states[2], GameObject.FindGameObjectWithTag("Canvas").transform);
                break;
            case 4:
                int i = Random.Range(0, 3);
                curState = i + 1;
                Instantiate(states[i], GameObject.FindGameObjectWithTag("Canvas").transform);
                break;
        }

    }
}
