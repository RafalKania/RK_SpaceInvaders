//**************************************************
// PlayerCharacter.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 27 June 2021
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSoldiers
{
	public class PlayerCharacter : Character, IMove
	{
        private Queue<Bullet> _shotBullets = new Queue<Bullet>();

        [SerializeField]
        private Rigidbody _rigidbody = null;

        [SerializeField]
        private Collider _collider = null;

        [SerializeField]
        private GameObject _invulnerabilityShield = null;

        [SerializeField]
        private bool _canMove = false;
        public bool _CanMove
        {
            get => _canMove;
            set => _canMove = value;
        }

        [SerializeField]
        private bool _isInvulnerable = false;

        [SerializeField]
        private float _invunerableTime = 0;

        //[SerializeField]
        //private Vector2 _moveBounds;

        [SerializeField]
        private float _moveBounds = 0;

        private Vector3 _moveDirection;

        [SerializeField]
        private bool _move = false;
        public bool _Move
        {
            get => _move;
            set => _move = value;
        }

        public override void InitializeCharacter(MainManager mainManager)
        {
            base.InitializeCharacter(mainManager);

            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();

            _LivePoints = mainManager._GameConfig._MaxPlayerLives;
            _invunerableTime = mainManager._GameConfig._PlayerInvunerableTime;
            _moveBounds = mainManager._CameraManager.GetCameraBounds().x - 1;
            _canMove = false;
        }

        public override void UpdateCharacter(MainManager mainManager)
        {
            base.UpdateCharacter(mainManager);

            if (_isInvulnerable)
            {
                _invunerableTime -= Time.deltaTime;

                if(_invunerableTime < 0)
                {
                    _isInvulnerable = false;
                    _invulnerabilityShield.SetActive(false);
                    _collider.enabled = true;

                    _invunerableTime = mainManager._GameConfig._PlayerInvunerableTime;
                }
            }
        }

        public override void FixedUpdateCharacter(MainManager mainManager)
        {
            base.FixedUpdateCharacter(mainManager);

            if (_canMove)
            {
                if (_move)
                {
                    if (transform.position.x > -_moveBounds && transform.position.x < _moveBounds)
                    {
                        _rigidbody?.MovePosition(transform.position + (_moveDirection * mainManager._GameConfig._PlayerMovingVelocity * Time.deltaTime));
                    }
                }
            }

            if (transform.position.x < -_moveBounds)
            {
                _rigidbody?.MovePosition(transform.position + 
                    (new Vector3(-(transform.position.x + _moveBounds) + 0.2f, transform.position.y, transform.position.z) 
                    * mainManager._GameConfig._PlayerMovingVelocity 
                    * Time.deltaTime));
            }

            if (transform.position.x > _moveBounds)
            {
                _rigidbody?.MovePosition(transform.position +
                    (new Vector3((transform.position.x - _moveBounds) - 0.2f, transform.position.y, transform.position.z)
                    * mainManager._GameConfig._PlayerMovingVelocity
                    * Time.deltaTime));
            }
        }

        public override void DeinitCharacter(MainManager mainManager)
        {
            base.DeinitCharacter(mainManager);
        }

        public override void Shot()
        {
            base.Shot();
        }

        public override void Die()
        {
            base.Die();
        }

        #region IBullet implementation
        public override void CreateBulletsPool(BulletParentType parentType)
        {
            base.CreateBulletsPool(parentType);
        }

        public override void ResetBulletRequest(Bullet bullet)
        {
            base.ResetBulletRequest(bullet);
        }
        #endregion

        #region IMove implementation
        public void DirectionMove(Vector3 direction)
        {
            _move = true;

            _moveDirection = direction;
        }

        public void StopMoving()
        {
            _move = false;
        }
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals(Keys.Tags.BULLET_TAG))
            {
                var bullet = other.transform.GetComponent<Bullet>();

                if (bullet._ParentType == BulletParentType.ENEMY)
                {
                    if (!_isInvulnerable)
                    {
                        _LivePoints -= 1;
                    }

                    if (_LivePoints == 0)
                    {
                        Die();
                    }
                    else
                    {
                        _isInvulnerable = true;
                        _invulnerabilityShield.SetActive(true);
                        _collider.enabled = false;
                    }
                    bullet.Hide();
                }
            }

            if (other.gameObject.tag.Equals(Keys.Tags.ENEMY_TAG))
            {
                _LivePoints = 0;
                Die();
            }
        }
    }
}
