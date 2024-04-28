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
		float moveDistance = moveSpeed * Time.deltaTime;
		float playerRadius = .7f;
		float playerHeight = 2f;
		bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
		if(!canMove){
			// Não pode se mover na direção de moveDir
			// Tenta se mover no eixo X
			Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
			canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
			if (canMove){
				// Só pode se mover no eixo X
				moveDir = moveDirX;
			} else {
				// Tenta se mover no eixo Z
				Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
				canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
				if (canMove){
					// Só pode se mover no eixo Z
					moveDir = moveDirZ;
				}
			}
		}
		if (canMove){
			transform.position += moveDir * moveDistance;
		}
		isWalking = moveDir != Vector3.zero;
		transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);

		//Debug.Log("Player position: " + transform.position);
	}

	public bool IsWalking(){
		return isWalking;
	}
}
