using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectsParent {

   public Transform GetKitchenObjectFollowTransform();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public void clearKitchenObject();

    public bool hasKitchenObject();
    
}
