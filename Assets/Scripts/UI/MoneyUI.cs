using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Transform valueTransform;
    
    [Header("Settings")]
    [Range(0.01f, 1)]
    [SerializeField] private float timeBetweenMoneyUpdates = 0.2f;
    [SerializeField] private float maxTextAnimationTime = 2f;
    [SerializeField] private float textShakeStrength = 15;

    private int _currentMoney = 0;
    private int _finalMoney = 0;
    private float _timeUntilEndOfAnimation;
    private Coroutine _moneyAnimation;
    private Tweener _textShakingTweener;

    public void CoinReceiveHandler(int coinValue)
    {
        _timeUntilEndOfAnimation = maxTextAnimationTime;
        
        _textShakingTweener?.Complete();
        _textShakingTweener = valueTransform.DOShakePosition(_timeUntilEndOfAnimation + 1, textShakeStrength);

        _finalMoney += coinValue;
        if (_moneyAnimation == null)
        {
            _moneyAnimation = StartCoroutine(AnimateMoney());
        }
    }

    private IEnumerator AnimateMoney()
    {
        while (_currentMoney < _finalMoney)
        {
            _timeUntilEndOfAnimation -= timeBetweenMoneyUpdates;
            var averageTimePerIter = _timeUntilEndOfAnimation / timeBetweenMoneyUpdates;
            if (_timeUntilEndOfAnimation <= timeBetweenMoneyUpdates)
                averageTimePerIter = 1;

            var moneyPerIter = (_finalMoney - _currentMoney) / averageTimePerIter;
            if (moneyPerIter < 1)
                moneyPerIter = 1;
            
            _currentMoney += (int) moneyPerIter;
            if (_currentMoney > _finalMoney)
                _currentMoney = _finalMoney;
            
            UpdateText();
            yield return new WaitForSeconds(timeBetweenMoneyUpdates);
        }

        _moneyAnimation = null;
    }
    
    private void UpdateText()
    {
        textMesh.text = _currentMoney.ToString();
    }
}