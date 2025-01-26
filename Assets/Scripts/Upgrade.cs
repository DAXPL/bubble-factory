using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour {
    [Tooltip("Upgrade informations")]
    [SerializeField] private int price;
    [SerializeField] private string upgradeTitle;
    [SerializeField, TextArea] private string upgradeDescription;

    [Tooltip("Upgrade components")]
    [SerializeField] private TextMeshProUGUI buyButtonPriceText;
    [SerializeField] private TextMeshProUGUI upgradeTitleText;
    [SerializeField] private TextMeshProUGUI upgradeDescriptionText;


    private void Start() {
        UpdateUIPrice();
        UpdateUIInformation();
    }


    public void UpdateUIPrice() {
        buyButtonPriceText.SetText($"{price} Bubbles");
    }


    public void UpdateUIInformation() {
        upgradeTitleText.SetText(upgradeTitle);
        upgradeDescriptionText.SetText(upgradeDescription);
    }

    public void UpgradeTapMultiplier(Factory currentFactory) {
        if (GameManager.Instance == null)
            return;

        if (!CanBuy())
            return;

        currentFactory.IncreaseTapMultiplier(currentFactory.GetTapMultiplier()/10);
    }
    
    public void UpgradePasiveMultiplier(Factory currentFactory) {
        if (GameManager.Instance == null)
            return;

        if (!CanBuy())
            return;

        currentFactory.IncreasePasiveMultiplier(currentFactory.GetPasiveMultiplier()/10);
    }

    public void UpgradeBubbleMultiplier(Factory currentFactory) {
        if (GameManager.Instance == null)
            return;

        if (!CanBuy()) 
            return;

        currentFactory.IncreaseBubbleMultiplier(currentFactory.GetBubbleMultiplier()/10);
    }

    private bool CanBuy() {
        if (GameManager.Instance == null) {
            UnityEngine.Debug.LogError("No GameManager on the scene");
            return false;
        }

        if (GameManager.Instance.GetScore() >= price) {
            GameManager.Instance.IncreaseScore(-price);
            price *= 2;
            UpdateUIPrice();
            return true;
        }

        // Show Popup "not enugh bubbles
        UnityEngine.Debug.LogWarning("Not enough bubbles");
        return false;
    }

}
