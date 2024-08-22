using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectsParent {

    [SerializeField] private Transform counterTopPoint; // Ponto onde os objetos da cozinha serão instanciados (em cima da bancada)
    private KitchenObject kitchenObject;


    public virtual void Interact(Player player){
        Debug.Log("Interagindo com a bancada");
    }

    public Transform GetKitchenObjectFollowTransform(){
        return counterTopPoint;
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
}
