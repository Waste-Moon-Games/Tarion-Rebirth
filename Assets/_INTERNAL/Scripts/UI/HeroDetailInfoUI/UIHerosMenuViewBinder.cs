using Core.DI;
using R3;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HeroDetailInfoUI
{
    public class UIHerosMenuViewBinder : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;

        public void Bind(DIContainer container, Subject<Unit> exitSignalSubj, HerosMenuView view)
        {
            view.Bind(container, exitSignalSubj, _exitButton);
        }
    }
}