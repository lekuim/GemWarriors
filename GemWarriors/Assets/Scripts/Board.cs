using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int width; // ������ ����
    [SerializeField] private int height; // ������ ����
    public int shadowFactor;
    [SerializeField] private int maxShd;
    [SerializeField] private GameObject tilePrefab; // ������ Baground
    [SerializeField] private GameObject[] dots;//������ �� ��������� ����
    private GameObject[,] allDots;
    public List<Dot> dotsToDestroy;
    [SerializeField] private GameEngine engine;
    public Dot CurDot;
    public Vector2 CurDotPos;
    public int range;
    [SerializeField] private Player player;
    [SerializeField] private Score score;
    public int numberOfChains;

    void Start()
    {
        numberOfChains = 0;
        maxShd = 0;
        allDots = new GameObject[width,height];
        SetUp();
    }



    public void Alternate()
    {
        for (int i = 0; i < dotsToDestroy.Count; i++)
        {
            int targetX = (int)dotsToDestroy[i].transform.localPosition.x;
            int targetY = (int)dotsToDestroy[i].transform.localPosition.y;
            Destroy(allDots[targetX, targetY].transform.GetChild(0).gameObject);
        }
        dotsToDestroy.Clear();
        CurDot = null;
        CurDotPos = Vector2.zero;
    }



    private void SetUp()
    {
        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                    GetDot(i, j);
            }
        }
        Vector3 pos = new Vector3(-2.98f, -6.59f, 5f);
        this.transform.SetPositionAndRotation(pos, Quaternion.identity);
    }

    public void GetDot(int i, int j)
    {
        Vector2 tempPos = new Vector2(i, j); // �������� ������ ��������� ����������

        int dotToUse = Random.Range(0, dots.Length);//��������� ���������� ����� ������� ��������� ������
        //��������� ����////////////////////////////////
        if (dotToUse == dots.Length - 1)
        {
            if (maxShd >= shadowFactor)
            {
                dotToUse = Random.Range(0, dots.Length - 1);
            }
            else
                maxShd++;

        }
        ////////////////////////////////////////////////

        GameObject dot = Instantiate(dots[dotToUse], this.transform);//������������ ������ � ����� dots[dotToUse] � ��������� tranform, �� ����������� ���������
        dot.transform.localPosition = tempPos;
        //dot.transform.parent = this.transform;
        dot.name = "( " + i + "," + j + " )";
        allDots[i, j] = dot;
    }

    public void DestroyDots()
    {
        if (dotsToDestroy.Count >= 3) 
        {
             numberOfChains++;
            score.CheckRank(numberOfChains);
                switch (dotsToDestroy[0].tag)
                {
                case "blue":
                    player.curState = 1;
                    break;
                case "red":
                    player.curState = 2;
                    break;
                case "green":
                    player.curState = 3;
                    break;
                }
            player.ShowState();




            for (int i = 0; i < dotsToDestroy.Count; i++)
            {
                int targetX = (int)dotsToDestroy[i].transform.localPosition.x;
                int targetY = (int)dotsToDestroy[i].transform.localPosition.y;


                if (allDots[targetX, targetY].CompareTag("ShadowDot"))
                {
                    maxShd--;
                }

                Destroy(allDots[targetX, targetY]);
                allDots[targetX, targetY] = null;
                GetDot(targetX, targetY);
            }
            score.score += dotsToDestroy.Count * 5 * score.multi[score.idRank];
            score.ShowMove("CHAIN", dotsToDestroy.Count * 5 * score.multi[score.idRank]);
            score.ShowScr();
            dotsToDestroy.Clear();
            CurDot = null;
            CurDotPos = Vector2.zero;

        }
        else
        {
            Alternate();
        }

        Debug.Log("�����");
    }


}
