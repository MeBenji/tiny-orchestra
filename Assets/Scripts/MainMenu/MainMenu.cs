using UnityEngine;

namespace MainMenu {
    public class MainMenu : MonoBehaviour {
        public void OnMoveToScene(int i) {
            TransitionManager.MoveToScene?.Invoke(i);
        }

        public void OnQuit() {
            Application.Quit();
        }
    }

}