using System.Collections;
using Game.Scripts.Systems;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Enemies
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        private byte _poolSize = 30;
        internal byte currentEnemyInPool;
        
        internal ObjectPool<EnemyData> _enemyPool { get; private set; }

        [SerializeField] private EnemyData _enemyPrefab;
        [SerializeField] private float _spawnRadius = 3;
        [Space(5)]
        [Tooltip("The target from which the circular spawn of objects will occur")]
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _parentObject;
        
        private EnemyListener _enemyListener;

        
        [Inject]
        private void Construct(EnemyListener enemyListener)
        {
            _enemyListener = enemyListener;
            
            _enemyPool = new ObjectPool<EnemyData>(
                Preload,
                GetAction,
                ReturnAction,
                _poolSize);
            
          StartCoroutine(SpawnEnemy());
        }
        
        private IEnumerator SpawnEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                
                while (_poolSize > currentEnemyInPool)
                {
                    EnemyData newEnemy = _enemyPool.Get();
                    newEnemy.Initialize(_enemyPrefab.Health);
                
                    _enemyListener.Subscribe(newEnemy);
                    
                    currentEnemyInPool += 1;
                    yield return null;
                }
            }
        }

        //Нужно спавнить не рандомно по квадрату, а рандомно по квадрату вокруг игрока
        private EnemyData Preload()
        {
            RandomSpawnAroundTarget(out Vector2 spawnPosition);
            
            return Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity, _parentObject);
        }

        private void ReturnAction(EnemyData enemy) => enemy.gameObject.SetActive(false);
        
        private void GetAction(EnemyData enemy)
        {
            RandomSpawnAroundTarget(out Vector2 spawnPosition);
            
            EnableEnemy(spawnPosition, enemy);
        }

        private void EnableEnemy(Vector2 spawnPosition, EnemyData enemy)
        {
            enemy.transform.position = spawnPosition;
            enemy.gameObject.SetActive(true);
        }

        private void RandomSpawnAroundTarget(out Vector2 spawnPosition)
        {
            Vector2 targetPosition = _target.position;
            
            float randomAngle = Random.Range(0, Mathf.PI * 2);
            
            float randomDistance = Random.Range(_spawnRadius, _spawnRadius * 2);
            
            Vector2 randomOffset = new Vector2(
                Mathf.Cos(randomAngle),
                Mathf.Sin(randomAngle)) * randomDistance;
            
            spawnPosition = targetPosition + randomOffset;
            
            float minDistanceToPlayer = 2.0f;
            
            if (Vector2.Distance(spawnPosition, targetPosition) < minDistanceToPlayer)
                spawnPosition += (spawnPosition - targetPosition).normalized * minDistanceToPlayer;
        }
    }
}
