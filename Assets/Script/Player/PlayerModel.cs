using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject", order = 1)]
    public class PlayerModel : ScriptableObject
    {
        [Header("Cannon turrent Rotation")]
        public float RotationSpeed = 100f; 
        public float MinRotationAngle = -90f;     
        public float MaxRotationAngle = 90f;

        public PlayerController PlayerController { get; private set; }

        public void SetPlayerController(PlayerController playerController)
        {
            PlayerController = playerController;
        }
    }
}