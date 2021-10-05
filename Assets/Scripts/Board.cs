using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject square;

    [SerializeField]
    private Material blackMat;

    [SerializeField]
    private Material whiteMat;

    public GameObject Board_obj;

    public List<GameObject> Pieces = new List<GameObject>();
    

    private string[] FILES = {"a", "b", "c", "d", "e", "f", "g", "h"};

    void CreateBoard()
    {
        
        int m = 0;
        int n = 0;
        for (int file = 0; file < 8; file++)
        {
            for (int rank = 0; rank < 8; rank++)
            {

                bool isLightSquare = (rank + file) % 2 != 0;
                var clone = Instantiate(square);

                clone.transform.position = new Vector2(-2.5f + file, -3.5f + rank);
                clone.name = FILES[file] + (rank + 1).ToString();

                clone.transform.parent = Board_obj.transform;
                clone.AddComponent<BoxCollider2D>();



                if (isLightSquare)
                {
                    clone.GetComponent<SpriteRenderer>().material = whiteMat;
                }
                else
                {
                    clone.GetComponent<SpriteRenderer>().material = blackMat;
                }

                //setup pieces
                //PREDELAT!!!
                
                if (rank == 1)
                {
                    CreatePiece(clone, file, rank, 11);
                    

                }

                if (rank == 0)
                {

                    if (file < 5)
                    {
                        CreatePiece(clone, file, rank, 6+file);
                        
                    }
                    else
                    {
                        CreatePiece(clone, file, rank, 8-m);
                        
                        m++;
                    }
                }
                if (rank == 6)
                {
                    CreatePiece(clone, file, rank, 5);
                   
                }

                if (rank == 7)
                {

                    if (file < 5)
                    {
                        CreatePiece(clone, file, rank, 0+file);
                        
                    }
                    else
                    {
                        CreatePiece(clone, file, rank, 2-n);
                        
                        n++;
                    }
                }
            }
                    
                
            
        }
        
    }
    void CreatePiece(GameObject clone, int file, int rank, int pieceNumber)
    {
        var piece = Instantiate(Pieces[pieceNumber]);
        piece.transform.localScale = new Vector2(0.25f, 0.25f);
        piece.transform.position = new Vector2(-2.5f + file, -3.5f + rank);
        piece.name = Pieces[pieceNumber].name + "(" + clone.name + ")";
        piece.transform.parent = clone.transform;
        piece.AddComponent<BoxCollider2D>();
        if(piece.name.Contains("Rook"))
        {
            piece.AddComponent<RookMove>().piecePlace = piece.transform;

        }
        if(piece.name.Contains("Pawn"))
        {
            piece.AddComponent<PawnMove>().piecePlace = piece.transform;
            piece.GetComponent<PawnMove>().Pawn = piece;

        }
        if (piece.name.Contains("Bishop"))
        {
            piece.AddComponent<BishopMove>().piecePlace = piece.transform;

        }
        if (piece.name.Contains("King"))
        {
            piece.AddComponent<KingMove>().piecePlace = piece.transform;

        }
        if (piece.name.Contains("Knight"))
        {
            piece.AddComponent<KnightMove>().piecePlace = piece.transform;

        }
        if (piece.name.Contains("Queen"))
        {
            piece.AddComponent<QueenMove>().piecePlace = piece.transform;

        }

    }

    

    // Start is called before the first frame update
    void Start()
    {
        CreateBoard();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
