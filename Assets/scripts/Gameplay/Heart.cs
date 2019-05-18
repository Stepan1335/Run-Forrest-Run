using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : IntEventInvoker
{
    // Start is called before the first frame update
    void Start()
    {
        //events
        unityEvents.Add(EventName.LivesChangeEvent, new LivesChangeEvent());
        EventManager.AddInvoker(EventName.LivesChangeEvent, this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject coll = collision.gameObject;

        if (coll.CompareTag("Forrest"))
        {
            AudioManager.Play(AudioClipName.HealthPickup);
            unityEvents[EventName.LivesChangeEvent].Invoke(1); // Add one life if player get this item
            Destroy(gameObject);
        }
    }
}
