using DG.Tweening;
using UnityEngine;

public class SellZone : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private MoneyUI moneyUI;
    [SerializeField] private Transform sellPoint;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform spawnedPointsParent;
    [SerializeField] private Transform moneyIconPos;
    
    [Header("Settings")]
    [SerializeField] private float coinAnimationTime = 1;
    [SerializeField] private float coinMaxScale = 1;

    public Transform SellPoint => sellPoint;

    private void OnTriggerStay(Collider other)
    {
        var harvestStack = other.GetComponent<HarvestStack>();
        if (!harvestStack || harvestStack.CurrentSize == 0)
            return;

        harvestStack.Sell(this);
    }

    public void SpawnCoin(int coinValue)
    {
        var screenPos = cam.WorldToScreenPoint(sellPoint.position);
        var coin = Instantiate(coinPrefab, screenPos, Quaternion.identity, spawnedPointsParent);
        coin.transform.DOMove(moneyIconPos.position, coinAnimationTime).OnComplete(() =>
        {
            moneyUI.CoinReceiveHandler(coinValue);
            Destroy(coin.gameObject);
        });
        coin.transform.DOScale(coinMaxScale, coinAnimationTime);
    }
}
