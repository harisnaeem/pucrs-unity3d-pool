﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject cueBall;
	public GameObject redBalls;
	public GameObject mainCamera;
	public float maxForce;
	public float minForce;

	private Vector3 strikeDirection;
	private Vector3 cameraOffset;

	// Use this for initialization
	void Start () {
		strikeDirection = Vector3.forward;
		cameraOffset = cueBall.transform.position - mainCamera.transform.position;
		cueBall.GetComponent<Rigidbody>().sleepThreshold = 10f;
		foreach (var rigidbody in redBalls.GetComponentsInChildren<Rigidbody>())
			rigidbody.sleepThreshold = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		var body = cueBall.GetComponent<Rigidbody>();

		if (Input.GetButton("Fire1") && body.IsSleeping()) {
			body.AddForce(strikeDirection * maxForce);
		} else if (body.IsSleeping()) {
			var x = Input.GetAxis("Horizontal");
			if (x != 0) {
				var angle = x * 75 * Time.deltaTime;
				strikeDirection = Quaternion.AngleAxis(angle, Vector3.up) * strikeDirection;
				mainCamera.transform.RotateAround(cueBall.transform.position, Vector3.up, angle);
			}
		}
	}

	void LateUpdate() {
		if (!cueBall.GetComponent<Rigidbody>().IsSleeping()) {
			mainCamera.transform.position = cueBall.transform.position - cameraOffset;
			mainCamera.transform.LookAt(cueBall.transform.position);
		}
	}
}