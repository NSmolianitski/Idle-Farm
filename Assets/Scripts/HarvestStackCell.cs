using UnityEngine;

public class HarvestStackCell
{
    public Vector3 PositionInBackpack { get; }
    public HarvestedBlock HarvestedBlock { get; }
    public bool IsInBackpack { get; set; }

    public HarvestStackCell(HarvestedBlock harvestedBlock, Vector3 position)
    {
        HarvestedBlock = harvestedBlock;
        PositionInBackpack = position;
    }

    public void UpdatePositionInBackpack(Vector3 backpackPosition, Quaternion rotation)
    {
        if (!IsInBackpack)
            return;
        
        HarvestedBlock.transform.position = backpackPosition + PositionInBackpack;
        HarvestedBlock.transform.rotation = rotation;
    }
}