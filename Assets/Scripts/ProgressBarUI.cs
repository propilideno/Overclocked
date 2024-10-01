using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    [SerializeField] private FixCounter fixCounter;
    [SerializeField] private Image barImage;
    
    private void Start(){
        fixCounter.OnProgressChanged += FixCounter_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void FixCounter_OnProgressChanged(object sender, FixCounter.OnProgressChangedEventArgs e){
        barImage.fillAmount = e.progressNormalized;

        if(e.progressNormalized == 0f || e.progressNormalized == 1f){
            Hide();
        } else {
            Show();
        }
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    public void Hide(){
        gameObject.SetActive(false);
    }
}

