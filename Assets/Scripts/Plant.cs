using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject harvestedBlockPrefab;
    [SerializeField] private GameObject grownModel;
    [SerializeField] private GameObject cutModel;
    [SerializeField] private GameObject slicedModel;
    [SerializeField] private PlantSettings settings;
    
    private float _timeUntilGrown;
    private Collider _collider;
    
    private void Awake()
    {
        grownModel.SetActive(true);
        cutModel.SetActive(false);

        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (_timeUntilGrown > 0)
            Grow();
    }

    private void Grow()
    {
        _timeUntilGrown -= Time.deltaTime;
        if (_timeUntilGrown <= 0)
        {
            grownModel.SetActive(true);
            cutModel.SetActive(false);
            _collider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Cut();
    }
    
    private void Cut()
    {
        grownModel.SetActive(false);
        cutModel.SetActive(true);
        _collider.enabled = false;
        _timeUntilGrown = settings.GrowTime;

        var slicedPlant = Instantiate(slicedModel, transform.position, Quaternion.identity);
        var rigidbodies = slicedPlant.GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rigidbodies)
        {
            rb.AddExplosionForce(10f, transform.position * Random.Range(1, 5), 10);
        }
        Destroy(slicedPlant, settings.SlicesDisappearTime);
        
        var harvestedBlock = Instantiate(harvestedBlockPrefab, 
            transform.position + (Vector3.up * 2), Quaternion.identity);
        harvestedBlock.GetComponent<HarvestedBlock>().SellCost = settings.SellCost;
    }
}
