using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        [Tooltip("Уникальный ID для идентификации объекта")]
        [SerializeField] private string _uniqID = "Default";

        private static Dictionary<string, DontDestroyOnLoad> _instances = new();

        private void Awake()
        {
            if (_instances.TryGetValue(_uniqID, out var existing))
            {
                if (existing != null)
                {
                    Destroy(existing);
                    return;
                }
            }

            _instances[_uniqID] = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}