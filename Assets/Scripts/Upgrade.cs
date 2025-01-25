using UnityEngine;


public class Upgrade : MonoBehaviour {

    private void Start() {
        UpdateUI();
    }

    public void UpdateUI() {
        // Update The item ui (this will be also called when changed the factory)
    }

    public void UpgradeTapMultiplier() {
        //IncreaseTapMultiplier(GetTapMultiplier()/10);
    }
    
    public void UpgradePasiveMultiplier() {
        //IncreasePasiveMultiplier(GetPasiveMultiplier()/10);
    }

    public void UpgradeBubbleMultiplier() {
        //IncreaseBubbleMultiplier(GetBubbleMultiplier()/10);
    }

}
