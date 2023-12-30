using Bullet;
using Enemy;
using Player;
using UnityEngine;
using Utilities;

namespace Service
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        [Header("Services")]
        private PlayerService playerService;
        private EnemyService enemyService;

        [Header("Prefabs")]
        [SerializeField] private PlayerView playerPrefab;
        [SerializeField] private BulletView bullerPrefab;
        [SerializeField] private EnemyView enemyHelicopterPrefab;
        [SerializeField] private EnemyView enemyParatrooperPrefab;

        [Header("Scriptable Objects/ Data")]
        [SerializeField] private PlayerModel playerData;
        [SerializeField] private BulletModel bulletData;
        [SerializeField] private EnemyModel enemyData;

        [Header("Enemy Position")]
        [SerializeField] private Transform[] enemyHelicopterSpawnPosition;

        private void Start()
        {
            playerService = new PlayerService(playerPrefab, playerData, bullerPrefab, bulletData);
            enemyService = new EnemyService(enemyHelicopterPrefab, enemyParatrooperPrefab, enemyData);  
            Spawn();
        }

        private void OnEnable()
        {
            //InvokeRepeating(nameof(Spawn), 2f, 2f);
        }

        private void Update() 
        {
            int left = enemyService.GetLeftParatrooper().Count;
            Debug.Log("Left List" + left);

            int right = enemyService.GetLeftParatrooper().Count;
            Debug.Log("Right List" + right);
        }

        public void Spawn()
        {
            enemyService?.Update(enemyHelicopterSpawnPosition);
        }

        public PlayerService GetPlayerService() => playerService;
        public PlayerView GetPlayerPrefab() => playerPrefab;

        public EnemyService GetEnemyService() => enemyService;    
    }
}