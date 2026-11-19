using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : PersistentManager<MenuManager>
{
    Stack<MenuUI> menuStack = new();

    [SerializeField]
    MenuUI mainMenu;

    [SerializeField]
    MenuUI dataMenu;

    // [SerializeField]
    // GameObject optionMenu;

    [SerializeField]
    ConfirmPanel confirmPanel;

    void Start() { }

    public void Continue()
    {
        while (menuStack.Count > 0)
        {
            MenuUI ui = menuStack.Pop();
            ui.Close();
        }
        InputStateManager.Instance.PopState();
    }

    public void Open(MenuUI ui)
    {
        if (menuStack.Count > 0)
        {
            MenuUI parent = menuStack.Peek();
            parent.LastSelected = EventSystem.current.currentSelectedGameObject;
            if (ui.type == UIType.Screen)
            {
                parent.Close();
            }
            else if (ui.type == UIType.Popup)
            {
                parent.canvasGroup.interactable = false;
            }
        }
        menuStack.Push(ui);
        ui.Open();
    }

    public void GoBack()
    {
        if (menuStack.Count == 0)
        {
            InputStateManager.Instance.PopState();
            return;
        }
        ;
        MenuUI current = menuStack.Pop();
        current.Close();
        if (menuStack.Count > 0)
        {
            Debug.Log("open: " + menuStack.Peek().gameObject.name);
            menuStack.Peek().Open();
        }
    }

    void OpenDataMenu()
    {
        Open(dataMenu);
    }

    public void Save()
    {
        OpenDataMenu();
        SavedDataManager.currentAction = SavedDataManager.DataAction.Save;
    }

    public void Load()
    {
        OpenDataMenu();
        SavedDataManager.currentAction = SavedDataManager.DataAction.Load;
    }

    // public void Config()
    // {
    //     optionMenu.SetActive(true);
    //     mainMenu.SetActive(false);
    //     currentOpenSubMenu = optionMenu;
    //     EventSystem.current.SetSelectedGameObject(optionMenuFirst);
    // }

    public void BackToMenu()
    {
        Open(confirmPanel);
        ShowConfirmPanel(
            "Are you sure you want to quit",
            () =>
            {
                SceneManager.LoadScene("MainMenu");
            }
        );
    }

    public void Quit()
    {
        Open(confirmPanel);
        ShowConfirmPanel(
            "Are you sure you want to quit",
            () =>
            {
                Application.Quit();
            }
        );
    }

    public void HandleOpenCloseMenu()
    {
        if (menuStack.Count == 0)
        {
            Open(mainMenu);
            InputStateManager.Instance.PushState(new UIChoiceState());
        }
        else
            GoBack();
    }

    void ShowConfirmPanel(string content, UnityAction action)
    {
        confirmPanel.gameObject.SetActive(true);
        confirmPanel.Display(content, action);
    }
}
