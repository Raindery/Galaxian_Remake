using Galaxian.Spaceship;
using NaughtyAttributes;
using UnityEngine;

namespace Galaxian.Common
{
    public class PlayerControlHandler: MonoBehaviour
    {
        [Header("Rotation")] 
        [SerializeField, InputAxis] private string _rotationAroundXInputAxis;
        [SerializeField, MinValue(0.1f)] private float _rotationAroundXSpeed;
        [SerializeField, InputAxis] private string _rotationAroundYInputAxis;
        [SerializeField, MinValue(0.1f)] private float _rotationAroundYSpeed;
        
        private bool _isSpaceshipAttached;
        private BaseSpaceship _playerSpaceship;
        private Transform _cachedPlayerSpaceshipTransform;
        
        
        private void Awake()
        {
            if (TryGetComponent<BaseSpaceship>(out _playerSpaceship))
            {
                _cachedPlayerSpaceshipTransform = _playerSpaceship.SpaceshipCharacterController.transform;
                _isSpaceshipAttached = true;
            }
            else
            {
                Debug.LogWarning("Spaceship is not attached!"); 
            }
                
        }

        private void Update()
        {
            if(!_isSpaceshipAttached)
                return;
            
            MovementHandle();
        }


        private void MovementHandle()
        {
            float rotateAroundX = Mathf.Clamp(Input.GetAxis(_rotationAroundXInputAxis), -1, 1) * _rotationAroundXSpeed * Time.deltaTime;
            float rotateAroundY = Mathf.Clamp(Input.GetAxis(_rotationAroundYInputAxis),-1, 1) * _rotationAroundYSpeed * Time.deltaTime;
            _cachedPlayerSpaceshipTransform.Rotate(new Vector2(rotateAroundX, rotateAroundY));

            _playerSpaceship.SpaceshipCharacterController.Move(
                Time.deltaTime * _playerSpaceship.Speed * _cachedPlayerSpaceshipTransform.forward);
        }
        
    }
}