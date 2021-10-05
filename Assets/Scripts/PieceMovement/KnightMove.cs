using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KnightMove : MonoBehaviour
{
    public Transform piecePlace;


    private Vector2 initialPosition;

    private Vector2 mousePosition;


    private float deltaX, deltaY;

    public static bool locked;

    private int[] Pos = { -2, 2 };
    private int[] PosTwo = { -1, 1, };

    void Start()
    {
        initialPosition = transform.position;
        piecePlace.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnMouseDown()
    {
        locked = false;
        if (!locked)
        {


            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;

            //
        }
    }

    private void OnMouseDrag()
    {
        if (!locked)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
        }


    }

    private void OnMouseUp()
    {
        if (Mathf.Abs(transform.position.x - piecePlace.position.x) <= 0.5f && Mathf.Abs(transform.position.y - piecePlace.position.y) <= 0.5f)
        {
            KnightMovement();


        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
    }
    void KnightMovement()
    {
        
        if (!(Mathf.Floor(mousePosition.x) < -3.5 || Mathf.Floor(mousePosition.y) < -4 || Mathf.Floor(mousePosition.x) > 4.5 || Mathf.Floor(mousePosition.y) > 3.5))
        {
            

            if (Pos.Contains((int)(Mathf.Floor(piecePlace.position.x) - Mathf.Floor(initialPosition.x))) && PosTwo.Contains((int)(Mathf.Floor(piecePlace.position.y) - Mathf.Floor(initialPosition.y))) || PosTwo.Contains((int)(Mathf.Floor(piecePlace.position.x) - Mathf.Floor(initialPosition.x))) && Pos.Contains((int)(Mathf.Floor(piecePlace.position.y) - Mathf.Floor(initialPosition.y))))
            {

                transform.position = new Vector2(Mathf.Floor(piecePlace.position.x) + 0.5f, Mathf.Floor(piecePlace.position.y) + 0.5f);

                initialPosition.x = Mathf.Floor(piecePlace.position.x) + 0.5f;
                initialPosition.y = Mathf.Floor(piecePlace.position.y) + 0.5f;

            }
            else
            {
                transform.position = new Vector2(initialPosition.x, initialPosition.y);
            }
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
    }
}
