using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    public GameObject StopButton;
    public GameObject ContinueButton;
    public GameObject StartButton;
    public GameObject RestartButton;
    public void OnMouseDown()
    {
        StopButton.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
        ContinueButton.gameObject.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Continue);
    }
}
