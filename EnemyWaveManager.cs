//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyWaveManager : MonoBehaviour
//{
//    public event EventHandler OnWaveNumberChanged;
//    private enum State
//    {
//        WaitinToSpawnNextWave,
//        SpawningWave,
//    }

    
//    [SerializeField] private Transform nextWaveSpawnPositionTransform;
//    [SerializeField] private List<Transform> enemySpawnPositionTransformList;


   

//    private State state;
//    private int waveNumber;
//    private float nextWaveSpawnTimer;
//    private float nextEnemySpawnTimer;
//    private int remainingEnemySpawnAmount;
//    private Vector3 spawnPosition;
//    private void Start()
//    {
//        state = State.WaitinToSpawnNextWave;
//        spawnPosition = enemySpawnPositionTransformList[UnityEngine.Random.Range(0, enemySpawnPositionTransformList.Count)].position;
//        nextWaveSpawnPositionTransform.position = spawnPosition;
//        nextWaveSpawnTimer = 3f;    // ilk wave'in gelme süresi
       
//    }
//    private void Update()
//    {
//        switch (state)
//        {
//            case State.WaitinToSpawnNextWave:
//                nextWaveSpawnTimer -= Time.deltaTime;
//                if (nextWaveSpawnTimer <= 0)
//                {
//                    SpawnWave();
//                }
//                break;

//            case State.SpawningWave:
//                if (remainingEnemySpawnAmount > 0)
//                {
//                    nextEnemySpawnTimer -= Time.deltaTime;
//                    if (nextEnemySpawnTimer < 0f)
//                    {
//                        nextEnemySpawnTimer = UnityEngine.Random.Range(2f, 3f);
//                        Minion.Create(MinionManager.Instance.GetRandomMinionFromFirstSet(), false, spawnPosition /*+ UtilsClass.GetRandomDir() * UnityEngine.Random.Range(0f, 10f)*/);

//                        remainingEnemySpawnAmount--;

//                        if (remainingEnemySpawnAmount <= 0) //tüm düşmanlar spawn'landıktan sonra
//                        {
//                            state = State.WaitinToSpawnNextWave;
//                            spawnPosition = enemySpawnPositionTransformList[UnityEngine.Random.Range(0, enemySpawnPositionTransformList.Count)].position;
//                            nextWaveSpawnPositionTransform.position = spawnPosition;
//                            nextWaveSpawnTimer = 15f;
//                        }
//                    }
//                }
//                break;
//        }
//    }
//    private void SpawnWave()
//    {
//        remainingEnemySpawnAmount = 10 + 2 * waveNumber;
//        state = State.SpawningWave;
//        waveNumber++;
//        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
//    }
//    public int GetWaveNumber()
//    {
//        return waveNumber;
//    }
//    public float GetNextWaveSpawnTimer()
//    {
//        return nextWaveSpawnTimer;
//    }
//    public Vector3 GetSpawnPosition()
//    {
//        return spawnPosition;
//    }
   
    
//}
