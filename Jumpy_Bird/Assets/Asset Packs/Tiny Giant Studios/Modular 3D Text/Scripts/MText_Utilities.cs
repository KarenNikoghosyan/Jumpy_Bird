using UnityEngine;

namespace MText
{   
    /// <summary>
    /// This class contains different reusable methods for the asset
    /// </summary>
    public class MText_Utilities
    {
        public static MText_UI_List GetParentList(Transform transform)
        {
            if (transform.parent?.GetComponent<MText_UI_List>())
            {
                return transform.parent.GetComponent<MText_UI_List>();
            }
            else return null;
        }
    }
}