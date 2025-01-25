using TMPro;
using UnityEngine;
using UnityEngine.UI;


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
        buyButtonPriceText.SetText(price.ToString());
        upgradeTitleText.SetText(upgradeTitle);
        upgradeDescriptionText.SetText(upgradeDescription);
        // Update The item ui (this will be also called when changed the factory)
    }
    
    public void UpdateUIInformation() {
        upgradeTitleText.SetText(upgradeTitle);
        upgradeDescriptionText.SetText(upgradeDescription);
    }

    public void UpgradeTapMultiplier() {
        if (GameManager.Instance == null)
            return;

        if (!CanBuy())
            return;

        Factory currentFactory = GameManager.Instance.GetCurrentFactory();
        currentFactory.IncreaseTapMultiplier(currentFactory.GetTapMultiplier()/10);
    }
    
    public void UpgradePasiveMultiplier() {
        if (GameManager.Instance == null)
            return;

        if (!CanBuy())
            return;

        Factory currentFactory = GameManager.Instance.GetCurrentFactory();
        currentFactory.IncreasePasiveMultiplier(currentFactory.GetPasiveMultiplier()/10);
    }

    public void UpgradeBubbleMultiplier() {
        if (GameManager.Instance == null)
            return;

        if (!CanBuy()) 
            return;

        Factory currentFactory = GameManager.Instance.GetCurrentFactory();
        currentFactory.IncreaseBubbleMultiplier(currentFactory.GetBubbleMultiplier()/10);
    }

    private bool CanBuy() {
        if (GameManager.Instance == null) {
            Debug.LogError("No GameManager on the scene");
            return false;
        }

        if (GameManager.Instance.GetScore() >= price) {
            GameManager.Instance.IncreaseScore(-price);
            price *= 2;
            UpdateUIPrice();
            return true;
        }

        // Show Popup "not enugh bubbles
        Debug.LogWarning("Not enough bubbles");
        return false;
    }

}
