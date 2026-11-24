using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Diary : Item
{
    // static bool isFirstTime = true;
    public int page = 0;
    [Header("Diary UI")]

    [SerializeField]
    CanvasGroup diaryUI;

    [SerializeField]
    TextMeshProUGUI leftPage;

    [SerializeField]
    TextMeshProUGUI rightPage;
    [Header("Pagination UI")]

    [SerializeField]
    GameObject pagination;

    [SerializeField]
    Image prevArrow;

    [SerializeField]
    Image nextArrow;
    [TextArea(3, 10)]

    [SerializeField]
    string[] diaryContent;

    void Start()
    {
        Close();
    }

    public override void Interact()
    {
        base.Interact();
        var actions = new List<(string, Action)> { ("Read", Open) };

        UIManager.Instance.ShowAction(actions, gameObject.transform.position);
    }

    void Open()
    {
        diaryUI.gameObject.SetActive(true);
        pagination.SetActive(true);
        Show(0);
        // if (isFirstTime)
        // {
        //     StartCoroutine(ShowThoughtAfterDelay("Day1.diary_page_1", 3f));
        //     isFirstTime = false;
        // }
        // else
            InputStateManager.Instance.SetState(new ReadDiaryState(this));
    }
    IEnumerator ShowThoughtAfterDelay(string pathName, float delay)
    {
        yield return new WaitForSeconds(delay);
        // DialogueManager.Instance.GoToPath(pathName, onFinish: () => NextPage());
    }

    void Show(int page)
    {
        if (page == 0)
            prevArrow.gameObject.SetActive(false);
        else
            prevArrow.gameObject.SetActive(true);

        if (page >= diaryContent.Length - 2)
            nextArrow.gameObject.SetActive(false);
        else
            nextArrow.gameObject.SetActive(true);

        leftPage.text = diaryContent[page];
        if (page + 1 < diaryContent.Length)
            rightPage.text = diaryContent[page + 1];
        else
            rightPage.text = "";
    }

    public void NextPage()
    {
        if (page < diaryContent.Length - 2)
        {
            page += 2;
        }
        Show(page);
    }

    public void PreviousPage()
    {
        if (page > 1)
        {
            page -= 2;
        }
        Show(page);
    }

    public void Close()
    {
        diaryUI.gameObject.SetActive(false);
        pagination.SetActive(false);
        page = 0;
    }
}
