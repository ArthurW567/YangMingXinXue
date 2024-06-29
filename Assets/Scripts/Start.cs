using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    public GameObject StopButton;
    public GameObject RestartButton;
    public GameObject ContinueButton;
    public void OnMouseDown()
    {
        gameObject.SetActive(false);
        StopButton.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
        ContinueButton.gameObject.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Playing);
    }
}
