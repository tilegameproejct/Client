using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_Piece : MonoBehaviour
{
    public enum BoardState
    {
        None,
        White,
        Black
    }

    GameObject Disk;

    Vector2 i_pos;
    BoardState state = BoardState.None;
    public void SetPos(int x, int y)
    {
        i_pos = new Vector2(x, y);
    }

    public Vector2 GetPos() { return i_pos; }
    public BoardState GetState() { return state; }

    public void ChangeDisk(BoardState change_state, GameObject change_disk=null)
    {
        state = change_state;
        if (Disk == null)
        {
            Disk = Instantiate(change_disk, transform);
            Disk.transform.position = transform.position - new Vector3(0, 0, 0.6f);
        }
        if (state == BoardState.Black)
        {
            Disk.GetComponent<Disk_changer>().SetDiskColor(Disk_changer.DiskState.Black);
        }
        else if (state == BoardState.White)
        {
            Disk.GetComponent<Disk_changer>().SetDiskColor(Disk_changer.DiskState.White);
        }

    }
}
