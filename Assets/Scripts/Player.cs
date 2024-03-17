using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public int curHp;
    public int Hp;
    public int curState;
    public GameObject[] states;
    [SerializeField] private TMP_Text hpText;

    private void Start()
    {
        ShowHp();
        ShowState();
    }

    public void ShowHp()
    {
        hpText.text = curHp.ToString() + "/" + Hp.ToString();
    }
    public void hideState()
    {
        curState = 0;
        Destroy(GameObject.FindGameObjectWithTag("state"));
    }

    public void ShowState()
    {
        Destroy(GameObject.FindGameObjectWithTag("state"));
        switch (curState)
        {
            case 1:
                Instantiate(states[0]);
                break;
            case 2:
                Instantiate(states[1]);
                break;
            case 3:
                Instantiate(states[2]);
                break;
        }

    }

    public void SetState(int state)
    {
        curState = state;
    }
}
