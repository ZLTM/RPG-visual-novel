using Naninovel;
using Naninovel.Commands;
using UnityEngine;
using UnityEngine.EventSystems;

[CommandAlias("adventure")]
public class SwitchToAdventureMode : Command
{
    public override async UniTask ExecuteAsync (AsyncToken asyncToken = default)
    {
        // 1. Disable Naninovel input.
        var inputManager = Engine.GetService<IInputManager>();
        inputManager.ProcessInput = false;

        // 2. Stop script player.
        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        scriptPlayer.Stop();

        // 3. Reset state.
        var stateManager = Engine.GetService<IStateManager>();
        await stateManager.ResetStateAsync();

        // 4. Switch cameras.
        var advCamera = GameObject.Find("ADVCamera").GetComponent<Camera>();
        advCamera.enabled = true;
        var naniCamera = Engine.GetService<ICameraManager>().Camera;
        naniCamera.enabled = false;

        // 5. Enable character control.
        var advEvent = GameObject.Find("ADVEventSystem").GetComponent<EventSystem>();
        advCamera.enabled = true;
        var vnEvent = GameObject.Find("InputManager").GetComponent<EventSystem>();
        vnEvent.enabled = false;
        //var controller = Object.FindObjectOfType<CharacterController3D>();
        //controller.IsInputBlocked = false;
    }
}