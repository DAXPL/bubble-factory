using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour {
    [SerializeField] private string factoryName;
    [SerializeField] private int factoryPrice;
    [SerializeField] private Sprite factoryTwoChimneysSprite;
    [SerializeField] private Sprite factoryThreeChimneysSprite;
    [SerializeField] private GameObject bubble;
    [SerializeField] private GameObject goldenBubble;
    private bool isBought = false;
    private bool hasChangedToThreeChimneys;
    private Image factorySprite;

    private float bubbleProgress = 0;
    private float tapMultiplier = 1;
    private float pasiveIncomeMultiplier = 0;
    private float bubbleMultiplier = 1;

    private float goldenBubbleChance = 0.01f;

    private float bubbleCost = 10;

    private float additionalSize = 0;
    private Vector3 startSize = Vector3.zero;

    [SerializeField] private AudioSource sourceOfAudio;
    [SerializeField] private AudioClip bubbleSound;
    [SerializeField] private AudioClip buySound;
    [SerializeField] private AudioClip cantBuySound;

    private void Awake() {
        factorySprite = GetComponent<Image>();
    }

    private void Start()
    {
        startSize = this.transform.localScale;
        hasChangedToThreeChimneys = false;
        Button button = GetComponent<Button>();
        if (button != null) button.interactable = isBought;
    }

    private void Update()
    {
        additionalSize -= Time.deltaTime/2;
        additionalSize = Mathf.Clamp(additionalSize, 0, 0.2f);
        this.transform.localScale = startSize + (Vector3.one* additionalSize);
    }

    public void OnClick()
    {
        additionalSize += 0.1f;
        bubbleProgress += 1 * tapMultiplier;
        CheckBubbleProgress();
    }

    public void PasiveIncome()
    {
        
        bubbleProgress += pasiveIncomeMultiplier;
        CheckBubbleProgress();
    }

    private void CheckBubbleProgress()
    {
        int bubbleCount = 0;
        while(bubbleProgress >= bubbleCost)
        {
            bubbleProgress -= bubbleCost;
            bool goldenBubbleRand = Random.Range(0, 1.0f) <= goldenBubbleChance;
            GameManager.Instance.IncreaseScore(goldenBubbleRand ? 10 : 1);
          
            if (isActiveAndEnabled && bubbleCount<5) 
            {
                GameObject preab = goldenBubbleRand ? goldenBubble : bubble;
                if(preab == null) return;
                GameObject buble = Instantiate(preab, this.transform, false);
                buble.transform.SetParent(this.transform.parent);
                Destroy(buble, 5);
                bubbleCount++;
            }
            
        }
    }

    public bool BuyFactory(bool force=false)
    {
        bool canAfford = GameManager.Instance != null &&
                    GameManager.Instance.GetScore() > factoryPrice;
        if (force || canAfford)
        {
            isBought = true;

            if(force == false && canAfford)
            {
                GameManager.Instance.IncreaseScore(factoryPrice);
            }
            Button button = GetComponent<Button>();
            if (button != null) button.interactable = isBought;
            return true;
        }
        return false;
    }

    public void TryBuyFactory()
    {
        bool result = BuyFactory();
        if (sourceOfAudio) sourceOfAudio.PlayOneShot(result? buySound : cantBuySound);
    }

    public string GetFactoryName() {
        return factoryName;
    }

    public void IncreaseTapMultiplier(float amount)
    {
        tapMultiplier += amount;
        AddChimney();
    }

    private void AddChimney() {
        if (hasChangedToThreeChimneys)
            return;

        if (tapMultiplier >= 2 && tapMultiplier < 3) {
            factorySprite.sprite = factoryTwoChimneysSprite;
            return;
        } 
        
        if (tapMultiplier >= 3) {
            factorySprite.sprite = factoryThreeChimneysSprite;
            hasChangedToThreeChimneys = true;
        }

    }

    public void MultiplyTapMultiplier(float amount)
    {
        tapMultiplier *= amount;
    }
    public void IncreasePasiveMultiplier(float amount)
    {
        pasiveIncomeMultiplier += amount;
    }
    public void MultiplyPasiveMultiplier(float amount)
    {
        pasiveIncomeMultiplier *= amount;
    }
    public void IncreaseBubbleMultiplier(float amount)
    {
        bubbleMultiplier += amount;
    }
    public void MultiplyBubbleMultiplier(float amount)
    {
        bubbleMultiplier *= amount;
    }
    public float GetTapMultiplier()
    {
        return tapMultiplier;
    }
    public float GetPasiveMultiplier()
    {
        return pasiveIncomeMultiplier;
    }
    public float GetBubbleMultiplier()
    {
        return bubbleMultiplier;
    }
}
