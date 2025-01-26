using TMPro;
using UnityEngine;


[CreateAssetMenu(fileName = "ButtonManager", menuName = "ButtonManager/New Button Manager")]
public class ButtonManager : ScriptableObject {
    private int factoryIndex = 1;

    public void SwitchStoreFactory(TextMeshProUGUI factoryNameText) {
        GameManager.Instance.SwitchFactories();
        factoryNameText.SetText(GameManager.Instance.GetCurrentFactory().GetFactoryName());
    }
}
