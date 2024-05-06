using UnityEngine;

public class EventTitles : MonoBehaviour
{
    public void Run(string name)
    {
        transform.Find(name).GetComponent<EventTitle>().Run();
    }
}
