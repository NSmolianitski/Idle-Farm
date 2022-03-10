using TMPro;
using UnityEngine;

public class BackpackUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private HarvestStack harvestStack;

    private int _stackMaxSize;
    
    private void Awake()
    {
        _stackMaxSize = harvestStack.MaxSize;
        harvestStack.StackSizeChangedEvent += UpdateText;
    }

    private void UpdateText()
    {
        textMesh.text = $"{harvestStack.CurrentSize}/{_stackMaxSize}";
    }
}
