using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPos : MonoBehaviour {
    // 좌클릭시 선택될 Object
    Transform SelectObject;
    Transform TilePosition;

    private bool Moving;

    // Use this for initialization
    void Start () {
        Moving = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Moving && SelectObject != null)
        {
            MoveToTile(TilePosition);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                SelectObject = hitInfo.transform;

            }
        }
        else if (Input.GetMouseButtonDown(1)) {

            if (Physics.Raycast(ray, out hitInfo))
            {
                TilePosition = hitInfo.transform;
                if (SelectObject != null) Moving = true;

                Debug.Log(hitInfo.transform.gameObject.name);
            }
        }

        //Debug.Log(SelectObject.gameObject.name);
    }

    void MoveToTile(Transform tilePos)
    {
        if (SelectObject.position == tilePos.position) {
            Moving = false;
            SelectObject = null;
        }
        else
        {
            Vector3 dir = tilePos.position - SelectObject.position;
            dir.y = 0f;
            dir = Vector3.Normalize(dir);

            SelectObject.forward = dir;
            SelectObject.position += 2f * dir * Time.deltaTime;
        }
    }
}
