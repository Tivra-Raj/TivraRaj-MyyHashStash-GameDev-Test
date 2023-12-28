using Bullet;
using Player;
using UnityEngine;
using Utilities;

namespace Service
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        [Header("Services")]
        private PlayerService playerService;

        [Header("Prefabs")]
        [SerializeField] private PlayerView playerPrefab;
        [SerializeField] private BulletView bullerPrefab;

        [Header("Scriptable Objects/ Data")]
        [SerializeField] private PlayerModel playerData;
        [SerializeField] private BulletModel bulletData;

        private void Start()
        {
            playerService = new PlayerService(playerPrefab, playerData, bullerPrefab, bulletData);
        }

        public PlayerService GetPlayerService() => playerService;
    }
}