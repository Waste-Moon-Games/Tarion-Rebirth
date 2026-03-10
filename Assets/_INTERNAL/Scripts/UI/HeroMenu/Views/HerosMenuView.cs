using UnityEngine;

namespace UI.HeroUI
{
    public class HerosMenuView : MonoBehaviour
    {
        public void AttachView(GameObject view)
        {
            view.transform.SetParent(transform, false);
        }
    }
}