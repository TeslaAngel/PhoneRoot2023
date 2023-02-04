using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    using Touchable;

    public class PopupAnim : MonoBehaviour
    {
        //[SerializeField]
        protected int m_popupWindowStyleId = 0;
        public int PopupWindowStyleId {
            get => m_popupWindowStyleId;
            set {
                m_popupWindowStyleId = value;
            }
        }


        [SerializeField]
        protected SpriteItemAnimData mainAnimData;
        [SerializeField]
        protected SpriteItemAnimData closeBtnAnimData;


        protected bool m_pressExited = false;

        public void CloseBtnHoldPressExit()
        {
            m_pressExited = true;
        }

        public void CloseBtnHold()
        {
            m_pressExited = false;
            closeBtnAnimData
                .FinishAllAnims()
                .SetValueImmedi(TargetType.Alpha, 1);
        }

        public void CloseBtnRelease()
        {
            closeBtnAnimData
                .FinishAllAnims()
                .SetValueImmedi(TargetType.Alpha, 0);
            if (!m_pressExited)
            {
                ClosePopupWindow();
                // �����ٵ���һ������
                Level2Logic.Inst.ShowNewPopupWindow();
            }
        }


        [SerializeField]
        protected Vector2 popupDefScale = new Vector2(1, 1);
        [SerializeField]
        protected float openPopupTime = .2f;
        [SerializeField]
        protected float closePopupTime = .2f;

        /*public void ClosePopupWindow()
        {
            mainAnimData
                .FinishAllAnims()
                .SetValueImmedi(TargetType.ScaleX, popupDefScale.x)
                .SetValueImmedi(TargetType.ScaleY, popupDefScale.y)
                .StartValueAnim(TargetType.ScaleX, 0, closePopupTime)
                .StartValueAnim(TargetType.ScaleY, 0, closePopupTime);

            // �ݲ����գ�ֱ������
            Destroy(this, closePopupTime + 0.01f);
        }*/

        public void ClosePopupWindow()
        {
            StartCoroutine(ClosePopupWindow_Coroutine());
        }
        public IEnumerator ClosePopupWindow_Coroutine()
        {
            mainAnimData
                .FinishAllAnims()
                .SetValueImmedi(TargetType.ScaleX, popupDefScale.x)
                .SetValueImmedi(TargetType.ScaleY, popupDefScale.y)
                .StartValueAnim(TargetType.ScaleX, 0, closePopupTime)
                .StartValueAnim(TargetType.ScaleY, 0, closePopupTime);
            yield return new WaitForSeconds(closePopupTime);
            gameObject.SetActive(false);
            // ��ȴ�һ���¼����ͷ�
            yield return new WaitForSeconds(1f);
            Level2Logic.Inst.ReleasePopupWindow(this);
        }


        public void ShowPopupWindow()
        {
            mainAnimData
                .FinishAllAnims()
                .SetValueImmedi(TargetType.ScaleX, 0)
                .SetValueImmedi(TargetType.ScaleY, 0)
                .StartValueAnim(TargetType.ScaleX, popupDefScale.x, openPopupTime)
                .StartValueAnim(TargetType.ScaleY, popupDefScale.y, openPopupTime);

            // ...
        }

        [SerializeField]
        protected Rect randomPosRange = new Rect(-2, -3, 2, 3);

        public void SetRandomPosition()
        {
            var range = randomPosRange;
            var pos = new Vector3(
                Random.Range(range.x, range.width),
                Random.Range(range.y, range.height),
                transform.position.z);
            transform.position = pos;
        }


        public void OnGotFocus()
        {
            // �����ö�
            SetLayerOrderZ(Level2Logic.Inst.GetPopupWindowTopZ());
        }


        [SerializeField]
        protected SpriteRenderer bgRenderer;
        //[SerializeField]
        //protected SpriteRenderer barRenderer;
        [SerializeField]
        protected SpriteRenderer closeBtnRenderer;

        /*[SerializeField]
        protected Sprite bgSprite;
        [SerializeField]
        protected Sprite closeBtnSprite;
        [SerializeField]
        protected Vector2 closeBtnOffset;

        protected void Awake()
		{
		}*/

		public void SetLayerOrderZ(int z)
        {
            if (bgRenderer)
                bgRenderer.sortingOrder = z + 0;
            //if (barRenderer)
            //    barRenderer.sortingOrder = z + 1;
            if (closeBtnRenderer)
                closeBtnRenderer.sortingOrder = z + 2;

            var pos = transform.position;
            pos.z = 15 + z;
            transform.position = pos;
        }
    }
}