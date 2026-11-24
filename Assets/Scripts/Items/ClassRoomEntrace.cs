using UnityEngine;

public class ClassRoomEntrance : Entrance
{
    void Start()
    {
        // EventManager.Instance.RegisterEvent(
        //     Location.School,
        //     new Exercise(GameManager.Instance.GetExamOnDay(TimeManager.Instance.CurrentDay))
        // );
    }

    // public void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         EventManager.Instance.RunNextEvent(Location.School);
    //     }
    // }
}
