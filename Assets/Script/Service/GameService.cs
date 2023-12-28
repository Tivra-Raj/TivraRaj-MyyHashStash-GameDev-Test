using Player;
using UnityEngine;
using Utilities;

namespace Service
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        private PlayerController playerController;

        [Header("Services")]

        [Header("Prefabs")]
        [SerializeField] private PlayerView playerPrefab;

        [Header("Scriptable Objects/ Data")]
        [SerializeField] private PlayerModel playerData;

        private void Start()
        {
            playerController = new PlayerController(playerPrefab, playerData);
        }

        public PlayerController GetPlayerController() => playerController;
    }
}