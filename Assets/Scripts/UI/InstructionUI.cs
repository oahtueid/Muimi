using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstructionUI : MonoBehaviour
{
    public int page = 0;
    public List<Instruction> instructions;

    [Header("Instruction UI")]
    [SerializeField]
    GameObject instructionUI;

    [SerializeField]
    TextMeshProUGUI contentText;
    
    [SerializeField]
    Image guideImg;

    [Header("Pagination UI")]
    [SerializeField]
    GameObject pagination;

    [SerializeField]
    Image prevArrow;

    [SerializeField]
    Image nextArrow;

    public Action OnClosed;
    void Start()
    {
        Close();
    }

    public void Open()
    {
        instructionUI.SetActive(true);
        pagination.SetActive(true);
        Show(0);
    }

    void Show(int page)
    {
        if (page == 0)
            prevArrow.gameObject.SetActive(false);
        else
            prevArrow.gameObject.SetActive(true);

        if (page >= instructions.Count - 1)
            nextArrow.gameObject.SetActive(false);
        else
            nextArrow.gameObject.SetActive(true);

        contentText.text = instructions[page].description;
        guideImg.sprite = instructions[page].guideImg.sprite;
    }

    public void NextPage()
    {
        if (page < instructions.Count - 1)
        {
            page += 1;
            Show(page);
        }else Close();
    }

    public void PreviousPage()
    {
        if (page > 0)
        {
            page -= 1;
        }
        Show(page);
    }

    public void Close()
    {
        instructionUI.SetActive(false);
        pagination.SetActive(false);
        page = 0;
        OnClosed?.Invoke();
    }
}
