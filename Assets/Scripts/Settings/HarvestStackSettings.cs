using UnityEngine;

[CreateAssetMenu(fileName = "New HarvestStack Settings", menuName = "HarvestStack Settings")]
public class HarvestStackSettings : ScriptableObject
{
    [SerializeField] private float stackIndent = 0.5f;
    [SerializeField] private int stackMaxSize = 40;
    [SerializeField] private float sellSpeed = 0.2f;
    [SerializeField] private float sellCooldown = 0.1f;

    public float StackIndent => stackIndent;
    public int StackMaxSize => stackMaxSize;
    public float SellSpeed => sellSpeed;
    public float SellCooldown => sellCooldown;
}