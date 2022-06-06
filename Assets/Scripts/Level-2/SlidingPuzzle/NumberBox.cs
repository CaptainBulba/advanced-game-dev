using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NumberBox : MonoBehaviour
{
    //Internal variables related to the position and value of the Numberbox/tile
    public int index = 0;
    public bool isCorrectPlace;
    int x =0;
    int y =0;

    private Action<int, int> swapFunc = null;
    
    public static UnityAction OnSwap;

    public void Init(int i, int j, int index, Sprite sprite, Action<int,int> swapFunc)
    {
        this.index = index;
        this.GetComponent<SpriteRenderer>().sprite = sprite;
        this.GetComponent<SpriteRenderer>().sortingOrder = 2;

        UpdatePos(i, j);
        //To save the current box we have clicked
        this.swapFunc = swapFunc;
    }

    //To update the position of the NumberBox tile based on the new position i,j
    public void UpdatePos(int i, int j)
    {
        x = i;
        y = j;

        CheckCorrectPosition();

        StartCoroutine(Move());

    }

    //This is to introduce a smooth animation to the movement of the block/tile sliding
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

    public void CheckCorrectPosition()
    {
        int checkIndex = 4 * (3 - y) + x + 1;
        if(checkIndex == index)
            isCorrectPlace = true;
        else
            isCorrectPlace = false;
        Debug.Log("The tile current index is " + checkIndex + " The actual index is " + index);
    }

    public bool IsCorrectPlace() { return isCorrectPlace; }
        
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
            OnSwap?.Invoke();
        }
    }
}
