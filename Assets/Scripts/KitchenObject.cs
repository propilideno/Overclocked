using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO; // Scriptable Object que contém os objetos da cozinha             

    private IKitchenObjectsParent kitchenObjectParent; // Referência ao script ClearCounter
    
    public KitchenObjectsSO GetKitchenObjectsSO (){ // Método que retorna o Scriptable Object kitchenObjectsSO
        return kitchenObjectsSO;
    }

    public void setKitchenObjectParent(IKitchenObjectsParent kitchenObjectParent){ // Método que seta a referência ao script ClearCounter
        if(this.kitchenObjectParent != null){
            this.kitchenObjectParent.clearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;

        if(kitchenObjectParent.hasKitchenObject()){
            Debug.Log("Já existe um objeto na bancada");
        }

        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectsParent GetKitchenObjectParent(){ // Método que retorna a referência ao script ClearCounter
        return kitchenObjectParent;
    }

    public void destroySelf() { // Método que destroi o objeto da cozinha
        kitchenObjectParent.clearKitchenObject();
        Destroy(gameObject);
    }

    public void setTimeToDestroy(float timeToDestroy){ // Método que seta o tempo para destruir o objeto
        StartCoroutine(DestroyAfterTime(timeToDestroy));
    }

    public IEnumerator DestroyAfterTime(float timeToDestroy){ // Método que destroi o objeto após um tempo
        yield return new WaitForSeconds(timeToDestroy);
        destroySelf();
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectsSO kitchenObjectsSO, IKitchenObjectsParent kitchenObjectParent){
        Transform kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.setKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }
}
