using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    using Touchable;

    public class WaterDropAnim : MonoBehaviour
    {
        [SerializeField]
        protected SpriteItemAnimData animData;

        [SerializeField, Min(0)]
        protected float dropTime = 1f;

        [SerializeField, Min(0)]
        protected float dropDist = 8f;


        protected IEnumerator m_curAnimCoroutine;

        public void StopCurAnimCoroutine()
        {
            if (m_curAnimCoroutine != null)
            {
                StopCoroutine(m_curAnimCoroutine);
                m_curAnimCoroutine = null;
            }
        }

        public void StartWaterDropAnim(Vector2 startPos)
        {
            StopCurAnimCoroutine();
            m_curAnimCoroutine = StartWaterDropAnim_Coroutine(startPos);
            StartCoroutine(m_curAnimCoroutine);
        }

        protected IEnumerator StartWaterDropAnim_Coroutine(Vector2 startPos)
        {
            ShowWaterDrop();
            animData
                .FinishAllAnims()
                .SetValueImmedi(TargetType.PosX, startPos.x)
                .SetValueImmedi(TargetType.PosY, startPos.y)
                .StartValueAnim(TargetType.PosY, startPos.y - dropDist, dropTime);
            yield return new WaitForSeconds(dropTime);
            HideAndReleaseWaterDrop();
        }


        public void ShowWaterDrop()
        {
            animData.SetValueImmedi(TargetType.Alpha, 1);
        }

        public void HideAndReleaseWaterDrop()
        {
            StopCurAnimCoroutine();
            animData.StopAllAnims();
            animData.SetValueImmedi(TargetType.Alpha, 0);
            if (Level3Logic.Inst)
                Level3Logic.Inst.ReleaseWaterDrop(this);
            else
                Destroy(this, 0.3f);
        }
    }
}
