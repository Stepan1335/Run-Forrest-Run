using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : IntEventInvoker
{
    int points = 20;

    // Start is called before the first frame update
    void Start()
    {
        //events
        unityEvents.Add(EventName.PointsAddedEvent, new PointsAddedEvent());
        EventManager.AddInvoker(EventName.PointsAddedEvent, this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject coll = collision.gameObject;

        if (coll.CompareTag("Forrest"))
        {
            AudioManager.Play(AudioClipName.GenericPickup);
            unityEvents[EventName.PointsAddedEvent].Invoke(points);
            Destroy(gameObject);
        }
    }

}
