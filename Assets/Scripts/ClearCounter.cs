using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {   

    [SerializeField] private KitchenObjectsSO kitchenObjectsSO; // Scriptable Object que contém os objetos da cozinha



    public override void Interact(Player player){
        if (!hasKitchenObject()){
            // Não tem objeto na bancada
            if(player.hasKitchenObject()){
                // Se o player está segundo um objeto
                player.GetKitchenObject().setKitchenObjectParent(this);
            } else {
                // Se o player não está segurando um objeto
            }
        } else {
            // Tem objeto na bancada
            if(player.hasKitchenObject()){
                // Se o player está segundo um objeto
            } else {
                GetKitchenObject().setKitchenObjectParent(player);
            }
        }
    }

    
}
