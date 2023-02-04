using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    using Touchable;

    public class WechatIconAnim : MonoBehaviour
    {
        [SerializeField]
        protected SpriteItemAnimData cage1AnimData;
        [SerializeField]
        protected SpriteItemAnimData cage2AnimData;


        protected float m_cage1BaseY = 0;
        protected float m_cage2BaseY = 0;

		protected void Awake()
		{
            m_cage1BaseY = cage1AnimData.transform.position.y;
            m_cage2BaseY = cage2AnimData.transform.position.y;
        }


		[SerializeField]
        protected float downDist = 0.2f;
        [SerializeField]
        protected float upDist = 0.2f;
        [SerializeField]
        protected float fadeInTime = 0.2f;
        [SerializeField]
        protected float waitTime = 0.05f;
        [SerializeField]
        protected float moveTime = 0.2f;


        public void LockCage()
        {
            StartCoroutine(LockCage_Coroutine());
        }
        protected IEnumerator LockCage_Coroutine()
        {
            cage1AnimData
                .FinishAllAnims()
                .SetValueImmedi(TargetType.PosY, m_cage1BaseY + downDist)
                .SetValueImmedi(TargetType.Alpha, 0)
                .StartValueAnim(TargetType.Alpha, 1, fadeInTime);
            cage2AnimData
                .FinishAllAnims()
                .SetValueImmedi(TargetType.PosY, m_cage2BaseY - upDist)
                .SetValueImmedi(TargetType.Alpha, 0)
                .StartValueAnim(TargetType.Alpha, 1, fadeInTime);

            yield return new WaitForSeconds(fadeInTime);
            yield return new WaitForSeconds(waitTime);

            cage1AnimData
                .FinishAllAnims()
                .SetValueImmedi(TargetType.PosY, m_cage1BaseY + downDist)
                .StartValueAnim(TargetType.PosY, m_cage1BaseY, moveTime);
            cage2AnimData
                .FinishAllAnims()
                .SetValueImmedi(TargetType.PosY, m_cage2BaseY - upDist)
                .StartValueAnim(TargetType.PosY, m_cage2BaseY, moveTime);
        }
    }
}
