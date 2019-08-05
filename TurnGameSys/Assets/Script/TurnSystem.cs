using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour {
	public GameObject SelectPrefab;
	GameObject RedTeam;
	GameObject BlueTeam;

	Transform[] RedTeamCharcter = new Transform[3];
	Transform[] BlueTeamCharcter = new Transform[3];

	// Use this for initialization
	void Start () {
		RedTeam = GameObject.Find("RedTeam");
		BlueTeam = GameObject.Find("BlueTeam");

		for(int i = 0; i < RedTeam.transform.childCount; i++){
			RedTeamCharcter[i] = RedTeam.transform.GetChild(i);
		}

		for(int i = 0; i < BlueTeam.transform.childCount; i++){
			BlueTeamCharcter [i] = BlueTeam.transform.GetChild(i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
