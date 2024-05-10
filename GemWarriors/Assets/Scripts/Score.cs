using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private string[] ranks;
    public int[] multi;
    public int idRank;
    public long score;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text move;
    private void Start()
    {
        ranks = new string[]{ "C", "B", "A", "S"};
        multi = new int[] {1,2,5,10};
        score = 0;
        idRank = 0;
        ShowScr();
    }

    public void ShowScr()
    {
        scoreText.text = ranks[idRank] + " | " + score + "\nx" + multi[idRank];
    }

    public void ShowMove(string type, int score)
    {
        move.text = type + "\n+" + score;
    }

    public void CheckRank(int n)
    {
        switch (n)
        {
            case 2:
                idRank++;
                break;
            case 4:
                idRank++;
                break;
            case 6:
                idRank++;
                break;
        }
    }

}
