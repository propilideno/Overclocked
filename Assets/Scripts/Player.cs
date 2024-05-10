using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{	
	// Properties
	public static Player Instance {get; private set;}

	public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
	public class OnSelectedCounterChangedEventArgs : EventArgs {
		public ClearCounter selectedCounterI;
	}
	private bool isWalking;
	private Vector3 lastInteractDir;
	private ClearCounter selectedCounter;

	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private float rotationSpeed = 10f;
	[SerializeField] private GameInput gameInput;

	// Nunca ter +1 player
	private void Awake(){
		if(Instance != null){
			Debug.LogError("Mais de uma instância de Player encontrada!");
		}
			Instance = this;
		
	}

	private void Start(){
		gameInput.onInteractAction += GameInput_OnInteractAction;
	}

	private void GameInput_OnInteractAction(object sender, System.EventArgs e){
		if(selectedCounter != null){
			selectedCounter.Interact();
		}
	}

	private void Update() {
		handleMovement();	
		handleInteractions();
	}

	private void handleInteractions(){
		Vector2 inputVector = gameInput.GetMovementVectorNormalized();

		Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

		float interactDistance = 2f;
		if(moveDir != Vector3.zero){
			lastInteractDir = moveDir;
		}

		// out significa que essa variavel armazena o retorno da função
		if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance)){
			// TryGetComponent já lida com valores null
			if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)){
				// Há um clearCounter na frente do player
				if (clearCounter != selectedCounter){
					SetSelectedCounter(clearCounter);
				}
			} else {
				SetSelectedCounter(null);
			}
		} else {
			SetSelectedCounter(null);
		}

	}

	private void handleMovement(){
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

	private void SetSelectedCounter(ClearCounter selectedCounter){
		this.selectedCounter = selectedCounter;
		OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{
			selectedCounterI = selectedCounter
		});
	}
}
