using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    public class Board : MonoBehaviour
    {
        [SerializeField]
        private GameObject square;

        [SerializeField]
        private Material blackMat;

        [SerializeField]
        private Material whiteMat;

        public Material lightRed;
        public Material Yellow;

        public GameObject Board_obj;

        public List<GameObject> Pieces = new List<GameObject>();

        public List<GameObject> WhitePieces = new List<GameObject>();
        public List<GameObject> BlackPieces = new List<GameObject>();
        public List<Vector2> WhitePos = new List<Vector2>();
        public List<Vector2> BlackPos = new List<Vector2>();

        public bool IsBlackOnMove;

        public List<Vector2> Squares_pos = new List<Vector2>();
        public List<GameObject> Squares = new List<GameObject>();
        public List<Material> Squares_mat = new List<Material>();

        private readonly string[] FILES = { "a", "b", "c", "d", "e", "f", "g", "h" };

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



                    if (isLightSquare)
                    {
                        clone.GetComponent<SpriteRenderer>().material = whiteMat;
                    }
                    else
                    {
                        clone.GetComponent<SpriteRenderer>().material = blackMat;
                    }

                    AddToList(clone);

                    if (rank == 1)
                    {
                        CreatePiece(clone, file, rank, 11);


                    }

                    if (rank == 0)
                    {

                        if (file < 5)
                        {
                            CreatePiece(clone, file, rank, 6 + file);

                        }
                        else
                        {
                            CreatePiece(clone, file, rank, 8 - m);

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
                            CreatePiece(clone, file, rank, 0 + file);

                        }
                        else
                        {
                            CreatePiece(clone, file, rank, 2 - n);

                            n++;
                        }
                    }
                }



            }

        }

        private void AddToList(GameObject clone)
        {
            Squares.Add(clone);
            Squares_pos.Add(clone.transform.position);
            Squares_mat.Add(clone.GetComponent<SpriteRenderer>().material);
        }

        void CreatePiece(GameObject clone, int file, int rank, int pieceNumber)
        {
            var piece = Instantiate(Pieces[pieceNumber]);
            piece.transform.localScale = new Vector2(0.3f, 0.3f);
            piece.transform.position = new Vector2(-2.5f + file, -3.5f + rank);
            piece.name = Pieces[pieceNumber].name;
            piece.transform.parent = clone.transform;

            piece.AddComponent<Pieces>().piecePlace = piece.transform;
            piece.GetComponent<SpriteRenderer>().sortingLayerName = "Pieces";
            piece.GetComponent<Pieces>().Piece = piece;
            piece.GetComponent<Pieces>().board_obj = Board_obj;

            if (piece.name.Contains("White"))
            {
                piece.AddComponent<BoxCollider2D>();

                WhitePos.Add(piece.transform.position);
                WhitePieces.Add(piece);

            }
            else
            {
                piece.AddComponent<BoxCollider2D>();

                BlackPos.Add(piece.transform.position);
                BlackPieces.Add(piece);

            }



        }



        void Start()
        {
            CreateBoard();


        }

        void Update()
        {

            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        }

       public void random_()
        {
            Debug.Log("ahoj");
        } 
    }
}
