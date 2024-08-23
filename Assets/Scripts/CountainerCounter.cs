using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountainerCounter : BaseCounter {
    
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectsSO kitchenObjectsSO; // Scriptable Object que contém os objetos da cozinha

    public override void Interact(Player player){
        if(!player.hasKitchenObject()){
            KitchenObject.SpawnKitchenObject(kitchenObjectsSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
            
    }

    

}
