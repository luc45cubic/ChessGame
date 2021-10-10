using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pieces : MonoBehaviour
{
    public Transform piecePlace;


    public GameObject Piece;

    private Vector2 initialPosition;

    private Vector2 mousePosition;
    
    
    private float deltaX, deltaY;

    public static bool locked;
    int possibleSquares = 2;

    public bool isBlackOnMove;
    public List<Vector3> WhitePOS = new List<Vector3>();
    public List<Vector3> BlackPOS = new List<Vector3>();

    public List<GameObject> WhitePIECES = new List<GameObject>();
    public List<GameObject> BlackPIECES = new List<GameObject>();


    private int[] Pos = { -2, 2 };
    private int[] PosTwo = { -1, 1, };

    public GameObject board_obj;

    void Start()
    { 
        initialPosition = transform.position;
        piecePlace.GetComponent<BoxCollider2D>().isTrigger = true;
        WhitePOS = board_obj.GetComponent<Board>().WhitePos;
        BlackPOS = board_obj.GetComponent<Board>().BlackPos;

        WhitePIECES = board_obj.GetComponent<Board>().WhitePieces;
        BlackPIECES = board_obj.GetComponent<Board>().BlackPieces;

    }
    private void Update()
    {
        isBlackOnMove = board_obj.GetComponent<Board>().IsBlackOnMove;
        
    }



    private void OnMouseDown()
    {
        if (!isBlackOnMove && Piece.name.Contains("White"))
        {
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        }
        else if (isBlackOnMove && Piece.name.Contains("Black"))
        {
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        }
    }
    private void OnMouseDrag()
    {
        if (!isBlackOnMove && Piece.name.Contains("White"))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
        }
        else if (isBlackOnMove && Piece.name.Contains("Black"))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
        }
    }

    private void OnMouseUp()
    {
        
        if (!(Mathf.Floor(mousePosition.x) < -3.5 || Mathf.Floor(mousePosition.y) < -4 || Mathf.Floor(mousePosition.x) > 4.5 || Mathf.Floor(mousePosition.y) > 3.5))
        {
            if (!isBlackOnMove && Piece.name.Contains("White"))
            {
                if (Piece.name.Contains("King"))
                    KingMovement(WhitePOS, BlackPIECES, BlackPOS);
                    
                else if (Piece.name.Contains("Knight"))
                    KnightMovement(WhitePOS, BlackPIECES, BlackPOS);
                else if (Piece.name.Contains("Queen"))
                    QueenMovement(WhitePOS, BlackPIECES, BlackPOS);
                else if (Piece.name.Contains("Pawn"))
                    PawnMovement(WhitePOS, BlackPIECES, BlackPOS);
                else if (Piece.name.Contains("Bishop"))
                    BishopMovement(WhitePOS, BlackPIECES, BlackPOS);
                else if (Piece.name.Contains("Rook"))
                    RookMovement(WhitePOS, BlackPIECES, BlackPOS);
            }
            else if(isBlackOnMove && Piece.name.Contains("Black"))
            {
                if (Piece.name.Contains("King"))
                    KingMovement(BlackPOS, WhitePIECES, WhitePOS);
                else if (Piece.name.Contains("Knight"))
                    KnightMovement(BlackPOS, WhitePIECES, WhitePOS);
                else if (Piece.name.Contains("Queen"))
                    QueenMovement(BlackPOS, WhitePIECES, WhitePOS);
                else if (Piece.name.Contains("Pawn"))
                    PawnMovement(BlackPOS, WhitePIECES, WhitePOS);
                else if (Piece.name.Contains("Bishop"))
                    BishopMovement(BlackPOS, WhitePIECES, WhitePOS);
                else if (Piece.name.Contains("Rook"))
                    RookMovement(BlackPOS, WhitePIECES, WhitePOS);
            }

        }

        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
        
    }

    void KnightMovement(List<Vector3> AttPiecesPos, List<GameObject> DefPieces, List<Vector3>DefPiecesPos)
    {

        if (Pos.Contains((int)(Mathf.Floor(piecePlace.position.x) - Mathf.Floor(initialPosition.x))) && PosTwo.Contains((int)(Mathf.Floor(piecePlace.position.y) - Mathf.Floor(initialPosition.y))) || PosTwo.Contains((int)(Mathf.Floor(piecePlace.position.x) - Mathf.Floor(initialPosition.x))) && Pos.Contains((int)(Mathf.Floor(piecePlace.position.y) - Mathf.Floor(initialPosition.y))))
        {

            PieceChanges(AttPiecesPos, DefPieces, DefPiecesPos);
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }

    }
    void QueenMovement(List<Vector3> AttPiecesPos, List<GameObject> DefPieces, List<Vector3> DefPiecesPos)
    {
        
        if (Mathf.Floor(piecePlace.position.x) == Mathf.Floor(initialPosition.x) || Mathf.Floor(piecePlace.position.y) == Mathf.Floor(initialPosition.y) || Mathf.Abs(Mathf.Floor(piecePlace.position.x) - Mathf.Floor(initialPosition.x)) == Mathf.Abs(Mathf.Floor(piecePlace.position.y) - Mathf.Floor(initialPosition.y)))
        {
            PieceChanges(AttPiecesPos, DefPieces, DefPiecesPos);
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
        
    }
    void PawnMovement(List<Vector3> AttPiecesPos, List<GameObject> DefPieces, List<Vector3> DefPiecesPos)
    {
        
        if (Piece.name.Contains("White"))
        {
            if (Mathf.Floor(mousePosition.x) + 0.5f == initialPosition.x && Mathf.Floor(piecePlace.position.y) > initialPosition.y && Mathf.Floor(piecePlace.position.y) < initialPosition.y + possibleSquares)
            {
                PieceChanges(AttPiecesPos, DefPieces, DefPiecesPos);

            }
            else
            {
                transform.position = new Vector2(initialPosition.x, initialPosition.y);
            }
        }
        else
        {

            if (Mathf.Floor(mousePosition.x) + 0.5f == initialPosition.x && Mathf.Floor(piecePlace.position.y) < initialPosition.y && Mathf.Floor(piecePlace.position.y) > Mathf.Floor(initialPosition.y) - (possibleSquares + 0.5))
            {
                PieceChanges(AttPiecesPos, DefPieces, DefPiecesPos);

            }
            else
            {
                transform.position = new Vector2(initialPosition.x, initialPosition.y);
            }
        }
        
        
    }
    void KingMovement(List<Vector3> AttPiecesPos, List<GameObject> DefPieces, List<Vector3> DefPiecesPos)
    {
        int possibleSquares = 1;
        
        if (Mathf.Abs(Mathf.Floor(piecePlace.position.x) - Mathf.Floor(initialPosition.x)) <= possibleSquares && Mathf.Abs(Mathf.Floor(piecePlace.position.y) - Mathf.Floor(initialPosition.y)) <= possibleSquares)
        {
            PieceChanges(AttPiecesPos, DefPieces, DefPiecesPos);

        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
        
    }
    void BishopMovement(List<Vector3> AttPiecesPos, List<GameObject> DefPieces, List<Vector3> DefPiecesPos)
    {

        if (Mathf.Abs(Mathf.Floor(piecePlace.position.x) - Mathf.Floor(initialPosition.x)) == Mathf.Abs(Mathf.Floor(piecePlace.position.y) - Mathf.Floor(initialPosition.y)))
        {

            PieceChanges(AttPiecesPos, DefPieces, DefPiecesPos);
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
        
    }
    void RookMovement(List<Vector3> AttPiecesPos, List<GameObject> DefPieces, List<Vector3> DefPiecesPos)
    {
        
        if (Mathf.Floor(piecePlace.position.x) == Mathf.Floor(initialPosition.x) && Mathf.Floor(piecePlace.position.y) != initialPosition.y || Mathf.Floor(piecePlace.position.x) != initialPosition.x && Mathf.Floor(piecePlace.position.y) == Mathf.Floor(initialPosition.y))
        {
            PieceChanges(AttPiecesPos, DefPieces, DefPiecesPos);

        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
       
    }


    void PieceChanges(List<Vector3> AttPiecesPos, List<GameObject> DefPieces, List<Vector3> DefPiecesPos)
    {
        if (!AttPiecesPos.Contains(new Vector3((Mathf.Floor(piecePlace.position.x) + 0.5f), (Mathf.Floor(piecePlace.position.y) + 0.5f), 0)))
        {
            transform.position = new Vector2(Mathf.Floor(piecePlace.position.x) + 0.5f, Mathf.Floor(piecePlace.position.y) + 0.5f);
            AttPiecesPos[AttPiecesPos.FindIndex(ind => ind.Equals(new Vector3(initialPosition.x, initialPosition.y, 0)))] = new Vector3((Mathf.Floor(piecePlace.position.x) + 0.5f), (Mathf.Floor(piecePlace.position.y) + 0.5f), 0);
            initialPosition.x = Mathf.Floor(piecePlace.position.x) + 0.5f;
            initialPosition.y = Mathf.Floor(piecePlace.position.y) + 0.5f;
            board_obj.GetComponent<Board>().IsBlackOnMove = !isBlackOnMove;
            if (DefPiecesPos.Contains(new Vector3((Mathf.Floor(piecePlace.position.x) + 0.5f), (Mathf.Floor(piecePlace.position.y) + 0.5f), 0)))
            {
                Destroy(DefPieces[DefPiecesPos.FindIndex(ind => ind.Equals(new Vector3((Mathf.Floor(piecePlace.position.x) + 0.5f), (Mathf.Floor(piecePlace.position.y) + 0.5f), 0)))]);
                DefPiecesPos.RemoveAt(DefPiecesPos.FindIndex(ind => ind.Equals(new Vector3((Mathf.Floor(piecePlace.position.x) + 0.5f), (Mathf.Floor(piecePlace.position.y) + 0.5f), 0))));

            }
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);

        }
    }
}
