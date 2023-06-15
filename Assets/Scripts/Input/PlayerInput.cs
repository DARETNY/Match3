using System;
using CameraHandle;
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

            var mainCamera = CameraUtils.MainCamera;
            fp = mainCamera.ScreenToWorldPoint(fp);
            lp = mainCamera.ScreenToWorldPoint(lp);
            
            OnSwipe?.Invoke(fp, lp);
        }
    }
}
