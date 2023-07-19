using System;
using NaughtyAttributes;
using UnityEngine;


namespace Galaxian.Spaceship
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class BaseSpaceship : MonoBehaviour
    {
        [Header("Spaceship Settings")] 
        [SerializeField, MinValue(1)] private int _health = 1;
        [SerializeField, MinValue(1f)] private float _speed;


        public int Health
        {
            get { return _health;}
            protected set { _health = value; }
        }

        public float Speed
        {
            get { return _speed; }
            protected set { _speed = value; }
        }
        

        private CharacterController _characterController;

        public CharacterController SpaceshipCharacterController
        {
            get
            {
                if (_characterController == null)
                    _characterController = GetComponent<CharacterController>();
                return _characterController;
            }
        }


        protected void Shoot(Vector3 direction)
        {
            throw new NotImplementedException();
        }
    }
}


