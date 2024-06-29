using UnityEngine;

namespace Core
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private static DontDestroyOnLoad instance;

        private void Awake() {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else {
                DestroyImmediate(this.gameObject);
            }
        }
    }
}