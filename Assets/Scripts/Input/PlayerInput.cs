using System;
using Game.Board;
using Game.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Input
{
    public class PlayerInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public static event Action<Vector2, Vector2> OnSwipe;

        private Vector3 fp; //First touch position
        private Vector3 lp; //Last touch position


        public void OnPointerDown(PointerEventData eventData)
        {
            fp = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            lp = eventData.position;
            //todo:resize yapınca bozulmalar oluyor yeniden bakılacak
            var mainCamera = CameraUtils.MainCamera;
            fp = mainCamera.ScreenToWorldPoint(fp);
            lp = mainCamera.ScreenToWorldPoint(lp);
            var checkDistance = Vector3.Distance(fp, lp);
            
            if (Math.Round(checkDistance) <= BoardController.Instance.CellSize)
            {
                OnSwipe?.Invoke(fp, lp);
                Debug.Log($"{fp} {lp}");
            }
        }
    }
}