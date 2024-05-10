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
    [SerializeField] private TMP_Text getReady;
    [SerializeField] private Score score;
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
        board.numberOfChains = 0;
        score.idRank = 0;
        score.ShowScr();
        yield return new WaitForSeconds(3);
        int t = Random.Range(0, enemyList.Length);
        Instantiate(enemyList[t], GameObject.FindGameObjectWithTag("Canvas").transform); 
        enemy = FindObjectOfType<Enemy>();
        enemy.ShowState(enemy.atkLoop[i]);
        GameOnRun();
        getReady.enabled = false;
        InvokeRepeating("GenerateAtk", timeAmount, timeAmount + 2); // Ожиданияе время атаки + время на заглушку
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
        CompareAtk();
        enemy.hideState();
        player.hideState();
        enemy.ShowHp();
        player.ShowHp();
        yield return new WaitForSeconds(2f);
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
        if (player.curHp <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }



    }
    // 1->2. 2->3 3->1
    private void CompareAtk()
    {

        if((enemy.curState == 2 && player.curState == 1) || 
            (enemy.curState == 3 && player.curState == 2) || 
            (enemy.curState == 1 && player.curState == 3)) //АТАКА
        {
            enemy.curHp -= 1;
            score.score += 500 * score.multi[score.idRank];
            score.ShowMove("ATTACK", 500 * score.multi[score.idRank]);
            score.ShowScr();
        }
        else if (enemy.curState == player.curState) // БЛОК
        {
            score.score += 100 * score.multi[score.idRank];
            score.ShowMove("BLOCK", 100 * score.multi[score.idRank]);
            score.ShowScr();
        }
        else
        {
            score.ShowMove("MISSED", 0);
            player.curHp -= 1;
        }

    }
}
