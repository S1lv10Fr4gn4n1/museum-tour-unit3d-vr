using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {

	public GameObject player;

	public void GoToNextRoom(Transform transform) {
		player.transform.position = transform.position;
	}
}
