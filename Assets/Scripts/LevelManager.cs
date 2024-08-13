using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private int i = -1;
    public int[] number;

    public static string dialogueLabel = "Level1";

    public static void GoToDialogueScene()
	{
        SceneManager.LoadScene("Dialogue");
	}

    public void Victory()
    {
        level++;
        dialogueLabel = "Level" + level;
        if(level >= 7)
            dialogueLabel = "End";
        GoToDialogueScene();
    }

    public void Fail()
	{
        dialogueLabel = "Level" + level + "_Fail";
        GoToDialogueScene();
    }

    private void Awake()
    {
        Instance = this;
    }
    public void GenerateSerial()
    {
        i = -1;
        number = Serial();
    }
    [SerializeField] public static int level = 1;
    [SerializeField] private float wave;

    public int GetLevel()
    {
        return level;
    }
    public int LevelTime()
    {
        switch (level)
        {
            case 1:
                return 60;
            case 2:
                return 90;
            case 3:
                return 120;
            case 4:
                return 120;
            case 5:
                return 150;
            case 6:
                return 180;
            case 7:
                return 180;
        }
        return 0;
    }
    public int TotalNumber()
    {
        switch (level)
        {
            case 1:
                return 100;
            case 2:
                return 150;
            case 3:
                return 200;
            case 4:
                return 200;
            case 5:
                return 300;
            case 6:
                return 350;
            case 7:
                return 400;
        }
        return 0;
    }
    public int []Serial()
    {
        int totalnumber = 0;
        int[] number = new int[400];
        switch (level)
        {
            case 1:
                totalnumber = 100;
                for(int a = 0; a < 60; a++) { number[a] = 0; }
                for (int a = 60; a <80; a++) { number[a] = 1; }
                for (int a = 80; a < 100; a++) { number[a] = 2; }
                break;
            case 2:
                totalnumber = 150;
                for (int a = 0; a < 75; a++) { number[a] = 0; }
                for (int a = 75; a < 105; a++) { number[a] = 1; }
                for (int a = 105; a < 120; a++) { number[a] = 2; }
                for (int a = 120; a < 135; a++) { number[a] = 3; }
                for (int a = 135; a < 150; a++) { number[a] = 4; }
                break;
            case 3:
                totalnumber = 200;
                for (int a = 0; a < 80; a++) { number[a] = 0; }
                for (int a = 80; a < 120; a++) { number[a] = 1; }
                for (int a = 120; a < 160; a++) { number[a] = 2; }
                for (int a = 160; a < 180; a++) { number[a] = 3; }
                for (int a = 180; a < 200; a++) { number[a] = 4; }
                break;
            case 4:
                totalnumber = 200;
                for (int a = 0; a < 60; a++) { number[a] = 0; }
                for (int a = 60; a < 110; a++) { number[a] = 1; }
                for (int a = 110; a < 150; a++) { number[a] = 2; }
                for (int a = 150; a < 180; a++) { number[a] = 3; }
                for (int a = 180; a < 190; a++) { number[a] = 4; }
                for (int a = 190; a < 200; a++) { number[a] = 5; }
                break;
            case 5:
                totalnumber = 300;
                for (int a = 0; a < 60; a++) { number[a] = 0; }
                for (int a = 60; a < 110; a++) { number[a] = 1; }
                for (int a = 110; a < 170; a++) { number[a] = 2; }
                for (int a = 170; a < 220; a++) { number[a] = 3; }
                for (int a = 220; a < 270; a++) { number[a] = 4; }
                for (int a = 270; a < 300; a++) { number[a] = 5; }
                break;
            case 6:
                totalnumber = 350;
                for (int a = 0; a < 70; a++) { number[a] = 0; }
                for (int a = 70; a < 120; a++) { number[a] = 1; }
                for (int a = 120; a < 190; a++) { number[a] = 2; }
                for (int a = 190; a < 260; a++) { number[a] = 3; }
                for (int a = 260; a < 320; a++) { number[a] = 4; }
                for (int a = 320; a < 350; a++) { number[a] = 5; }
                break;
            case 7:
                totalnumber = 400;
                for (int a = 0; a < 70; a++) { number[a] = 0; }
                for (int a = 700; a < 130; a++) { number[a] = 1; }
                for (int a = 130; a < 190; a++) { number[a] = 2; }
                for (int a = 190; a < 290; a++) { number[a] = 3; }
                for (int a = 290; a < 350; a++) { number[a] = 4; }
                for (int a = 350; a < 400; a++) { number[a] = 5; }
                break;
        }
        for(int a = 0; a < totalnumber; a++)
        {
            int x;
            x = number[a];
            number[Random.Range(0, totalnumber)] = x;
            number[a] = number[Random.Range(0, totalnumber)];
        }
        return number;
    }
    public string GetName()
    {
        string[] name = new string[] { "A", "B", "C", "D", "E", "F" };
        i++;
        return name[number[i]]; 
    }
    public float Wave()
    {
        switch (level)
        {
            case 1:
                wave=30;
                break;
            case 2:
                wave = 45;
                break;
            case 3:
                wave = 40;
                break;
            case 4:
                wave = 40;
                break;
            case 5:
                wave = 36;
                break;
            case 6:
                wave = 45;
                break;
            case 7:
                wave = 45;
                break;
        }
        return wave;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
