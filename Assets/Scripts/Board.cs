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


                if (isLightSquare)
                {
                    clone.GetComponent<SpriteRenderer>().material = whiteMat;
                }
                else
                {
                    clone.GetComponent<SpriteRenderer>().material = blackMat;
                }

                //setup pieces
                
                if (rank == 1)
                {
                    var piece = Instantiate(Pieces[11]);
                    piece.transform.localScale = new Vector2(0.25f, 0.25f);
                    piece.transform.position = new Vector2(-2.5f + file, -3.5f + rank);
                    piece.name = Pieces[11].name + "(" + clone.name + ")";
                }

                if (rank == 0)
                {

                    if (file < 5)
                    {
                        var piece = Instantiate(Pieces[6 + file]);
                        piece.transform.localScale = new Vector2(0.25f, 0.25f);
                        piece.transform.position = new Vector2(-2.5f + file, -3.5f + rank);
                        piece.name = Pieces[6 + file].name;
                        piece.transform.parent = clone.transform;
                    }
                    else
                    {

                        var piece = Instantiate(Pieces[8 - m]);
                        piece.transform.localScale = new Vector2(0.25f, 0.25f);
                        piece.transform.position = new Vector2(-2.5f + file, -3.5f + rank);
                        piece.name = Pieces[8 - m].name;
                        piece.transform.parent = clone.transform;

                        m++;
                    }
                }
                if (rank == 6)
                {
                    var piece = Instantiate(Pieces[5]);
                    piece.transform.localScale = new Vector2(0.25f, 0.25f);
                    piece.transform.position = new Vector2(-2.5f + file, -3.5f + rank);
                    piece.name = Pieces[5].name + "(" + clone.name + ")";
                }

                if (rank == 7)
                {

                    if (file < 5)
                    {
                        var piece = Instantiate(Pieces[0 + file]);
                        piece.transform.localScale = new Vector2(0.25f, 0.25f);
                        piece.transform.position = new Vector2(-2.5f + file, -3.5f + rank);
                        piece.name = Pieces[0 + file].name;
                        piece.transform.parent = clone.transform;
                    }
                    else
                    {

                        var piece = Instantiate(Pieces[2 - n]);
                        piece.transform.localScale = new Vector2(0.25f, 0.25f);
                        piece.transform.position = new Vector2(-2.5f + file, -3.5f + rank);
                        piece.name = Pieces[2 - n].name;
                        piece.transform.parent = clone.transform;

                        n++;
                    }
                }
            }
                    
                
            
        }
        
    }
    

    // Start is called before the first frame update
    void Start()
    {
        CreateBoard();
        //Board_obj.transform.localScale = new Vector2(2f, 1.15f);
        //Board_obj.transform.localPosition = new Vector2(-1, -0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
