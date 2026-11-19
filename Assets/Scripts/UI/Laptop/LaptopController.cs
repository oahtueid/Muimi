using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//TODO: effect when open file/folder
public class LaptopController : MonoBehaviour
{
    public static bool IsUnlocked = false;

    [SerializeField]
    string laptopPassword = "123456";

    [SerializeField]
    GameObject logInScreen;

    [SerializeField]
    GameObject loggedInScreen;

    void Start()
    {
        Open();
    }
    public void Open()
    {
        if (!IsUnlocked)
        {
            Logout();
        }
        else
        {
            Login();
        }
    }

    public void GuessPassword(string password)
    {
        if (password == laptopPassword)
        {
            IsUnlocked = true;
            Login();
        }
        else
        {
            Debug.Log("Wrong pass!");
            // TODO: red border or shake input when wrong
        }
    }

    void Login()
    {
        loggedInScreen.SetActive(true);
        logInScreen.SetActive(false);
    }

    void Logout()
    {
        logInScreen.SetActive(true);
        loggedInScreen.SetActive(false);
    }
}
