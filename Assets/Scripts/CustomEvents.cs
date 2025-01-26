using System;
using UnityEngine;

public class CustomEvents : MonoBehaviour {
    public static CustomEvents current;

    private void Awake() {
        current = this;
    }

    public event Action OnFactorySwitch;
    public void FactorySwitch() {
        OnFactorySwitch?.Invoke();
    }

}
