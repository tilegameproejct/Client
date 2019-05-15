using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    enum PlayerTurn
    {
        Black,
        White
    }

    public GameObject p_Board_Piece;
    public GameObject p_Disk;
    public GameObject CrossHair;

    GameObject[,] Board;
    PlayerTurn turn;
    List<GameObject> lst_cross;
    // Start is called before the first frame update
    void Start()
    {
        lst_cross = new List<GameObject>();
        turn = PlayerTurn.Black;
        Board = new GameObject[8, 8];
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                Board[i, j] = Instantiate(p_Board_Piece, transform);
                Board[i, j].transform.localPosition = new Vector3(-3.5f + i, 3.5f - j, 0);
            }
        }

        Board[3, 3].GetComponent<Board_Piece>().ChangeDisk(global::Board_Piece.BoardState.White, p_Disk);
        Board[4, 4].GetComponent<Board_Piece>().ChangeDisk(global::Board_Piece.BoardState.White, p_Disk);
        Board[4, 3].GetComponent<Board_Piece>().ChangeDisk(global::Board_Piece.BoardState.Black, p_Disk);
        Board[3, 4].GetComponent<Board_Piece>().ChangeDisk(global::Board_Piece.BoardState.Black, p_Disk);

        SetCrossHair();

    }

    bool SetCrossHair()
    {
        for (int i = 0; i < lst_cross.Count; ++i)
        {
            Destroy(lst_cross[i]);
        }
        lst_cross.Clear();
        Board_Piece.BoardState check_board;
        Board_Piece.BoardState change_board;
        if (turn == PlayerTurn.Black)
        {
            check_board = Board_Piece.BoardState.White;
            change_board = Board_Piece.BoardState.Black;
        }
        else
        {
            check_board = Board_Piece.BoardState.Black;
            change_board = Board_Piece.BoardState.White;
        }
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                if (Board[i, j].GetComponent<Board_Piece>().GetState() == Board_Piece.BoardState.None)
                {
                    if (CheckDir(check_board, change_board, new Vector2(i, j), 0, -1) ||//상
                     CheckDir(check_board, change_board, new Vector2(i, j), 0, 1) ||//하
                     CheckDir(check_board, change_board, new Vector2(i, j), -1, 0) ||//좌
                     CheckDir(check_board, change_board, new Vector2(i, j), 1, 0) ||//우
                     CheckDir(check_board, change_board, new Vector2(i, j), -1, -1) ||//상좌
                     CheckDir(check_board, change_board, new Vector2(i, j), 1, -1) ||//상우
                     CheckDir(check_board, change_board, new Vector2(i, j), -1, 1) ||//하좌
                     CheckDir(check_board, change_board, new Vector2(i, j), 1, 1))//하우
                    {
                        GameObject obj_cross = Instantiate(CrossHair, Board[i, j].transform);
                        obj_cross.transform.localPosition = new Vector3(0, 0, -0.5f);
                        lst_cross.Add(obj_cross);
                    }
                }
            }
        }
        if (lst_cross.Count == 0)
            return false;//
        else return true;
    }

    bool CheckDir(Board_Piece.BoardState check_board, Board_Piece.BoardState change_board, Vector2 pos, int dirx, int diry)
    {
        Queue<Vector2> q = new Queue<Vector2>();
        q.Clear();
        int i = (int)pos.x + dirx;
        int j = (int)pos.y + diry;

        while (true)
        {
            if (i < 0 || i > 7 || j < 0 || j > 7)
            {
                return false;
            }
            if (Board[i, j].GetComponent<Board_Piece>().GetState() == Board_Piece.BoardState.None)
            {
                return false;
            }
            else if (Board[i, j].GetComponent<Board_Piece>().GetState() == check_board)
            {
                q.Enqueue(new Vector2(i, j));
            }
            else
            {
                if (q.Count != 0)
                    return true;
                else
                    return false;
            }
            i += dirx;
            j += diry;
        }
    }




    public void ClickBoard(GameObject target)
    {
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                if (target.Equals(Board[i, j]) && target.GetComponent<Board_Piece>().GetState() == Board_Piece.BoardState.None)
                {
                    ChangeDisksOnBoard(turn, new Vector2(i, j));
                    if (turn == PlayerTurn.Black)
                    {
                        Board[i, j].GetComponent<Board_Piece>().ChangeDisk(global::Board_Piece.BoardState.Black, p_Disk);
                        turn = PlayerTurn.White;
                    }
                    else
                    {
                        Board[i, j].GetComponent<Board_Piece>().ChangeDisk(global::Board_Piece.BoardState.White, p_Disk);
                        turn = PlayerTurn.Black;
                    }
                    break;
                }
            }
        }
        if (!SetCrossHair())
        {
            if(turn== PlayerTurn.Black) turn = PlayerTurn.White;
            else turn = PlayerTurn.Black;
            SetCrossHair();
        }

    }

    void ChangeDisksOnBoard(PlayerTurn checkturn, Vector2 pos)
    {
        Board_Piece.BoardState check_board;
        Board_Piece.BoardState change_board;
        if (checkturn == PlayerTurn.Black)
        {
            check_board = Board_Piece.BoardState.White;
            change_board = Board_Piece.BoardState.Black;
        }
        else
        {
            check_board = Board_Piece.BoardState.Black;
            change_board = Board_Piece.BoardState.White;
        }
        CheckChangeDir(check_board, change_board, pos, 0, -1);//상
        CheckChangeDir(check_board, change_board, pos, 0, 1);//하
        CheckChangeDir(check_board, change_board, pos, -1, 0);//좌
        CheckChangeDir(check_board, change_board, pos, 1, 0);//우
        CheckChangeDir(check_board, change_board, pos, -1, -1);//상좌
        CheckChangeDir(check_board, change_board, pos, 1, -1);//상우
        CheckChangeDir(check_board, change_board, pos, -1, 1);//하좌
        CheckChangeDir(check_board, change_board, pos, 1, 1);//하우

    }

    void CheckChangeDir(Board_Piece.BoardState check_board, Board_Piece.BoardState change_board, Vector2 pos, int dirx, int diry)
    {
        Queue<Vector2> q = new Queue<Vector2>();
        q.Clear();
        int i = (int)pos.x + dirx;
        int j = (int)pos.y + diry;

        while (true)
        {
            if (i < 0 || i > 7 || j < 0 || j > 7)
            {
                return;
            }
            if (Board[i, j].GetComponent<Board_Piece>().GetState() == Board_Piece.BoardState.None)
            {
                break;
            }
            else if (Board[i, j].GetComponent<Board_Piece>().GetState() == check_board)
            {
                q.Enqueue(new Vector2(i, j));
            }
            else
            {
                while (q.Count != 0)
                {
                    Vector2 v = q.Dequeue();
                    Board[(int)v.x, (int)v.y].GetComponent<Board_Piece>().ChangeDisk(change_board);
                }
                break;
            }
            i += dirx;
            j += diry;
        }
    }
}
