//**************************************************
// Bullet.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 1 July 2021
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSoldiers
{
    public enum BulletParentType
    {
        NONE = 0,
        PLAYER,
        ENEMY
    }

	public class Bullet : MonoBehaviour
	{
        public IBullet _BulletListener = null;

        [SerializeField]
        private Rigidbody _rigidbody = null;

        [SerializeField]
        private BulletParentType _parentType = BulletParentType.NONE;
        public BulletParentType _ParentType => _parentType;

        [SerializeField]
        private Vector3 _moveDirection;
        public Vector3 _MoveDirection
        {
            get => _moveDirection;
            set => _moveDirection = value;
        }

        [SerializeField]
        private Transform _parent;
        public Transform _Parent
        {
            get => _parent;
            set => _parent = value;
        }

        [SerializeField]
        private float _bounds = 0;
        public float _Bound
        {
            get => _bounds;
            set => _bounds = value;
        }

        [SerializeField]
        private float _movementVelocity = 0f;

        [SerializeField]
        private bool _canMove = false;
        public bool _CanMove
        {
            get => _canMove;
            set => _canMove = value;
        }


        private void Update()
        {
            if (_canMove)
            {
                CheckIsVisible(_parent);
            }
        }

        private void FixedUpdate()
        {
            if (_canMove)
            {
                MoveBullet(transform.forward);
            }
        }

        public void InitializeBullet(BulletParentType parentType)
        {
            _rigidbody = GetComponent<Rigidbody>();

            _parentType = parentType;
        }

        public void MoveBullet(Vector3 moveDirection)
        {
            _rigidbody.velocity = moveDirection * _movementVelocity;
        }

        public void SetVelocity(float velocity)
        {
            _movementVelocity = velocity;
        }

        public void SetBulletParent(Transform parent)
        {
            _parent = parent;
        }

        public void SetBulletBounds(float bounds)
        {
            _bounds = bounds;
        }

        public void AddBulletToQueue(Queue queue)
        {
            queue.Enqueue(this);
        }

        public void AddBulletToList(List<Bullet> list)
        {
            list.Add(this);
        }

        public void Hide()
        {
            _canMove = false;

            transform.SetParent(_parent);
            transform.position = _parent.position;

            gameObject.SetActive(false);
        }

        public void CheckIsVisible(Transform parent)
        {
            if(transform.position.z < -_bounds || transform.position.z > _bounds)
            {
                Hide();
            }
        }

        private void OnBecameInvisible()
        {
            _BulletListener.ResetBulletRequest(this);
        }
    }
}
