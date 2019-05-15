using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk_changer : MonoBehaviour
{
    public enum DiskState
    {
        White,
        Black
    }
    public DiskState state;

    public void SetDiskColor(DiskState change)
    {
        state = change;
        if(state==DiskState.White)
            transform.eulerAngles = Vector3.zero;
        else
            transform.eulerAngles = new Vector3(180,0,0);
    }
}
