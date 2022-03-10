using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HarvestStack : MonoBehaviour
{
    [SerializeField] private Transform backpack;
    [SerializeField] private HarvestStackSettings settings;

    private Vector3 _stackTopPos;
    private Stack<HarvestStackCell> _harvestStack;
    private float _sellCooldownTime;
    
    public int MaxSize => settings.StackMaxSize;
    public int CurrentSize => _harvestStack.Count;
    public Action StackSizeChangedEvent;

    private void Awake()
    {
        _stackTopPos = Vector3.zero;
        _harvestStack = new Stack<HarvestStackCell>(settings.StackMaxSize);
    }

    private void Update()
    {
        var backpackPosition = backpack.position;
        var rotation = backpack.rotation;
        foreach (var cell in _harvestStack)
        {
            cell.UpdatePositionInBackpack(backpackPosition, rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var harvestedBlock = other.GetComponent<HarvestedBlock>();
        if (harvestedBlock && _harvestStack.Count < settings.StackMaxSize)
        {
            other.enabled = false;
            harvestedBlock.GetComponent<Rigidbody>().useGravity = false;
            
            var blockTransform = harvestedBlock.transform;
            blockTransform.position = backpack.position + _stackTopPos;
            blockTransform.rotation = transform.rotation;

            var cell = new HarvestStackCell(harvestedBlock, _stackTopPos);
            _harvestStack.Push(cell);
            cell.IsInBackpack = true;

            _stackTopPos += Vector3.up * settings.StackIndent;
            
            StackSizeChangedEvent?.Invoke();
        }
    }

    public void Sell(SellZone sellZone)
    {
        _sellCooldownTime -= Time.deltaTime;
        if (_sellCooldownTime > 0 || _harvestStack.Count == 0)
            return;

        _sellCooldownTime = settings.SellCooldown;
        
        var cell = _harvestStack.Pop();
        _stackTopPos = cell.PositionInBackpack;
        
        cell.HarvestedBlock.transform.DOMove(sellZone.SellPoint.position, settings.SellSpeed)
            .OnComplete(() => ItemSellAction(sellZone, cell.HarvestedBlock));
        StackSizeChangedEvent?.Invoke();
    }

    private void ItemSellAction(SellZone sellZone, HarvestedBlock harvestedBlock)
    {
        Destroy(harvestedBlock.gameObject);
        sellZone.SpawnCoin(harvestedBlock.SellCost);
    }
}
