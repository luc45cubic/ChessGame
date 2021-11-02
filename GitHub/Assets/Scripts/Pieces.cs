using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chess
{
    public class Pieces : MonoBehaviour
    {
        #region variables
        public Transform piecePlace;

        public GameObject board_obj;
        public GameObject Piece;

        private Vector2 initialPosition;
        private Vector2 mousePosition;


        private float deltaX, deltaY;

        private int possibleSquares = 2;

        private bool onMove;

        private List<Vector2> AttPiecesPos = new List<Vector2>();
        private List<Vector2> DefPiecesPos = new List<Vector2>();

        private List<GameObject> AttPieces = new List<GameObject>();
        private List<GameObject> DefPieces = new List<GameObject>();

        private readonly int[] Pos = { -2, 2 };
        private readonly int[] PosTwo = { -1, 1, };

        Board add_component_from_board;

        #endregion
        void Start()
        {

            initialPosition = transform.position;
            add_component_from_board = board_obj.GetComponent<Board>();

        }



        private void Update()
        {
            onMove = Piece.name.Contains("White") ? !add_component_from_board.IsBlackOnMove
                && Piece.name.Contains("White") : add_component_from_board.IsBlackOnMove && Piece.name.Contains("Black"); //zjisti kdo je na tahu

            if (Input.GetMouseButtonDown(1))
            {
                PieceSelectionChanged();
            }

        }


        private void OnMouseDown()
        {
            PieceSelectionChanged();

            if (onMove)
            {

                deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
                deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
                
                PseudoLegalMoves();

            }


        }
        private void OnMouseDrag()
        {
            
            if (onMove)
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
            }

        }

        private void OnMouseUp()
        {

            if (!(Mathf.Floor(mousePosition.x) < -3.5 || Mathf.Floor(mousePosition.y) < -4 || Mathf.Floor(mousePosition.x) > 4.5 || Mathf.Floor(mousePosition.y) > 3.5))
            {
                if (onMove)
                {
                    if (PieceSelection(piecePlace.position))
                        PieceChanges(AttPiecesPos, AttPieces, DefPieces, DefPiecesPos);

                    else
                        transform.position = new Vector2(initialPosition.x, initialPosition.y);

                }
                
            }

            else
            {
                transform.position = new Vector2(initialPosition.x, initialPosition.y);
            }

        }

        private void PseudoLegalMoves()
        {

            foreach (Vector2 square in add_component_from_board.Squares_pos)
            {

                if (PieceSelection(square))
                    ShowPossibleMoves(square);

            }
            
        }

        private bool PieceSelection(Vector2 location) //zjisti jaka figura je vybrana a vrati jeji mozne tahy
        {
            AttPiecesPos = Piece.name.Contains("White") ? add_component_from_board.WhitePos : add_component_from_board.BlackPos;
            AttPieces = Piece.name.Contains("White") ? add_component_from_board.WhitePieces: add_component_from_board.BlackPieces;
            DefPiecesPos = Piece.name.Contains("White") ? add_component_from_board.BlackPos : add_component_from_board.WhitePos;
            DefPieces = Piece.name.Contains("Black") ? add_component_from_board.WhitePieces : add_component_from_board.BlackPieces;

            

            if (Piece.name.Contains("Knight"))
            {
                return Pos.Contains((int)(Mathf.Floor(location.x) - Mathf.Floor(initialPosition.x))) && PosTwo.Contains((int)(Mathf.Floor(location.y) - Mathf.Floor(initialPosition.y))) || PosTwo.Contains((int)(Mathf.Floor(location.x) - Mathf.Floor(initialPosition.x))) && Pos.Contains((int)(Mathf.Floor(location.y) - Mathf.Floor(initialPosition.y)));

            }
            if (Piece.name.Contains("King"))
            {
                int possibleSquares = 1;

                return Mathf.Abs(Mathf.Floor(location.x) - Mathf.Floor(initialPosition.x)) <= possibleSquares && Mathf.Abs(Mathf.Floor(location.y) - Mathf.Floor(initialPosition.y)) <= possibleSquares;
                
            }
            if (Piece.name.Contains("Queen"))
            {
                
                return Mathf.Floor(location.x) == Mathf.Floor(initialPosition.x) || Mathf.Floor(location.y) == Mathf.Floor(initialPosition.y) || Mathf.Abs(Mathf.Floor(location.x) - Mathf.Floor(initialPosition.x)) == Mathf.Abs(Mathf.Floor(location.y) - Mathf.Floor(initialPosition.y));
                

            }
            if (Piece.name.Contains("White Pawn"))
            {
                return Mathf.Floor(location.x) + 0.5f == initialPosition.x && Mathf.Floor(location.y) > initialPosition.y && Mathf.Floor(location.y) < initialPosition.y + possibleSquares;
                

            }
            if (Piece.name.Contains("Black Pawn"))
            {
                return Mathf.Floor(location.x) + 0.5f == initialPosition.x && Mathf.Floor(location.y) < initialPosition.y && Mathf.Floor(location.y) > Mathf.Floor(initialPosition.y) - (possibleSquares + 0.5);
                

            }
            if (Piece.name.Contains("Bishop"))
            {
                return Mathf.Abs(Mathf.Floor(location.x) - Mathf.Floor(initialPosition.x)) == Mathf.Abs(Mathf.Floor(location.y) - Mathf.Floor(initialPosition.y));
                

            }
            if (Piece.name.Contains("Rook"))
            {
                return Mathf.Floor(location.x) == Mathf.Floor(initialPosition.x) && Mathf.Floor(location.y) != initialPosition.y || Mathf.Floor(location.x) != initialPosition.x && Mathf.Floor(location.y) == Mathf.Floor(initialPosition.y);
                

            }


            return false;

        }

        private void PieceChanges(List<Vector2> AttPiecesPos, List<GameObject> AttPieces, List<GameObject> DefPieces, List<Vector2> DefPiecesPos)
        {
            if (!AttPiecesPos.Contains(new Vector2(Mathf.Floor(piecePlace.position.x) + 0.5f, (Mathf.Floor(piecePlace.position.y) + 0.5f))))
            {
                PieceSelectionChanged();

                HighlightLastMove(initialPosition);
                HighlightLastMove(piecePlace.position);


                transform.position = new Vector2(Mathf.Floor(piecePlace.position.x) + 0.5f, Mathf.Floor(piecePlace.position.y) + 0.5f);
                
                AttPiecesPos[AttPiecesPos.FindIndex(ind => ind.Equals(new Vector2(initialPosition.x, initialPosition.y)))] = new Vector2((Mathf.Floor(piecePlace.position.x) + 0.5f), (Mathf.Floor(piecePlace.position.y) + 0.5f));
                
                AttPieces[AttPiecesPos.FindIndex(ind => ind.Equals(new Vector2(piecePlace.position.x, piecePlace.position.y)))].transform.parent = add_component_from_board.Squares[add_component_from_board.Squares_pos.FindIndex(ind => ind.Equals(new Vector2(Mathf.Floor(piecePlace.position.x) + 0.5f, Mathf.Floor(piecePlace.position.y) + 0.5f)))].transform;

                
                initialPosition = new Vector2(Mathf.Floor(piecePlace.position.x) + 0.5f, Mathf.Floor(piecePlace.position.y) + 0.5f);

                add_component_from_board.IsBlackOnMove = !add_component_from_board.IsBlackOnMove;


                PieceDestroy(DefPieces, DefPiecesPos);

                

                if (Piece.name.Contains("Pawn"))
                    Piece.GetComponent<Pieces>().possibleSquares = 1;
                
            }
            else
            {
                transform.position = new Vector2(initialPosition.x, initialPosition.y);

            }
        }

        private void HighlightLastMove(Vector2 squaresXY)
        {
            add_component_from_board.Squares[add_component_from_board.Squares_pos.FindIndex(ind => ind.Equals(new Vector2((Mathf.Floor(squaresXY.x) + 0.5f), (Mathf.Floor(squaresXY.y) + 0.5f))))].GetComponent<SpriteRenderer>().material = add_component_from_board.Yellow;

        }

        private void PieceDestroy(List<GameObject> DefPieces, List<Vector2> DefPiecesPos) //vyhodi figuru soupere
        {
            if (DefPiecesPos.Contains(new Vector2(Mathf.Floor(piecePlace.position.x) + 0.5f, Mathf.Floor(piecePlace.position.y) + 0.5f)))
            {

                Destroy(DefPieces[DefPiecesPos.FindIndex(ind => ind.Equals(new Vector2((Mathf.Floor(piecePlace.position.x) + 0.5f), (Mathf.Floor(piecePlace.position.y) + 0.5f))))]);
                DefPieces.RemoveAt(DefPiecesPos.FindIndex(ind => ind.Equals(new Vector2((Mathf.Floor(piecePlace.position.x) + 0.5f), (Mathf.Floor(piecePlace.position.y) + 0.5f)))));
                DefPiecesPos.RemoveAt(DefPiecesPos.FindIndex(ind => ind.Equals(new Vector2((Mathf.Floor(piecePlace.position.x) + 0.5f), (Mathf.Floor(piecePlace.position.y) + 0.5f)))));

            }
        }

        private void ShowPossibleMoves(Vector2 squareXY) //oznaci mozne tahy cervene
        {
            if (!AttPiecesPos.Contains(squareXY))
            {
                add_component_from_board.Squares[add_component_from_board.Squares_pos.FindIndex(ind => ind.Equals(new Vector2((Mathf.Floor(squareXY.x) + 0.5f), Mathf.Floor(squareXY.y) + 0.5f)))].GetComponent<SpriteRenderer>().material = add_component_from_board.lightRed;
            }
            
        }

        private void PieceSelectionChanged() //vymaze vsechny oznacene tahy
        {
            int elm = 0;
            foreach (Material square_mat in add_component_from_board.Squares_mat)
            {
                add_component_from_board.Squares[add_component_from_board.Squares_pos.FindIndex(ind => ind.Equals(new Vector2((Mathf.Floor(add_component_from_board.Squares_pos[elm].x) + 0.5f), (Mathf.Floor(add_component_from_board.Squares_pos[elm].y) + 0.5f))))].GetComponent<SpriteRenderer>().material = square_mat;
                elm++;
            }
        }

    }
}