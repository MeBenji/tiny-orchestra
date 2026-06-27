using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public delegate void GetInt(int i);
    public static GetInt MoveToScene;

    private void Awake() {
        MoveToScene = OnMoveToScene;
    }

    void OnMoveToScene(int i) {

        StartCoroutine(corrutine());

        IEnumerator corrutine() {

            //Transition animation

            yield return null;

            SceneManager.LoadScene(i);
        }
    }
}
