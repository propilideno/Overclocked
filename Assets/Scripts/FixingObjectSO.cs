using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FixingObjectSO : ScriptableObject {
    public KitchenObjectsSO input;
    public KitchenObjectsSO output;
    public int fixingProgressMax;
}
