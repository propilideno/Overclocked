using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private float rotationSpeed = 10f;

	Vector2 inputVector = new Vector2(0,0);

    // Update is called once per frame
    void Update(){
		if (Input.GetKey(KeyCode.W)) inputVector.y = 1;
		if (Input.GetKey(KeyCode.A)) inputVector.x = -1;
		if (Input.GetKey(KeyCode.S)) inputVector.y = -1;
		if (Input.GetKey(KeyCode.D)) inputVector.x = 1;
		if (! Input.GetKey(KeyCode.W) && ! Input.GetKey(KeyCode.S)) inputVector.y = 0;
		if (! Input.GetKey(KeyCode.A) && ! Input.GetKey(KeyCode.D)) inputVector.x = 0;

		// Normalize movements
		inputVector = inputVector.normalized;
		Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
		transform.position += moveDir * moveSpeed * Time.deltaTime;
		transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);

		Debug.Log("Player position: " + transform.position);
	}
}
