using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class wavespanner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    [System.Serializable]
    public class Wave 
    {
        public string name;
        public Transform enemy; 
        public int count;
        public float rate;
    }
    public Wave[] waves;
    private int nextWave = 0;

    public float timebetweenWaves= 5f;
    public float waveCountdown = 5f;
    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    private void Start()
    {
        waveCountdown = timebetweenWaves;
    }
    private void Update()
    {
        if (state == SpawnState.WAITING) 
        {
            if (!EnemyIsAlive()) 
            {
                //begin new round
                Debug.Log("wave complete!");
                return;
            }
        }
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }

        bool EnemyIsAlive()
         {
            searchCountdown -= Time.deltaTime;
            if (searchCountdown <= 0)
            {
                //important
                searchCountdown = 1f;
                if (GameObject.FindGameObjectWithTag("Enemy") == null)
                {
                    return false;
                }
            }
            return true;
         }
    }
        IEnumerator SpawnWave(Wave _wave) 
        {
        Debug.Log("Spawning Wave" + _wave.name);
        state = SpawnState.SPAWNING;

            for (int i = 0; i < _wave.count; i++) 
            {
            SpawningEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.rate);
            }
             
            state = SpawnState.WAITING;

            yield break;
    }

    void SpawningEnemy(Transform _enemy) 
    {
        Debug.Log("Spawning Enemy" + _enemy.name);
        Instantiate(_enemy, transform.position, transform.rotation);
    }

}
