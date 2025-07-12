using UnityEngine;

namespace UI.Base
{
    public class SimpleUIItem : MonoBehaviour
    {
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}