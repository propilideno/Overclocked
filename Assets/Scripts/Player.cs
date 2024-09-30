using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectsParent {	
	// Properties
	public static Player Instance {get; private set;}

	public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
	public class OnSelectedCounterChangedEventArgs : EventArgs {
		public BaseCounter selectedCounterI;
	}
	private bool isWalking;
	private Vector3 lastInteractDir;
	private BaseCounter selectedCounter;
	private KitchenObject kitchenObject;
	public int dollarsEarned = 0;
	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private float rotationSpeed = 10f;
	[SerializeField] private GameInput gameInput;
	[SerializeField] private Transform kitchenObjectHoldPoint;

	// Nunca ter +1 player
	private void Awake(){
		if(Instance != null){
			Debug.LogError("Mais de uma instância de Player encontrada!");
		}
			Instance = this;
		
	}

	private void Start(){
		gameInput.onInteractAction += GameInput_OnInteractAction;
		gameInput.onInteractAlternateAction += GameInput_OnInteractAlternateAction;
	}

	private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e){
		if(selectedCounter != null){
			selectedCounter.InteractAlternate(this);
		}
	}

	private void GameInput_OnInteractAction(object sender, System.EventArgs e){
		if(selectedCounter != null){
			selectedCounter.Interact(this);
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
			if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)){
				// Há um baseCounter na frente do player
				if (baseCounter != selectedCounter){
					SetSelectedCounter(baseCounter);
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
			canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
			if (canMove){
				// Só pode se mover no eixo X
				moveDir = moveDirX;
			} else {
				// Tenta se mover no eixo Z
				Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
				canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
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

	private void SetSelectedCounter(BaseCounter selectedCounter){
		this.selectedCounter = selectedCounter;
		OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{
			selectedCounterI = selectedCounter
		});
	}

	public Transform GetKitchenObjectFollowTransform(){
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }

    public void clearKitchenObject(){
        kitchenObject = null;
    }

    public bool hasKitchenObject(){
        return kitchenObject != null;
    }

	public void AddDollars(int dollars){
		dollarsEarned += dollars;
	}

	public int GetDollars(){
		return dollarsEarned;
	}
}
