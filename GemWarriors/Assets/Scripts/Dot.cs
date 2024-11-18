using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject chsdPref;
    private Board board;
    public int dotX;
    public int dotY;
    
    void Start()
    {
        board = FindObjectOfType<Board>();
        dotX = (int)transform.localPosition.x;
        dotY = (int)transform.localPosition.y;
        board.dotsToDestroy = new List<Dot>();
    }

    // Update is called once per frame

    private void OnMouseDown()
    {
        if (board.enabled)
        {
            board.CurDot = this;
            board.CurDotPos = this.transform.localPosition;
            if (!board.dotsToDestroy.Contains(this))
            {
                board.dotsToDestroy.Add(this);
                Instantiate(chsdPref, this.transform);
            }
        }
    }
    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0) && board.CurDot != null)
        {
            Color thisColor = this.GetComponent<SpriteRenderer>().color;
            Color curDotColor = board.CurDot.GetComponent<SpriteRenderer>().color;

            if (!board.dotsToDestroy.Contains(this) && thisColor.Equals(curDotColor))
            {
                board.range = (int)((board.CurDotPos - (Vector2)this.transform.localPosition).magnitude);
                if (board.range <= 1)
                {
                    board.dotsToDestroy.Add(this);
                    Instantiate(chsdPref, this.transform);
                    board.CurDotPos = this.transform.localPosition;
                }
            }
        }


    }
    private void OnMouseUp()
    {
        board.DestroyDots();
    }

}
