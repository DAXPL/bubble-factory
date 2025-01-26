using System;
using System.Collections;
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

        float currentMultiplier = currentFactory.GetPasiveMultiplier();
        //For the first 10 upgrades we reduce the time by 1 second. Then we cut by 10% of the current level
        currentFactory.IncreasePasiveMultiplier(currentMultiplier<=10?1f: currentMultiplier/10);
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


        StartCoroutine(TogglePopUp());

        UnityEngine.Debug.LogWarning("Not enough bubbles");
        return false;
    }

    private IEnumerator TogglePopUp() {
        GameManager.Instance.GetPopUp().SetActive(true);
        yield return new WaitForSeconds(1);
        GameManager.Instance.GetPopUp().SetActive(false);
    }
}
