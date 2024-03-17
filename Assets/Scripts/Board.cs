using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    // Start is called before the first frame update
    public int width; // ������ ����
    public int height; // ������ ����
    public int shadowFactor;
    public int maxShd;
    public GameObject tilePrefab; // ������ Baground
    public GameObject[] dots;//������ �� ��������� ����
    public GameObject[,] allDots;
    public List<Dot> dotsToDestroy;
    public GameEngine engine;
    public Dot CurDot;
    public Vector2 CurDotPos;
    public int range;
    public Player player;
    void Start()
    {
        maxShd = 0;
        allDots = new GameObject[width,height];
        SetUp();
    }



    public void Alternate()
    {
        for (int i = 0; i < dotsToDestroy.Count; i++)
        {
            int targetX = (int)dotsToDestroy[i].transform.position.x;
            int targetY = (int)dotsToDestroy[i].transform.position.y;
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
    }

    public void GetDot(int i, int j)
    {
        Vector2 tempPos = new Vector2(i, j); // �������� ������ ��������� ����������
        GameObject bacgroundTile = (GameObject)Instantiate(tilePrefab, tempPos, Quaternion.identity);//Instantiate(������� ������ ���� tilePrefab, � ������������ ������� tempPos, �� ����������� ��������) �������� � bacgroundTile
        bacgroundTile.transform.parent = this.transform; //������ ��������� ������� bacgroundTile ������ ������ Background
        bacgroundTile.name = "( " + i + "," + j + " )"; //������ ��� ������� bacgroundTile

        int dotToUse = Random.Range(0, dots.Length);//��������� ���������� ����� ������� ��������� ������
        if (dotToUse == dots.Length - 1)
        {
            if (maxShd >= shadowFactor)
            {
                dotToUse = Random.Range(0, dots.Length - 1);
            }
            else
                maxShd++;

        }
        GameObject dot = Instantiate(dots[dotToUse], tempPos, Quaternion.identity);//������������ ������ � ����� dots[dotToUse] � ��������� tranform, �� ����������� ���������
        dot.transform.parent = this.transform;
        dot.name = "( " + i + "," + j + " )";
        allDots[i, j] = dot;
    }


    public void DestroyDots()
    {
        if (dotsToDestroy.Count >= 3) 
        {

                switch (dotsToDestroy[0].tag)
                {
                case "blue":
                    player.SetState(1);
                    break;
                case "red":
                    player.SetState(2);
                    break;
                case "green":
                    player.SetState(3);
                    break;
                }
            player.ShowState();

            for (int i = 0; i < dotsToDestroy.Count; i++)
            {
                int targetX = (int)dotsToDestroy[i].transform.position.x;
                int targetY = (int)dotsToDestroy[i].transform.position.y;
                if (allDots[targetX, targetY].CompareTag("ShadowDot"))
                {
                    maxShd--;
                }
                Destroy(allDots[targetX, targetY]);
                allDots[targetX, targetY] = null;
                GetDot(targetX, targetY);
            }
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
