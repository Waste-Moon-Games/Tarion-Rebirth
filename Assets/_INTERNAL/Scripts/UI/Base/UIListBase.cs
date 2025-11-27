using UnityEngine;

namespace UI.Base
{
    public class UIListBase : MonoBehaviour
    {
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}