//**************************************************
// EnemyCharacter.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 29 June 2021
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSoldiers
{
	public class EnemyCharacter : Character
	{
        [SerializeField]
        private Rigidbody _rigidbody = null;
        public Rigidbody _Rigidbody => _rigidbody;

        [SerializeField]
        private Vector3 _startPosition;
        public Vector3 _StartPosition
        {
            get => _startPosition;
            set => _startPosition = value;
        }

         public override void InitializeCharacter(MainManager mainManager)
        {
            base.InitializeCharacter(mainManager);

            _rigidbody = GetComponent<Rigidbody>();

            _LivePoints = mainManager._GameConfig._EnemyLivePoints;

            CreateBulletsPool(BulletParentType.ENEMY);
        }

        public override void UpdateCharacter(MainManager mainManager)
        {
            base.UpdateCharacter(mainManager);
        }

        public override void FixedUpdateCharacter(MainManager mainManager)
        {
            base.FixedUpdateCharacter(mainManager);
        }

        public override void DeinitCharacter(MainManager mainManager)
        {
            base.DeinitCharacter(mainManager);
        }

        public override void Die()
        {
            base.Die();

            var enemyIndex = _MainManager._EnemyManager._Enemies.IndexOf(this);

            _MainManager._EnemyManager._DeadEnemies.Add(_MainManager._EnemyManager._Enemies[enemyIndex]);
            _MainManager._EnemyManager._Enemies.RemoveAt(enemyIndex);
        }

        public override void Shot()
        {
            base.Shot();
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals(Keys.Tags.BULLET_TAG))
            {
                var bullet = other.transform.GetComponent<Bullet>();

                if (bullet._ParentType == BulletParentType.PLAYER)
                {
                    _LivePoints -= 1;

                    if (_LivePoints == 0)
                    {
                        Die();
                    }

                    _MainManager._PlayerManager._Score += 1;
                  
                    bullet.Hide();
                }
            }
        }
    }
}
