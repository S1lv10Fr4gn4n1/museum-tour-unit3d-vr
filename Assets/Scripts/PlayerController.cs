using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public void MovePlayerToPosition(Transform newTransform) {
		transform.position = newTransform.position;
	}
}
