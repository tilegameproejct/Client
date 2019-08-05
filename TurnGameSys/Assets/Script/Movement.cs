using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public GameObject Prefab;

	public GameObject RedCharacterPrefab;
	public GameObject BlueCharacterPrefab;

	GameObject Map;
	Transform InitObject;

	GameObject Selector;

	int TurnCnt = 0;

	// Use this for initialization
	void Start () {
		Map = GameObject.Find("Map");
		InitObject = Map.transform.GetChild(0);
		Prefab.transform.position = InitObject.position;

		Selector = Instantiate(Prefab,Prefab.transform.position, Prefab.transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow) == true)
		{	
			Selector.transform.Translate(-3, 0 , 0);
		}
		if(Input.GetKeyDown(KeyCode.RightArrow) == true)
		{
			Selector.transform.Translate(3, 0 , 0);
		}

		if(Input.GetKeyDown(KeyCode.UpArrow) == true)
		{
			Selector.transform.Translate(0, 0 , 3);
		}
		if(Input.GetKeyDown(KeyCode.DownArrow) == true)
		{
			Selector.transform.Translate(0, 0 , -3);
		}
		
		if(Input.GetKeyDown(KeyCode.LeftControl) == true)
		{	
			if(TurnCnt == 0)
			{
				Instantiate(RedCharacterPrefab, Selector.transform.position, RedCharacterPrefab.transform.rotation);
				TurnCnt = 1;
			}
			else
			{
				Instantiate(BlueCharacterPrefab, Selector.transform.position, BlueCharacterPrefab.transform.rotation);
				TurnCnt = 0;
			}
		}
	}
}
