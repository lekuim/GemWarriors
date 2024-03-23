using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameEngine : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemyList;
    [SerializeField] private GameObject pauseBoard;
    private Enemy enemy;
    private Player player;
    private int i;
    [SerializeField] private Board board;
    [SerializeField] private Timer TimerLine;
    [SerializeField] private TMP_Text attaks;
    [SerializeField] private TMP_Text getReady;
    public float timeAmount;

    private void Start()
    {
        timeAmount = 6;
        i = 0;
        player = FindObjectOfType<Player>();
        StartCoroutine(GenerateEnemy());

    }

    IEnumerator GenerateEnemy()
    {
        GameOnPause();
        getReady.enabled = true;


        yield return new WaitForSeconds(3);


        int t = Random.Range(0, enemyList.Length);
        Instantiate(enemyList[t]); 
        enemy = FindObjectOfType<Enemy>();
        enemy.ShowState(enemy.atkLoop[i]);
        GameOnRun();
        getReady.enabled = false;
        InvokeRepeating("GenerateAtk", timeAmount, timeAmount + 2); // ќжидани€е врем€ атаки + врем€ на заглушку
    }

    void GenerateAtk()
    {

        if (i == enemy.atkLoop.Length - 1)
        {
            i = 0;
        }
        ++i;
        StartCoroutine(Attack());

    }

    private void GameOnPause()
    {
        TimerLine.resetTimer();
        pauseBoard.SetActive(true);
        board.enabled = false;
        TimerLine.enabled = false;
    }

    private void GameOnRun()
    {
        pauseBoard.SetActive(false);
        board.enabled = true;
        TimerLine.enabled = true;
    }
    IEnumerator Attack()
    {
        board.Alternate();
        GameOnPause();
        attaks.enabled = true;
        CompareAtk();
        enemy.hideState();
        player.hideState();
        enemy.ShowHp();
        player.ShowHp();
        yield return new WaitForSeconds(2f);
        attaks.enabled = false;
        enemy.ShowState(enemy.atkLoop[i]);
        enemy.ShowHp();
        GameOnRun();
        if (enemy.curHp <= 0)
        {
            enemy.killMe();
            enemy.hideState();
            CancelInvoke();
            StartCoroutine(GenerateEnemy());
        }



    }
    // 1->2. 2->3 3->1
    private void CompareAtk()
    {
        if (enemy.curState == player.curState)
            return;
        switch (player.curState)
        {
            case 1:
                if (enemy.curState == 2)
                    enemy.curHp -= 1;
                break;
            case 2:
                if (enemy.curState == 3)
                    enemy.curHp -= 1;
                break;
            case 3:
                if (enemy.curState == 1)
                    enemy.curHp -= 1;
                break;
            default:
                player.curHp -= 1;
                break;
        }
    }
}
