using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter {
    [SerializeField] private KitchenObjectsSO[] fixedObjectsSOArray; // Scriptable Object que contém os objetos da cozinha
    
    public override void Interact(Player player){
        if(player.hasKitchenObject()){
            if(canBeDelivered(player.GetKitchenObject().GetKitchenObjectsSO())){
            // Se o objeto que o player está segurando está na lista de objetos que podem ser entregues
            KitchenObject deliveredObject = player.GetKitchenObject();
            deliveredObject.setTimeToDestroy(1f);
            // Entrega o objeto para a bancada e ele é destruído
            deliveredObject.setKitchenObjectParent(this);
            player.AddDollars(deliveredObject.GetKitchenObjectsSO().value);
            Debug.Log("Dollars: " + player.GetDollars());
            }
        }
            
    }

    public bool canBeDelivered(KitchenObjectsSO kitchenObjectsSO){
        foreach(KitchenObjectsSO fixedObjectsSO in fixedObjectsSOArray){
            if(fixedObjectsSO == kitchenObjectsSO){
                return true;
            }
        }
        return false;
    }
}
