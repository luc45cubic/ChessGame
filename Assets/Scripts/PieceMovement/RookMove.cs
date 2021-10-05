using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookMove : MonoBehaviour
{
    public Transform piecePlace;


    private Vector2 initialPosition;

    private Vector2 mousePosition;
    

    private float deltaX, deltaY;

    public static bool locked;



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
            RookMovement();

            
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
    }
    void RookMovement()
    {
        if (!(Mathf.Floor(mousePosition.x) < -3.5 || Mathf.Floor(mousePosition.y) < -4 || Mathf.Floor(mousePosition.x) > 4.5 || Mathf.Floor(mousePosition.y) > 3.5))
        {
            if (Mathf.Floor(piecePlace.position.x) == Mathf.Floor(initialPosition.x) && Mathf.Floor(piecePlace.position.y) != initialPosition.y || Mathf.Floor(piecePlace.position.x) != initialPosition.x && Mathf.Floor(piecePlace.position.y) == Mathf.Floor(initialPosition.y))
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
