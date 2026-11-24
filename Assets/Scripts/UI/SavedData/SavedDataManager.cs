using System.Collections.Generic;
using UnityEngine;

public class SavedDataManager : MonoBehaviour
{
    SavedDataSlot[] slots;
    public static DataAction currentAction = DataAction.Load;

    public enum DataAction
    {
        Save,
        Load,
    }

    void Awake()
    {
        slots = GetComponentsInChildren<SavedDataSlot>();
    }

    void Start()
    {

        BinarySaveSystem.Load();
        if (BinarySaveSystem.saveDatas != null)
        {
            for (int i = 0; i < slots.Length && i < BinarySaveSystem.saveDatas.Length; i++)
            {
                slots[i].SetData(BinarySaveSystem.saveDatas[i]);
                slots[i].gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update() { }
}
