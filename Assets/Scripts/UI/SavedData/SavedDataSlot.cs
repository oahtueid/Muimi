using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SavedDataSlot : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI savedTime;

    [SerializeField]
    TextMeshProUGUI savedStory;

    SaveData data;

    void Start()
    {
        if (data != null)
        {
            savedTime.text = data.saveTime.ToLongTimeString();
        }
        else
        {
            savedTime.text = "No data saved";
        }
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void SetData(SaveData data)
    {
        this.data = data;
    }

    void OnClick()
    {
        int index = transform.GetSiblingIndex();
        if (SavedDataManager.currentAction == SavedDataManager.DataAction.Save)
            GameManager.Instance.Save(index);
        else
            GameManager.Instance.Load(index);
    }
}
