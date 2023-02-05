using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
	using Touchable;

	public class HackIconAnim : MonoBehaviour
	{
		protected void Awake()
		{
			if (!spriteRenderer)
				spriteRenderer = GetComponent<SpriteRenderer>();
			ShowNormalSprite();
		}

		protected void Update()
		{
			UpdateHitAnim();
		}


		#region HitAnim

		[SerializeField]
		protected SpriteRenderer spriteRenderer;

		[SerializeField]
		protected Sprite normalSprite;
		[SerializeField]
		protected Sprite hitSprite;

		public void SetupNormalAndHitSprite(Sprite normalSprite, Sprite hitSprite)
		{
			this.normalSprite = normalSprite;
			this.hitSprite = hitSprite;

			ShowNormalSprite();
		}

		protected void ShowNormalSprite()
		{
			spriteRenderer.sprite = normalSprite;
		}

		protected void ShowHitSprite()
		{
			spriteRenderer.sprite = hitSprite;
		}


		[SerializeField]
		protected float hitStayTime = .5f;

		protected float hitStayRemainTime = 0;

		public void StartHitAnim()
		{
			hitStayRemainTime = hitStayTime;
			ShowHitSprite();
		}


		protected void UpdateHitAnim()
		{
			if (hitStayRemainTime > 0)
			{
				hitStayRemainTime -= Time.deltaTime;
				if (hitStayRemainTime <= 0)
					ShowNormalSprite();
			}
		}

		#endregion HitAnim


		protected readonly string waterDropTag = "WaterDrop";

		protected void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.CompareTag(waterDropTag))
			{
				var waterDrop = collision.GetComponent<WaterDropAnim>();
				if (waterDrop)
					OnHitHackIcon(waterDrop);
			}
		}

		protected void OnHitHackIcon(WaterDropAnim waterDrop)
		{
			Level3Logic.Inst.OnHitHackIcon(this, waterDrop);
		}


		[SerializeField]
		protected int totalHp = 3;

		protected int m_curHp = 0;

		public void ResetHp()
		{
			m_curHp = totalHp;
		}


		public void ProcessHit()
		{
			StartHitAnim();

			if (m_curHp > 0)
			{
				--m_curHp;
				if (m_curHp <= 0)
					OnHpReduceTo0();
			}
		}

		protected void OnHpReduceTo0()
		{
			StartFadeOutAnim();
			Level3Logic.Inst.OnDefeatHackIcon();
		}


		[SerializeField]
		protected SpriteItemAnimData animData;

		[SerializeField]
		protected float fadeOutTime = .8f;

		[SerializeField]
		protected float disableTriggerBeforeFadeTime = .5f;

		public void StartFadeOutAnim()
		{
			StartCoroutine(StartFadeOutAnim_Coroutine());
		}

		protected IEnumerator StartFadeOutAnim_Coroutine()
		{
			animData
				.FinishAllAnims()
				.SetValueImmedi(TargetType.Alpha, 1)
				.StartValueAnim(TargetType.Alpha, 0, fadeOutTime);
			yield return new WaitForSeconds(disableTriggerBeforeFadeTime);
			DisableTrigger();
		}

		protected void DisableTrigger()
		{
			var collider = GetComponent<Collider2D>();
			collider.enabled = false;
		}
	}
}
