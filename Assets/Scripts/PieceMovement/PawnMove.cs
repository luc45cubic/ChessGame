using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMove : MonoBehaviour
{
    public Transform piecePlace;

    public GameObject Pawn;

    private Vector2 initialPosition;

    private Vector2 mousePosition;


    private float deltaX, deltaY;

    public static bool locked;

    int possibleSquares = 2;


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
            PawnMovement();


        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
    }
    void PawnMovement()
    {
        if (!(Mathf.Floor(mousePosition.x) < -3.5 || Mathf.Floor(mousePosition.y) < -4 || Mathf.Floor(mousePosition.x) > 4.5 || Mathf.Floor(mousePosition.y) > 3.5))
        {
            if (Pawn.name.Contains("White")) { 
                if (Mathf.Floor(mousePosition.x) + 0.5f == initialPosition.x && Mathf.Floor(piecePlace.position.y) > initialPosition.y && Mathf.Floor(piecePlace.position.y) < initialPosition.y + possibleSquares)
                {

                    transform.position = new Vector2(Mathf.Floor(piecePlace.position.x) + 0.5f, Mathf.Floor(piecePlace.position.y) + 0.5f);

                    initialPosition.x = Mathf.Floor(piecePlace.position.x) + 0.5f;
                    initialPosition.y = Mathf.Floor(piecePlace.position.y) + 0.5f;
                    possibleSquares = 1;
                }
                else
                {
                    transform.position = new Vector2(initialPosition.x, initialPosition.y);
                }
            }
            else
            {
                
                if (Mathf.Floor(mousePosition.x) + 0.5f == initialPosition.x && Mathf.Floor(piecePlace.position.y) < initialPosition.y && Mathf.Floor(piecePlace.position.y) > Mathf.Floor(initialPosition.y) - (possibleSquares+0.5))
                {

                    transform.position = new Vector2(Mathf.Floor(piecePlace.position.x) + 0.5f, Mathf.Floor(piecePlace.position.y) + 0.5f);

                    initialPosition.x = Mathf.Floor(piecePlace.position.x) + 0.5f;
                    initialPosition.y = Mathf.Floor(piecePlace.position.y) + 0.5f;
                    possibleSquares = 1;
                }
                else
                {
                    transform.position = new Vector2(initialPosition.x, initialPosition.y);
                }
            }
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
    }
}
