using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBox : MonoBehaviour
{
    //Internal variables related to the position and value of the Numberbox/tile
    public int index = 0;
    int x =0;
    int y =0;



    private Action<int, int> swapFunc = null;

    public void Init(int i, int j, int index, Sprite sprite, Action<int,int> swapFunc)
    {
        this.index = index;
        this.GetComponent<SpriteRenderer>().sprite = sprite;
        UpdatePos(i, j);
        //To save the current box we have clicked
        this.swapFunc = swapFunc;
    }

    //To update the position of the NumberBox tile based on the new position i,j
    public void UpdatePos(int i, int j)
    {
        x = i;
        y = j;

        StartCoroutine(Move());

        //This was replaced by the Coroutine to introduce a smooth animation to the movement of the block/tile sliding
        //this.gameObject.transform.localPosition = new Vector2(i,j);
    }

    IEnumerator Move()
    {
        //To control speed of tile movement
        float elapsedTime = 0;
        float duration = 0.2f;

        Vector2 start = this.gameObject.transform.localPosition;
        Vector2 end = new Vector2(x, y);

        while (elapsedTime < duration)
        {
            //Lerp: a mathematical function in Unity that returns a value between two others at a point on a linear scale
            this.gameObject.transform.localPosition = Vector2.Lerp(start, end, elapsedTime/duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        this.gameObject.transform.localPosition = end;
    }

    public bool IsEmpty()
    {
        //If the index of the current Number/Tile Box is equal to 16 means it is the empty box
        return index == 16;
    }
    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0) && swapFunc !=null)
        {
            swapFunc(x,y);
        }
    }
}
