using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class PlayerInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public static event Action<Vector2, Vector2> OnSwipe;
        
        private Vector3 fp; //First touch position
        private Vector3 lp; //Last touch position
        

        public void OnPointerDown(PointerEventData eventData)
        {
           fp =eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            lp = eventData.position;

            fp = Camera.main.ScreenToWorldPoint(fp);
            lp = Camera.main.ScreenToWorldPoint(lp);
            
            OnSwipe?.Invoke(fp, lp);
            
            Debug.Log(fp + " " + lp);
        }
    }
}