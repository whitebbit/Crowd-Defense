using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _3._Scripts.UI.Extensions
{
    public static class UIRaycast
    {
        public static T FindObject<T>(Vector2 position) 
        {
            var results = GetRaycastResults(position);
            return TryFindObject<T>(results);
        }

        private static T TryFindObject<T>(IReadOnlyList<RaycastResult> results)
        {
            if (results.Count <= 0) return default;
            var obj = results[0].gameObject;
            return obj.TryGetComponent(out T type) ? type : default;
        }

        private static List<RaycastResult> GetRaycastResults(Vector2 position)
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = position
            };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results;
        }

        
    }
}