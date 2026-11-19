using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ObjectiveState
{
    public ObjectiveData data;
    public bool isCompleted = false;

    public ObjectiveState(ObjectiveData objectiveData)
    {
        data = objectiveData;
    }
}

public class ObjectiveManager : PersistentManager<ObjectiveManager>
{
    [SerializeField]
    List<ObjectiveState> objectiveDatas;
    Dictionary<string, ObjectiveState> objectiveMap;
    Dictionary<string, ObjectiveState> currentObjectives;

    [SerializeField]
    GameObject objectiveContainer;

    [SerializeField]
    GameObject objectivePrefab;
    List<Objective> objectiveUIs = new();

    void Awake()
    {
        objectiveMap = new Dictionary<string, ObjectiveState>();
        currentObjectives = new Dictionary<string, ObjectiveState>();
    }

    void OnEnable()
    {
        EventBus.Subcribe<LocationEnteredEvent>(OnLocationEntered);
        EventBus.Subcribe<ItemInteractedEvent>(OnItemInteracted);
    }

    void OnDisable()
    {
        EventBus.UnSubcribe<LocationEnteredEvent>(OnLocationEntered);
        EventBus.UnSubcribe<ItemInteractedEvent>(OnItemInteracted);
    }

    void Start()
    {
        foreach (ObjectiveState objective in objectiveDatas)
        {
            objectiveMap.Add(objective.data.id, objective);
        }
        foreach (var obj in objectiveUIs)
        {
            obj.gameObject.SetActive(false);
        }
    }

    public void CompleteObjective(string id)
    {
        if (currentObjectives.ContainsKey(id))
        {
            Objective completed = objectiveUIs.Find((obj) => obj.Data.id == id);
            completed.Complete();
            Destroy(completed.gameObject);
            currentObjectives[id].isCompleted = true;
            currentObjectives[id].data.Complete();
            currentObjectives.Remove(id);
            EventBus.Publish(new ObjectiveCompletedEvent(id));
        }
    }

    public void AddObjective(string id)
    {
        if (!currentObjectives.ContainsKey(id) && objectiveMap.ContainsKey(id))
        {
            ObjectiveState obj = objectiveMap[id];
            currentObjectives.Add(id, obj);
            GameObject objectiveGameObject = Instantiate(
                objectivePrefab,
                objectiveContainer.transform
            );
            Objective objective = objectiveGameObject.GetComponent<Objective>();
            objective.Display(obj.data);
            objectiveUIs.Add(objective);
        }
    }

    public void AddObjective(ObjectiveData data)
    {
        currentObjectives.Add(data.id, new ObjectiveState(data));
        GameObject objectiveGameObject = Instantiate(objectivePrefab, objectiveContainer.transform);
        Objective objective = objectiveGameObject.GetComponent<Objective>();
        objective.Display(data);
        objectiveUIs.Add(objective);
    }

    public bool IsObjectiveCompleted(string id)
    {
        return objectiveMap[id].isCompleted;
    }

    void OnLocationEntered(LocationEnteredEvent e)
    {
        foreach (var (id, objective) in objectiveMap)
        {
            if (
                objective.data.objectiveType == ObjectiveType.LocationEntered
                && objective.data.target.id == e.locationId
            )
            {
                CompleteObjective(id);
            }
        }
    }

    void OnItemInteracted(ItemInteractedEvent e)
    {
        foreach (var (id, objective) in objectiveMap)
        {
            if (
                objective.data.objectiveType == ObjectiveType.ItemInteracted
                && objective.data.target.id == e.itemId
            )
            {
                CompleteObjective(id);
            }
        }
    }
}
