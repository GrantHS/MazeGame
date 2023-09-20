using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIBlocker : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
       {
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           RaycastHit hit;
           if (Physics.Raycast(ray, out hit))
           {
               if (!IsPointerOverUIObject()) //this is to check if the player clicks behind the ui
               {
                   Debug.Log("you should not be clicking behind the ui");
               }
           }
       }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDatacurrentPosition = new PointerEventData(EventSystem.current);
        eventDatacurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDatacurrentPosition, results);
        return results.Count > 0;
    }
}
