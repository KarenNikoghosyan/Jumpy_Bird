using System.Collections.Generic;
using UnityEngine;

namespace MText
{
    [DisallowMultipleComponent]
    public class MText_UI_Toggle : MonoBehaviour
    {
        public bool active = true;
        public List<GameObject> activeGraphic = new List<GameObject>();
        public List<GameObject> inactiveGraphic = new List<GameObject>();

        /// <summary> 
        /// Sets the activate state according to the parameter passed.
        /// </summary>
        public void Set(bool set)
        {
            active = set;
            VisualUpdate();
        }
        /// <summary> 
        /// Switches between on and off.
        /// </summary>
        public void Toggle()
        {
            active = !active;
            VisualUpdate();
        } 

        private void VisualUpdate()
        {
            if (active) ActiveVisualUpdate();
            else InactiveVisualUpdate();
        }

        /// <summary> 
        /// Changes the graphic to activated.
        /// <para>This only changes the visual. Doesn't update the "active" bool</para>
        /// </summary>
        public void ActiveVisualUpdate()
        {
            ToggleGraphic(inactiveGraphic, false);
            ToggleGraphic(activeGraphic, true);
        }


        /// <summary> 
        /// Changes the graphic to activated.
        /// <para>This only changes the visual. Doesn't update the "active" bool</para>
        /// </summary>
        public void InactiveVisualUpdate()
        {
            ToggleGraphic(inactiveGraphic, true);
            ToggleGraphic(activeGraphic, false);
        }

        private void ToggleGraphic(List<GameObject> list, bool enable)
        {
            for (int i = 0; i < list.Count; i++)
            {
#if UNITY_EDITOR
                //If user forgot to attach graphic. Notify them
                if (!list[i])
                {
                    string listName = !enable ? "inActive" : "Active";
                    Debug.Log(gameObject + " has a missing graphic in it's " + listName + "graphic list. Item number :" + i + ".", gameObject);
                }
#endif

                list[i].SetActive(enable);

            }
        }


        /// <summary> 
        /// Editor only. Adds the toggle event to attached button.
        /// <para>Used by menu item when creating a toggle gameobject</para>
        /// </summary>
#if UNITY_EDITOR
        public void AddEventToButton()
        {
            UnityEditor.Events.UnityEventTools.AddPersistentListener(GetComponent<MText_UI_Button>().onClickEvents, delegate { Toggle(); });
        }
#endif
    }
}