using System;
using UnityEngine;

[Serializable]
public class Wave
{
    [SerializeField] private GameObject _attackerPrefab;

    [Range(1, 1000)]
    [SerializeField] private int _countAttackers;

    [Range(0.1f, 5f)]
    [SerializeField] private float _delayBetweenSpawn;

    public GameObject AttackerPrefab => _attackerPrefab;
    public int CountAttackers => _countAttackers;
    public float DelayBetweenSpawn => _delayBetweenSpawn;
}
