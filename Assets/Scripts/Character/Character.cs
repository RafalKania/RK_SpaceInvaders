//**************************************************
// Character.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 27 June 2021
//**************************************************

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSoldiers
{
	public class Character : MonoBehaviour, ICharacter, IBullet
	{
        protected MainManager _MainManager { get; private set; } = null;

        [SerializeField]
        [HideInInspector]
        private int _livePoints = 0;
        public int _LivePoints
        {
            get => _livePoints;
            set => _livePoints = value;
        }

        [SerializeField]
        private Transform _bulletParent = null;
        public Transform _BulletParent => _bulletParent;

        [SerializeField]
        private Queue<Bullet> _bulletPool = new Queue<Bullet>();
        public Queue<Bullet> _BulletPool
        {
            get => _bulletPool;
            set => _bulletPool = value;
        }

        [SerializeField]
        private List<Bullet> _usedBullets = new List<Bullet>();

        #region ICharacter implementation
        public virtual void InitializeCharacter(MainManager mainManager)
        {
            _MainManager = mainManager;
        }

        public virtual void UpdateCharacter(MainManager mainManager)
        {

        }
        public virtual void FixedUpdateCharacter(MainManager mainManager)
        {
        }

        public virtual void DeinitCharacter(MainManager mainManager)
        {
            gameObject.SetActive(false);

            foreach(Bullet b in _usedBullets)
            {
                var index = _usedBullets.IndexOf(b);

                b._CanMove = false;
                b.transform.SetParent(_bulletParent);
                b.transform.position = _bulletParent.position;
                b.Hide();
            }

            _usedBullets.Clear();
        }

        public virtual void Die()
        {
            gameObject.SetActive(false);
        }

        public virtual void Shot()
        {
            if (gameObject.activeSelf)
            {
                if (_BulletPool.Count > 0)
                {
                    var bullet = _BulletPool.Dequeue();

                    bullet.gameObject.SetActive(true);

                    bullet.transform.parent = null;

                    bullet._CanMove = true;

                    _usedBullets.Add(bullet);
                }
            }
        }
        #endregion

        #region IBullets implementation
        public virtual void CreateBulletsPool(BulletParentType parentType)
        {
            int poolCount = 0;
            switch (parentType)
            {
                case BulletParentType.PLAYER:
                    poolCount = _MainManager._GameConfig._PlayerBulletPoolCount;
                    break;
                case BulletParentType.ENEMY:
                    poolCount = _MainManager._GameConfig._EnemyBulletPoolCount;
                    break;
            }

            for (int i = 0; i < poolCount; i++)
            {
                var bullet = Instantiate(_MainManager._BulletPrefab) as Bullet;

                bullet._BulletListener = this;

                bullet.SetBulletParent(_bulletParent);
                bullet.transform.position = _bulletParent.position;
                bullet.transform.parent = _bulletParent;
                

                if(parentType == BulletParentType.ENEMY)
                {
                    bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }

                bullet.InitializeBullet(parentType);

                bullet.SetVelocity(_MainManager._GameConfig._BulletMovementVelocity);
                bullet.SetBulletBounds(_MainManager._CameraManager.GetCameraBounds().z + 1);

                _BulletPool.Enqueue(bullet);

                bullet.gameObject.SetActive(false);
            }
        }

        public virtual void ResetBulletRequest(Bullet bullet)
        {
            _BulletPool.Enqueue(bullet);
        }
        #endregion
    }
}
