using UnityEngine;

public class TravelList : MonoBehaviour
{
    public Transform List;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var location in Database.Locations)
        {
            GameObject questObject = new GameObject();
            questObject.transform.parent = List;
            questObject.AddComponent<TravelListItem>();
            questObject.GetComponent<TravelListItem>().SetLocation(location);

            if (PlayerState.instance.CurrentLocation == location)
            {
                questObject.GetComponent<TravelListItem>().Disable();
            }
        }
    }

    public void OnLocationChanged()
    {
        foreach (Transform child in List)
        {
            TravelListItem listItem = child.gameObject.GetComponent<TravelListItem>();
            if (listItem.GetLocation() == PlayerState.instance.CurrentLocation)
            {
                listItem.Disable();
            } else {
                listItem.Enable();
            }
        }
    }
}
