using UnityEngine;
using Naninovel;
using UnityEngine.SceneManagement;

public class NaniDialogueSceneManager : MonoBehaviour
{
    public string ScriptName = "textcontrol";
    bool running = false;
    private async void Start()
    {
        //var inputManager = Engine.GetService<IInputManager>();
        //inputManager.ProcessInput = true;
        Debug.Log("Play Script: " + LevelManager.dialogueLabel);

        await RuntimeInitializer.InitializeAsync();
        Debug.Log("2");
        var naniCamera = Engine.GetService<ICameraManager>().Camera;
        naniCamera.enabled = true;
        var naniUICamera = Engine.GetService<ICameraManager>().UICamera;
        naniUICamera.enabled = true;
        naniCamera.cullingMask = ~0;
        naniUICamera.cullingMask = ~0;
        Debug.Log("3");
        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        scriptPlayer.PreloadAndPlayAsync(ScriptName, label: LevelManager.dialogueLabel).Forget();

        Debug.Log("4");
        System.Action<Script> action = delegate { OnScriptStop(); };
        scriptPlayer.OnStop += action;
        running = true;
    }

    void OnScriptStop()
	{
        if (!running)
            return;
        running = false;

        Debug.Log("5");
        var naniCamera = Engine.GetService<ICameraManager>().Camera;
        Debug.Log("Camera: "+naniCamera.gameObject.name);
        naniCamera.enabled = false;
        var naniUICamera = Engine.GetService<ICameraManager>().UICamera;
        naniUICamera.enabled = false;
        Debug.Log("6");

        var stateManager = Engine.GetService<IStateManager>();
        stateManager.ResetStateAsync();

        if(LevelManager.level <=6)
            SceneManager.LoadScene("Game");
        else
            SceneManager.LoadScene("Menu");
    }
}
