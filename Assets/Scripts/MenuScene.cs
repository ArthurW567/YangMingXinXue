using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene : MonoBehaviour
{
    public void StartButton()
	{
		LevelManager.level = 1;
		LevelManager.GoToDialogueScene();
	}
}
