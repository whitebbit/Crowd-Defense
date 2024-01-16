﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _3._Scripts.UI.Components
{
    public class MoneyEffect : MonoBehaviour
    {
        [Header("Movement")] [SerializeField] private RectTransform fromPoint;
        [SerializeField] private RectTransform toPoint;
        [Header("Settings")] [SerializeField] private Ease explosionEase;
        [SerializeField] private Ease moveEase;
        [Space] [SerializeField] private float explosionRadius;
        [SerializeField] private float explosionDuration;
        [SerializeField] private float moveDuration;
        [Space] [SerializeField] private RectTransform template;

        private readonly List<RectTransform> _objects = new();

        public void DoMoneyEffect(int count, Action onComplete)
        {
            StopAllCoroutines();
            StartCoroutine(DoEffect(count, onComplete));
        }

        private void SpawnObjectsWithExplosion(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var randomDirection = Random.insideUnitCircle.normalized;
                var spawnPosition = fromPoint.position +
                                    new Vector3(randomDirection.x, randomDirection.y, 0) * explosionRadius;
                var obj = Instantiate(template, spawnPosition, Quaternion.identity, transform);

                _objects.Add(obj);
                obj.transform.DOScale(Vector3.zero, explosionDuration)
                    .From()
                    .SetEase(explosionEase);
            }
        }

        private void AnimateObjectsToTarget()
        {
            var i = 0;
            foreach (var obj in _objects)
            {
                obj.DOMove(toPoint.position, moveDuration)
                    .SetEase(moveEase)
                    .SetDelay(i * 0.05f)
                    .OnComplete(() => Destroy(obj.gameObject));
                
                obj.DOSizeDelta(toPoint.sizeDelta, moveDuration)
                    .SetEase(moveEase)
                    .SetDelay(i * 0.05f);
                
                i++;
            }
        }

        private IEnumerator DoEffect(int count, Action onComplete)
        {
            SpawnObjectsWithExplosion(count);
            yield return new WaitForSeconds(explosionDuration);
            AnimateObjectsToTarget();
            yield return new WaitForSeconds(moveDuration + _objects.Count * 0.1f);
            _objects.Clear();
            onComplete?.Invoke();
        }
    }
}