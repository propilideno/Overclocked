using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{	
	private bool isWalking;
	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private float rotationSpeed = 10f;
	[SerializeField] private GameInput gameInput;

	private void Update() {
		Vector2 inputVector = gameInput.GetMovementVectorNormalized();
		Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
		float playerSize = .7f;
		bool canMove = !Physics.Raycast(transform.position, moveDir, playerSize);
		if (canMove){
			transform.position += moveDir * moveSpeed * Time.deltaTime;
		}
		isWalking = moveDir != Vector3.zero;
		transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);

		//Debug.Log("Player position: " + transform.position);
	}

	public bool IsWalking(){
		return isWalking;
	}
}
