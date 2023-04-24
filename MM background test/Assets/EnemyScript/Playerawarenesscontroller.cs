using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerawarenesscontroller : MonoBehaviour
{
   // public Object findplayer;
    public bool AwareofPlayer { get; private set; }
    public Vector2 DirectiontoPlayer { get; private set; }
   
    [SerializeField]
    private float _playerAwarnessDistance;

    public Transform _player;

        private void Awake() 
    {
        _player = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectiontoPlayer = enemyToPlayerVector.normalized;
        if (enemyToPlayerVector.magnitude <= _playerAwarnessDistance)
        {
            AwareofPlayer = true;
        }
        else
        {
            AwareofPlayer = false;
        }
    }
}
