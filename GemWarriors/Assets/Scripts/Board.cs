using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    // Start is called before the first frame update
    public int width; // ширина поля
    public int height; // высота поля
    public int shadowFactor;
    public int maxShd;
    public GameObject tilePrefab; // префаб Baground
    public GameObject[] dots;//Массив из возможный фигу
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
        Vector2 tempPos = new Vector2(i, j); // получаем вектор очередной координаты
        GameObject bacgroundTile = (GameObject)Instantiate(tilePrefab, tempPos, Quaternion.identity);//Instantiate(создаем объект типа tilePrefab, с координамати вектора tempPos, со стандартным наклоном) передаем в bacgroundTile
        bacgroundTile.transform.parent = this.transform; //Делаем родителем объекта bacgroundTile объект класса Background
        bacgroundTile.name = "( " + i + "," + j + " )"; //Меняем имя объекта bacgroundTile

        int dotToUse = Random.Range(0, dots.Length);//Генерация случайного числа который определит фигуру
        if (dotToUse == dots.Length - 1)
        {
            if (maxShd >= shadowFactor)
            {
                dotToUse = Random.Range(0, dots.Length - 1);
            }
            else
                maxShd++;

        }
        GameObject dot = Instantiate(dots[dotToUse], tempPos, Quaternion.identity);//Клонирование фигуры с типом dots[dotToUse] в положении tranform, со стандартным поворотом
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

        Debug.Log("КОНЕЦ");
    }


}
