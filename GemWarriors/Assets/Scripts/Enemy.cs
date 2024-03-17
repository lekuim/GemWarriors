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
    public void ShowState(int State)
    {
        switch (State)
        {
            case 1:
                curState = 1;
                Instantiate(states[0]);
                break;
            case 2:
                curState = 2;
                Instantiate(states[1]);
                break;
            case 3:
                curState = 3;
                Instantiate(states[2]);
                break;
            case 4:
                curState = Random.Range(1, 4);
                Instantiate(states[curState - 1]);
                break;
        }

    }
}
