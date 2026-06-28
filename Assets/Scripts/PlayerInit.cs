using UnityEngine;

public class PlayerInit : MonoBehaviour
{
    private void OnEnable()
    {
        ScoreManager.Instance.OnResetPoints();
        GameManager.Instance.onStart.Invoke();
        GetComponent<Direct>().enabled = true;
        GetComponent<PlayerController>().enabled = true;
    }
}
