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
    [SerializeField] private Image TimerLine;
    [SerializeField] private TMP_Text attaks;
    public float timeAmount;

    private void Start()
    {
        timeAmount = 6;
        GenerateEnemy();
        i = 0;
        player = FindObjectOfType<Player>();
        enemy.ShowState(enemy.atkLoop[i]);
        InvokeRepeating("GenerateAtk", timeAmount, timeAmount + 2); // Ожиданияе время атаки + время на заглушку
    }

    void GenerateEnemy()
    {
        int t = Random.Range(0, enemyList.Length);
        Instantiate(enemyList[t]); 
        enemy = FindObjectOfType<Enemy>();
    }

    void GenerateAtk()
    {
        if (i == enemy.atkLoop.Length - 1)
        {
            i = 0;
        }
        ++i;
        StartCoroutine(pause());
            
    }
    IEnumerator pause()
    {
        board.Alternate();
        pauseBoard.SetActive(true);
        board.enabled = false;
        attaks.enabled = true;
        TimerLine.enabled = false;
        CompareAtk();
        enemy.hideState();
        player.hideState();
        enemy.ShowHp();
        player.ShowHp();
        yield return new WaitForSeconds(2f);
        board.enabled = true;
        pauseBoard.SetActive(false);
        attaks.enabled = false;
        TimerLine.enabled = true;
        Debug.Log("Состояние изменилось");
        enemy.ShowState(enemy.atkLoop[i]);
        enemy.ShowHp();

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
