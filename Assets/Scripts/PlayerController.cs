using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.VR;
using Gvr.Internal;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

	public float smoothing = 2.0f;
	public float distanceFromPainting = 2.0f;
	public Transform cameraTransform;

	void MovePlayerToPosition(Vector3 targetPosition) {
		StartCoroutine(MovePlayerToWayPoint(targetPosition));
	}

	IEnumerator MovePlayerToWayPoint(Vector3 targetPosition) {
		// TODO try to fix it later, distance float wasn't reaching 0.1f or less to finish the condition
		float distance = Vector3.Distance(transform.position, targetPosition);
		decimal distanceRound = Math.Round((decimal)distance, 2);

		while ((float)distanceRound > 0.1f) {
			distance = Vector3.Distance(transform.position, targetPosition);
			distanceRound = Math.Round((decimal)distance, 2);
			transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
			yield return null;
		}
	}

	void Update() {
		if (GvrViewer.Instance.Triggered) {
			TriggerHasBeenPressed();
		}
	}

	void TriggerHasBeenPressed() {
		RaycastHit hit;
		if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 1000f)) {
			EventTrigger eventTrigger = hit.collider.gameObject.GetComponent<EventTrigger>();
			if (eventTrigger) {
				Transform objTransform = hit.collider.gameObject.transform;

//				Vector3 pointTarget = transform.position + new Vector3(0, 0, -distanceFromPainting);

//				Ray ray = new Ray(transform.position, cameraTransform.position - transform.position);
//				Vector3 position = ray.GetPoint(0);

				// create a point in front of painting
				Vector3 pointFrontPainting = objTransform.rotation * Vector3.forward;
				// create a ray between painting origing and point in front of 
				Ray ray = new Ray(objTransform.position, pointFrontPainting);
				// get a point in front of painting origing with 2 unities of distance
				Vector3 position = ray.GetPoint(distanceFromPainting);

				// move player to that point (center and in front of the painting)
				MovePlayerToPosition(position);
			}
		}
	}


}
