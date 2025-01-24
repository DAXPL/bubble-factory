using System.Collections;
using UnityEngine;

public class Factory : MonoBehaviour
{
    private float bubbleProgress = 0;
    private float tapMultiplier = 1;
    private float pasiveIncomeMultiplier = 0;
    private float bubbleMultiplier = 1;

    private float goldenBubbleChance = 0.01f;

    private float bubbleCost = 10;
   
    public void OnClick()
    {
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
        while(bubbleProgress >= bubbleCost)
        {
            bubbleProgress -= bubbleCost;

            GameManager.Instance.IncreaseScore((Random.Range(0, 1.0f) <= goldenBubbleChance ? 10 : 1));
        }
    }

    public void IncreaseTapMultiplier(float amount)
    {
        tapMultiplier += amount;
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
