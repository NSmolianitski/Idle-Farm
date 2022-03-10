using UnityEngine;

[CreateAssetMenu(fileName = "New Plant Settings", menuName = "Plant Settings")]
public class PlantSettings : ScriptableObject
{
    [SerializeField] private float growTime = 10f;
    [SerializeField] private float slicesDisappearTime = 3f;
    [SerializeField] private int sellCost = 15;

    public float GrowTime => growTime;
    public float SlicesDisappearTime => slicesDisappearTime;
    public int SellCost => sellCost;
}