using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    using Touchable;

    public class AppIconAnim : MonoBehaviour
    {
        [SerializeField]
        protected SpriteItemAnimData coverAnimData;


        public void ShowHover()
        {
            coverAnimData
                .FinishAllAnims()
                .SetValueImmedi(TargetType.Alpha, 1);
        }

        public void HideHover()
        {
            coverAnimData
                .FinishAllAnims()
                .SetValueImmedi(TargetType.Alpha, 0);
        }


        public void RequireNewPopupWindow()
        {
            Level2Logic.Inst.ShowNewPopupWindow();
        }
    }
}
