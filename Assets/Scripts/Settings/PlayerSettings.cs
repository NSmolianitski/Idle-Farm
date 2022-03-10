using UnityEngine;

[CreateAssetMenu(fileName = "New Player Settings", menuName = "Player Settings")]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] private float speed = 150f;

    public float Speed => speed;
}