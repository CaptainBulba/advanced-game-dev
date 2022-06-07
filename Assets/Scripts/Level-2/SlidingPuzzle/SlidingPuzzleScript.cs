using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzleScript : MonoBehaviour
{
    public NumberBox boxPrefab;
    public NumberBox[,] boxes = new NumberBox[4,4];
    public Sprite[] sprites;
    Vector2 lastMove;

    private LevelController levelController;
    private GameObject puzzleButton;

    public GameObject inventoryItem;
  
    void Start()
    {
        levelController = GameObject.Find("LevelController").GetComponent<LevelTwoController>();
        puzzleButton = GetComponent<PrefabSettings>().GetButton();

        //Initilize the board
        Init();

        //Prepare the game by shuffling tiles
        for(int i = 0; i < 5; i++)
        {
            Shuffle();
        }
    }

    private void OnEnable()
    {
        NumberBox.OnSwap += CheckWinningCondition;
    }

    private void OnDisable()
    {
        NumberBox.OnSwap -= CheckWinningCondition;
    }

    private void CheckWinningCondition()
    {
        for(int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!boxes[i, j].IsCorrectPlace())
                {
                    //if at least one is not in a correct position return (exit function)
                    return;
                }
            }
        }

        Inventory.Instance.AddItem(inventoryItem);
        //exit puzzle and return to level once all tiles are at their correct locations
        levelController.LaunchMainScreen(puzzleButton);
    }


    //To setup the puzzle board based on the sprite image we provided
    void Init()
    {
        int n = 0;
        for(int y=3; y>=0; y--)
        {
            for(int x = 0; x<4; x++)
            {
                NumberBox box = Instantiate(boxPrefab, new Vector2 (x, y), Quaternion.identity,this.transform);
                
                box.Init(x, y, n + 1, sprites[n], ClickToSwap);
                boxes[x,y] = box;
                n++;
            }
        }
    }

    void ClickToSwap(int x, int y)
    {
        int dx = GetNewX(x, y);
        int dy = GetNewY(x, y);
        Swap(x, y, dx, dy); 

    }

    void Swap(int x,int y, int dx, int dy)
    {
        var from = boxes[x,y];
        var target = boxes[x + dx, y + dy];

        //swap the boxes
        boxes[x, y] = target;
        boxes[x + dx, y + dy] = from;

        //update the position
        from.UpdatePos(x + dx, y + dy);
        target.UpdatePos(x, y);
    }

    int GetNewX(int x, int y)
    {
        //Check if the right NumberBox tile is empty
        if (x < 3 && boxes[x + 1, y].IsEmpty())
            return 1;

        //Check if the left NumberBox tile is empty
        if (x > 0 && boxes[x - 1, y].IsEmpty())
            return -1;
    
        return 0;

    }
    int GetNewY(int x, int y)
    {
        //Check if the top NumberBox tile is empty
        if (y < 3 && boxes[x, y + 1].IsEmpty())
            return 1;

        //Check if the bottom NumberBox tile is empty
        if (y > 0 && boxes[x, y - 1].IsEmpty())
            return -1;

        return 0;

    }

    void Shuffle()
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                //Check if the tile is empty and then swap it with an adjacent tile
                if (boxes[i, j].IsEmpty())
                {
                    Vector2 pos = GetValidMove(i, j);
                    Swap(i,j,(int)pos.x, (int)pos.y);
                }
            }
        }
    }

    Vector2 GetValidMove(int x, int y)
    {
        Vector2 pos = new Vector2();

        do
        {
            int n = Random.Range(0, 4);
            switch (n)
            {
                case 0:
                    pos = Vector2.left;
                    break;
                case 1:
                    pos = Vector2.right;
                    break;
                case 2:
                    pos = Vector2.up;
                    break;
                case 3:
                    pos = Vector2.down;
                    break;
            }
        }while(!(IsValidRange(x + (int)pos.x) && IsValidRange(y + (int)pos.y)) || IsRepeatMove(pos));

        lastMove = pos;
        return pos;
    }

    //To ensure that we don't go outside the boundaries of the board
    bool IsValidRange(int n)
    {
        return n >= 0 && n <= 3;
    }

    //To ensure we don't swap and unswap the same tile during shuffling process
    bool IsRepeatMove(Vector2 pos)
    {
        return pos * -1 == lastMove; 

    }


}
