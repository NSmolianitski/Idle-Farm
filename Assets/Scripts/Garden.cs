using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Garden : MonoBehaviour
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private Plant plantPrefab;
    [SerializeField] private float plantIndent = 0.8f;

    private void Awake()
    {
        var growPosition = transform.position;
        for (var i = 0; i < height; ++i)
        {
            for (var j = 0; j < width; ++j)
            {
                Instantiate(plantPrefab, growPosition, Quaternion.Euler(0, Random.Range(0, 360), 0), transform);
                growPosition.x += plantIndent;
            }

            growPosition.z -= plantIndent;
            growPosition.x = transform.position.x;
        }
    }

    private void OnDrawGizmos()
    {
        var growPosition = transform.position;
        for (var i = 0; i < height; ++i)
        {
            for (var j = 0; j < width; ++j)
            {
                Gizmos.DrawSphere(growPosition, 0.1f);
                growPosition.x += plantIndent;
            }

            growPosition.z -= plantIndent;
            growPosition.x = transform.position.x;
        }
    }
}