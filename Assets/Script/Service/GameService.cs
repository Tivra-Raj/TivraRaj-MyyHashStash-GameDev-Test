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

        [Header("Miscellaneous")]
        [SerializeField] private Transform GroundObject;
        private int totalNoOfHelicoperSpawned;

        private void Start()
        {
            playerService = new PlayerService(playerPrefab, playerData, bullerPrefab, bulletData);
            enemyService = new EnemyService(enemyHelicopterPrefab, enemyParatrooperPrefab, enemyData);   
        }

        private void OnEnable()
        {
            if(totalNoOfHelicoperSpawned != 5)
            {
                InvokeRepeating(nameof(Spawn), 2f, 5f);
            }
        }

        private void Update() 
        {
            enemyService?.MoveParatroopersTowardsTurret();
        }

        public void Spawn()
        {
            enemyService?.Spawn(enemyHelicopterSpawnPosition);
            totalNoOfHelicoperSpawned++;
        }

        public PlayerService GetPlayerService() => playerService;
        public PlayerView GetPlayerPrefab() => playerPrefab;

        public EnemyService GetEnemyService() => enemyService;
        
        public Transform GetGroundPosition() => GroundObject.transform;
    }
}