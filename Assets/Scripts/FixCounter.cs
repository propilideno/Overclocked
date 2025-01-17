using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FixCounter : BaseCounter {
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs {
        public float progressNormalized;
    }
    [SerializeField] private FixingObjectSO[] fixingObjectSOArray;
    [SerializeField] ProgressBarUI progressBarUI;
    private int fixingProgress;
    public override void Interact(Player player){
        if (!hasKitchenObject()){
            // Não tem objeto na bancada
            if(player.hasKitchenObject()){
                // Se o player está segundo um objeto
                player.GetKitchenObject().setKitchenObjectParent(this);
                fixingProgress = 0;

                FixingObjectSO fixingObjectSO = getFixingObjectSOWithInput(GetKitchenObject().GetKitchenObjectsSO());


                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs{
                    progressNormalized = (float) fixingProgress/fixingObjectSO.fixingProgressMax
                    });
            } else {
                // Se o player não está segurando um objeto
            }
        } else {
            // Tem objeto na bancada
            if(player.hasKitchenObject()){
                // Se o player está segundo um objeto
            } else {
                GetKitchenObject().setKitchenObjectParent(player);
                progressBarUI.Hide();
            }
        }
    }

    // Função para incrementar o progresso
    public void IncrementFixProgress()
    {
        if (hasKitchenObject() && canBeFixed(GetKitchenObject().GetKitchenObjectsSO()))
        {
            fixingProgress++;
            FixingObjectSO fixingObjectSO = getFixingObjectSOWithInput(GetKitchenObject().GetKitchenObjectsSO());

            if (fixingProgress >= fixingObjectSO.fixingProgressMax)
            {   
                progressBarUI.Hide();
                KitchenObjectsSO outputKitchenObjectSO = getOutputForInput(GetKitchenObject().GetKitchenObjectsSO());
                GetKitchenObject().destroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }

            UpdateProgress();
        }
    }

    private void UpdateProgress()
    {
        FixingObjectSO fixingObjectSO = getFixingObjectSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
        if(fixingObjectSO != null){
            float progressNormalized = (float)fixingProgress / fixingObjectSO.fixingProgressMax;
        
        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
        {
            progressNormalized = progressNormalized
        });
        }
        
    }

    public override void InteractAlternate(Player player){
        if(hasKitchenObject()){
            if(canBeFixed(GetKitchenObject().GetKitchenObjectsSO())){   
                progressBarUI.Show();
            }
        }
    }

    /*public override void InteractAlternate(Player player){
        if (hasKitchenObject()){
            if(canBeFixed(GetKitchenObject().GetKitchenObjectsSO())){
                fixingProgress++;

                FixingObjectSO fixingObjectSO = getFixingObjectSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
                
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs{
                    progressNormalized = (float) fixingProgress/fixingObjectSO.fixingProgressMax
                    });

                if(fixingProgress >= fixingObjectSO.fixingProgressMax){
                    KitchenObjectsSO outputKitchenObjectSO = getOutputForInput(GetKitchenObject().GetKitchenObjectsSO());
                
                    GetKitchenObject().destroySelf();
                    KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
                }
            }
        }
    }*/

    private bool canBeFixed(KitchenObjectsSO inputKitchenObjectSO){
        foreach (FixingObjectSO fixingObjectSO in fixingObjectSOArray){
            if (fixingObjectSO.input == inputKitchenObjectSO){
                return true;
            }
        }
        return false;
    }

    private KitchenObjectsSO getOutputForInput(KitchenObjectsSO inputKitchenObjectSO){
        foreach(FixingObjectSO fixingObjectSO in fixingObjectSOArray){
            if(fixingObjectSO.input == inputKitchenObjectSO){
                return fixingObjectSO.output;
            }
        }
        return null;
    }

    private FixingObjectSO getFixingObjectSOWithInput(KitchenObjectsSO inputKitchenObjectSO){
        foreach(FixingObjectSO fixingObjectSO in fixingObjectSOArray){
            if(fixingObjectSO.input == inputKitchenObjectSO){
                return fixingObjectSO;
            }
        }
        return null;
    }
}
