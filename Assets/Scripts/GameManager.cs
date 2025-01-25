using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    public static GameManager Instance;
    [SerializeField] private Factory[] factories;
    private int factoryPointer=0;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    
    private void Start()
    {
        if(factories.Length>0) factories[0].BuyFactory(true);
        StartCoroutine(PasiveIncomeThread());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void IncreaseScore(int amout=1)
    {
        score += amout;
    }
    
    public int GetScore() 
    {
        return score; 
    }

    private IEnumerator PasiveIncomeThread()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            foreach (var fact in factories) 
            {
                fact.PasiveIncome();
            }
        }
    }

    //by changing the sign you can rotate the list left or right
    public void SwitchFactories(int val = 1)
    {
        factoryPointer = (factoryPointer+val)%factories.Length;

    }
    public Factory GetCurrentFactory()
    {
        return factories[factoryPointer];
    }
}
