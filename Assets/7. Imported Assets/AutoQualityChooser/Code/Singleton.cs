using UnityEngine;

namespace net.krej.Singleton {
    public class Singleton<T> : SingletonBase where T : MonoBehaviour {
        public const string LOG_TAG = "[singleton] ";
        private static T instance;

        /// <summary>
        /// Returns the instance of this singleton.
        /// </summary>
        public static T Instance {
            get {
                Instantiate();
                return instance;
            }
        }

        public static T InstanceOrNull {
            get { return instance ?? (instance = (T)FindObjectOfType(typeof(T))); }
        }

        public static bool Exists() {
            return InstanceOrNull != null;
        }

        public override void Elect() {
            instance = this as T;
        }

        /// <summary>
        /// If instance is not on scene, create it.
        /// </summary>
        public static void Instantiate() {
            if (ReferenceEquals(instance, null)) {
                var instances = FindObjectsOfType(typeof(T));
                if (instances.Length > 1)
                    Debug.LogError(
                        LOG_TAG + "<B>Doubleton?</B> Do you really want two instances of <B><i>" + typeof(T).Name +
                        "</i></B>?\n", instances[1]);
                if (instances.Length >= 1) instance = (T)instances[0];
                if (ReferenceEquals(instance, null)) {
                    Debug.Log(LOG_TAG + "Creating " + typeof(T).Name +
                              " Singleton instance on the fly. \n\tTo have it's fields configured, add it manually to a GameObject");
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
                    instance.SendMessage("OnInstantiate");
                }
            }
        }

        public static bool AreTooManyOnScene() {
            var instances = FindObjectsOfType(typeof(T));
            return instances.Length > 1;
        }

        protected virtual void OnInstantiate() {
        }

        public static Transform STransform {
            get { return Instance.transform; }
        }

        public static Vector3 SPosition {
            get { return Instance.transform.position; }
            set { Instance.transform.position = value; }
        }

        public static GameObject SGameObject {
            get { return Instance.gameObject; }
        }

        public static Rigidbody SRigidbody {
            get { return Instance.GetComponent<Rigidbody>(); }
        }

        public virtual void OnDestroy() {
            instance = null;
        }
    }

    public abstract class SingletonBase : MonoBehaviour {
        public abstract void Elect();
    }
}