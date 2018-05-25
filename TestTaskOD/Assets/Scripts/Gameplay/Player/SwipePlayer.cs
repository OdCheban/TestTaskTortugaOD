using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#if UNITY_ANDROID
namespace gameDream
{
    public class SwipePlayer : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        private EnginePlayer player;

        float time;
        void Start()
        {
            player = GameObject.Find("RollerBall").GetComponent<EnginePlayer>();
        }
        void Update()
        {
            if (Input.touchCount >= 1)
            {
                if (Input.touches[0].phase == TouchPhase.Stationary) 
                    time += Time.deltaTime;
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    if (time < 0.15f)
                        player.Action("Forward");
                    time = 0;
                }
            }
            else
            {
                player.Action("Idle");
            }
        }

        public void OnDrag(PointerEventData eventData) {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.delta.x > 0)
                player.Action("Right");
            else
                player.Action("Left");
        }
    }
}
#endif