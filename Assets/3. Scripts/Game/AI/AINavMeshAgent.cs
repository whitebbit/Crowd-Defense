using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace _3._Scripts.Game.AI
{
    public class AINavMeshAgent
    {
        private readonly NavMeshAgent _navMeshAgent;

        private Vector3 _targetPosition;
        private Vector3 _lastPosition;

        private float _stoppedTime;
        
        private event Action OnFinish;
        
        public bool Moving { get; private set; }
        private Transform Transform => _navMeshAgent.transform;
        private bool Finished => (Vector3.Distance(_targetPosition, Transform.position) <= 1f);

        public AINavMeshAgent(NavMeshAgent navMeshAgent, Action action)
        {
            _navMeshAgent = navMeshAgent;
            _lastPosition = Transform.position;
            OnFinish += action;
        }
        
        public void Start(Vector3 pos)
        {
            Moving = true;
            SetTargetPosition(pos);
        }

        public void OnMove()
        {
            CheckStopping();
            Finishing();
        }

        public void Stop()
        {
            _navMeshAgent.ResetPath();
            Moving = false;
        }

        public Vector3 GetRandomTargetPosition(float minX, float maxX, float minZ, float maxZ)
        {
            var xRandDirection = Random.Range(-1, 1);
            var xRandRange = Random.Range(minX, maxX);
            var xDirection = Transform.right * xRandDirection * xRandRange;

            var zRandDirection = Random.Range(-1, 1);
            var zRandRange = Random.Range(minZ, maxZ);
            var zDirection = Player.Instance.transform.forward * zRandDirection * zRandRange;

            return Transform.position + xDirection + zDirection;
        }

        private bool IsMoving()
        {
            var position = Transform.position;
            var distance = Vector3.Distance(_lastPosition, position);
            return distance != 0;
        }
        
        private void SetTargetPosition(Vector3 pos)
        {
            _targetPosition = pos;
            _navMeshAgent.SetDestination(_targetPosition);
        }

        private void CheckStopping()
        {
            var isMoving = IsMoving();
            _lastPosition = Transform.position;

            if (isMoving)
            {
                _stoppedTime = 1;
                return;
            }

            _stoppedTime -= Time.deltaTime;
            MoveBack();
        }

        private void MoveBack()
        {
            if (!(_stoppedTime <= 0)) return;
            
            var target = Transform.position + Transform.forward * -Random.Range(25, 50);
            SetTargetPosition(target);
        }

        private void Finishing()
        {
            if (!Finished) return;

            Stop();
            OnFinish?.Invoke();
        }

        
    }
}