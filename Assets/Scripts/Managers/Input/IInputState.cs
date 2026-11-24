using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public interface IInputState
{
    void Enter();
    void Exit();
}
public abstract class BaseInputState : IInputState
{
    protected virtual InputActionMap[] GetInputActionMaps() => null;
    protected virtual InputActionMap GetInputActionMap() => null;

    InputActionMap[] actionMaps;

    public virtual void Enter()
    {
        var map = GetInputActionMap();
        if(map != null)
        {
            actionMaps = new InputActionMap[]{map};
        }else
        {
            actionMaps = GetInputActionMaps();
        }
        InputHandler.Instance.SwitchActionMaps(actionMaps);
        SubcribeEvent();
    }
    public virtual void Exit()
    {
        UnsubcribeEvent();
    }
    protected abstract void SubcribeEvent();
    protected abstract void UnsubcribeEvent();
}
public class GameplayState : BaseInputState
{
    readonly Player player;
    InputActionMap[] actionMaps;

    public GameplayState(Player player)
    {
        this.player = player;
        actionMaps = new InputActionMap[]{InputHandler.Instance.controls.Player, InputHandler.Instance.controls.UI};
    }
    protected override InputActionMap[] GetInputActionMaps() => actionMaps;
    protected override void SubcribeEvent()
    {
        InputHandler.Instance.controls.Player.Interact.performed += ctx => player.TryInteract();
        InputHandler.Instance.controls.Player.Move.performed += OnMovePerformed;
        InputHandler.Instance.controls.Player.Move.canceled += OnMoveCanceled;
        InputHandler.Instance.controls.Player.Back.performed += ctx =>
            GameManager.Instance.GoBack();
        CameraManager.Instance.SetActiveCinemachineCamera(true);
    }

    protected override void UnsubcribeEvent()
    {
        player.Stop();
        InputHandler.Instance.controls.Player.Interact.performed -= ctx => player.TryInteract();
        InputHandler.Instance.controls.Player.Move.performed -= OnMovePerformed;
        InputHandler.Instance.controls.Player.Move.canceled -= OnMoveCanceled;
        InputHandler.Instance.controls.Player.Back.performed -= ctx =>
            GameManager.Instance.GoBack();
        CameraManager.Instance.SetActiveCinemachineCamera(false);
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        OnNavigate(ctx.ReadValue<Vector2>());
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        OnNavigate(Vector2.zero);
    }

    public void OnNavigate(Vector2 dir)
    {
        player.Move(dir);
    }
}

public class DialogueState : BaseInputState
{
    readonly DialogueManager dialogue;

    public DialogueState(DialogueManager dialogue)
    {
        this.dialogue = dialogue;
    }
    protected override InputActionMap GetInputActionMap() => InputHandler.Instance.controls.UI;

    protected override void SubcribeEvent()
    {
        InputHandler.Instance.controls.UI.Choose.performed += OnSubmit;
        InputHandler.Instance.controls.UI.Back.performed += OnBack;
    }

    protected override void UnsubcribeEvent()
    {
        InputHandler.Instance.controls.UI.Choose.performed -= OnSubmit;
        InputHandler.Instance.controls.UI.Back.performed -= OnBack;
    }

    public void OnSubmit(InputAction.CallbackContext ctx)
    {
        dialogue.RequestAdvance();
    }

    public void OnBack(InputAction.CallbackContext ctx)
    {
        GameManager.Instance.GoBack();
    }
}

public class ChoiceState : BaseInputState
{
    protected override InputActionMap GetInputActionMap() => InputHandler.Instance.controls.UI;
    protected override void SubcribeEvent()
    {
        InputHandler.Instance.controls.UI.Choose.performed += OnSubmit;
        InputHandler.Instance.controls.UI.Back.performed += OnBack;
    }

    protected override void UnsubcribeEvent()
    {
        InputHandler.Instance.controls.UI.Choose.performed -= OnSubmit;
        InputHandler.Instance.controls.UI.Back.performed -= OnBack;
    }

    public void OnSubmit(InputAction.CallbackContext ctx)
    {
        var selected = EventSystem.current.currentSelectedGameObject;
        selected?.GetComponent<Button>()?.onClick.Invoke();
    }

    public virtual void OnBack(InputAction.CallbackContext ctx) { }
}

public class ActionChoiceState : ChoiceState
{
    public override void OnBack(InputAction.CallbackContext ctx)
    {
        UIManager.Instance.HideAction();
        InputStateManager.Instance.SetState(new GameplayState(Player.Instance));
    }
}

public class StoryChoiceState : ChoiceState
{
    public override void OnBack(InputAction.CallbackContext ctx)
    {
        GameManager.Instance.GoBack();
    }
}

public class UIChoiceState : ChoiceState
{
    public override void OnBack(InputAction.CallbackContext ctx)
    {
        GameManager.Instance.GoBack();
    }
}

public class ReadDiaryState : IInputState
{
    readonly Diary diary;

    public ReadDiaryState(Diary diary)
    {
        this.diary = diary;
    }

    public void Enter()
    {
        InputHandler.Instance.controls.Player.Interact.performed += OnSubmitPerformed;
        InputHandler.Instance.controls.Player.Move.performed += OnMovePerformed;
        InputHandler.Instance.controls.Player.Back.performed += OnBackPerformed;
    }

    public void Exit()
    {
        InputHandler.Instance.controls.Player.Interact.performed -= OnSubmitPerformed;
        InputHandler.Instance.controls.Player.Move.performed -= OnMovePerformed;
        InputHandler.Instance.controls.Player.Back.performed -= OnBackPerformed;
    }

    private void OnSubmitPerformed(InputAction.CallbackContext ctx)
    {
        OnSubmit();
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        OnNavigate(ctx.ReadValue<Vector2>());
    }

    private void OnBackPerformed(InputAction.CallbackContext ctx)
    {
        OnBack();
    }

    public void OnNavigate(Vector2 dir)
    {
        if (dir == Vector2.right)
            diary.NextPage();
        else if (dir == Vector2.left)
            diary.PreviousPage();
    }

    public void OnSubmit()
    {
        // diary.NextPage();
    }

    public void OnBack()
    {
        diary.Close();
        InputStateManager.Instance.SetState(new GameplayState(Player.Instance));
    }
}

public abstract class MinigameState : BaseInputState
{
    readonly InputActionMap[] actionMaps;

    protected MinigameState()
    {
        actionMaps = new InputActionMap[]
        {
            InputHandler.Instance.controls.Minigame,
            GetSpecificActionMap(),
        };
    }
    protected override InputActionMap[] GetInputActionMaps() => actionMaps;

    protected abstract InputActionMap GetSpecificActionMap();
    protected override void SubcribeEvent()
    {
        InputHandler.Instance.controls.Minigame.Submit.performed += OnSubmit;
    }
    protected override void UnsubcribeEvent()
    {
        InputHandler.Instance.controls.Minigame.Submit.performed -= OnSubmit;
    }
    public void OnSubmit(InputAction.CallbackContext ctx)
    {
        MinigameManager.Instance.OnSubmit();
    }
}
public class RythmMinigameState : MinigameState
{
    protected override void SubcribeEvent()
    {
        base.SubcribeEvent();
        InputHandler.Instance.controls.RhythmGame.Lane1.performed += OnLane1Performed;
        InputHandler.Instance.controls.RhythmGame.Lane2.performed += OnLane2Performed;
        InputHandler.Instance.controls.RhythmGame.Lane3.performed += OnLane3Performed;
        InputHandler.Instance.controls.RhythmGame.Lane4.performed += OnLane4Performed;
    }
    protected override InputActionMap GetSpecificActionMap() => InputHandler.Instance.controls.RhythmGame;

    protected override void UnsubcribeEvent()
    {
        base.UnsubcribeEvent();
        InputHandler.Instance.controls.RhythmGame.Lane1.performed -= OnLane1Performed;
        InputHandler.Instance.controls.RhythmGame.Lane2.performed -= OnLane2Performed;
        InputHandler.Instance.controls.RhythmGame.Lane3.performed -= OnLane3Performed;
        InputHandler.Instance.controls.RhythmGame.Lane4.performed -= OnLane4Performed;
    }

    public void OnLane1Performed(InputAction.CallbackContext ctx)
    {
        OnLanePerformed(0);
    }

    public void OnLane2Performed(InputAction.CallbackContext ctx)
    {
        OnLanePerformed(1);
    }

    public void OnLane3Performed(InputAction.CallbackContext ctx)
    {
        OnLanePerformed(2);
    }

    public void OnLane4Performed(InputAction.CallbackContext ctx)
    {
        OnLanePerformed(3);
    }

    void OnLanePerformed(int laneIndex)
    {
        JudgementManager.Instance.CheckHit(laneIndex);
    }
}
