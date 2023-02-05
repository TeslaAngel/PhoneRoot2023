using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    using Touchable;

    public class ProcessBarLv5Anim : MonoBehaviour
    {
        protected SpriteRenderer m_renderer;

		protected void Awake()
		{
            m_renderer = GetComponent<SpriteRenderer>();
        }


		[SerializeField]
        protected TouchableItem procBarItem;

        [SerializeField]
        protected Vector2 waterDropGeneratePos;

        [SerializeField, Min(0)]
        protected float dragThreshold = 0f;

        public void OnPressEnd()
        {
            if (procBarItem.PressingDragOffset.magnitude <= dragThreshold)
            {
                MakeWaterDrop();
            }
        }

        protected void MakeWaterDrop()
        {
            var waterDrop = Level3Logic.Inst.GetWaterDrop();
            waterDrop.StartWaterDropAnim((Vector2)transform.position + waterDropGeneratePos);
        }


        [SerializeField]
        protected List<Sprite> processBarSprites;

        public void SetProceeBarSprite(int index)
        {
            if (0 <= index && index < processBarSprites.Count)
                m_renderer.sprite = processBarSprites[index];
        }


        /*[SerializeField]
        protected SpriteItemAnimData animData;

        [SerializeField]
        protected float dropTime = 1f;
        [SerializeField]
        protected float dropDist = 8f;

        public void StartDropAnim()
        {
            var collider = GetComponent<Collider2D>();
            collider.enabled = false;

            animData
                .FinishAllAnims()
                //.StartValueAnim(TargetType.PosY, animData.CurrentPosY - dropDist, dropTime);
                .StartValueAnim(TargetType.PosY, transform.position.y - dropDist, dropTime);
        }*/

        [SerializeField]
        protected Rigidbody2D procBarPhy;
        [SerializeField]
        protected float dropTime = 2f;

        public void StartDropAnim()
        {
            var collider = GetComponent<Collider2D>();
            collider.enabled = false;

            m_renderer.enabled = false;

            StartCoroutine(StartDropAnim_Coroutine());
        }

        protected IEnumerator StartDropAnim_Coroutine()
        {
            procBarPhy.transform.position = transform.position;
            procBarPhy.gameObject.SetActive(true);
            yield return new WaitForSeconds(dropTime);
            procBarPhy.gameObject.SetActive(false);
        }
    }
}
