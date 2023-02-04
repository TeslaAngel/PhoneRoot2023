using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
	using Touchable;

	[RequireComponent(typeof(SpriteItemAnimData))]
	public class NumpadAnim : MonoBehaviour
	{
		protected SpriteItemAnimData m_itemAnimData;

		protected SpriteItemAnimData TouchItemAnim()
		{
			if (!m_itemAnimData)
				m_itemAnimData = GetComponent<SpriteItemAnimData>();
			return m_itemAnimData;
		}


		public Vector2 startScale = new Vector2(.5f, .5f);
		public Vector2 stayScale = new Vector2(1, 1);
		public Vector2 endScale = new Vector2(2, 2);
		[Min(0)]
		public float fadeInTime = .05f;
		[Min(0)]
		public float fadeOutTime = .3f;

		public void AnimShow()
		{
			var itemAnim = TouchItemAnim();
			if (itemAnim.HasAnimPlaying())
				return;
			itemAnim
				.FinishAllAnims()
				.SetValueImmedi(TargetType.ScaleX, startScale.x)
				.StartValueAnim(TargetType.ScaleX, stayScale.x, fadeInTime)
				.SetValueImmedi(TargetType.ScaleY, startScale.y)
				.StartValueAnim(TargetType.ScaleY, stayScale.y, fadeInTime)
				.SetValueImmedi(TargetType.Alpha, 1);
		}

		public void AnimHide()
		{
			var itemAnim = TouchItemAnim();
			if (itemAnim.HasAnimPlaying())
				return;
			itemAnim
				.FinishAllAnims()
				.SetValueImmedi(TargetType.ScaleX, stayScale.x)
				.StartValueAnim(TargetType.ScaleX, endScale.x, fadeOutTime)
				.SetValueImmedi(TargetType.ScaleY, stayScale.y)
				.StartValueAnim(TargetType.ScaleY, endScale.y, fadeOutTime)
				.SetValueImmedi(TargetType.Alpha, 1)
				.StartValueAnim(TargetType.Alpha, 0, fadeOutTime);
		}

		public void AnimLeftRight()
		{
			StartCoroutine(AnimLeftRight_Coroutine());
		}


		[Min(0)]
		public float leftRightTime = .1f;
		[Min(0)]
		public float leftRightLength = 1f;

		public IEnumerator AnimLeftRight_Coroutine()
		{
			var itemAnim = TouchItemAnim();
			if (!itemAnim.HasAnimPlaying())
			{
				itemAnim
					.FinishAllAnims()
					.StartValueAnim(TargetType.PosX, itemAnim.CurrentPosX + leftRightLength, leftRightTime);
				yield return new WaitForSeconds(leftRightTime);
				itemAnim
					.FinishAllAnims()
					.StartValueAnim(TargetType.PosX, itemAnim.CurrentPosX - leftRightLength * 2, leftRightTime * 2);
				yield return new WaitForSeconds(leftRightTime * 2);
				itemAnim
					.FinishAllAnims()
					.StartValueAnim(TargetType.PosX, itemAnim.CurrentPosX + leftRightLength * 2, leftRightTime * 2);
				yield return new WaitForSeconds(leftRightTime * 2);
				itemAnim
					.FinishAllAnims()
					.StartValueAnim(TargetType.PosX, itemAnim.CurrentPosX - leftRightLength, leftRightTime);
			}
			// ¿ÉÄÜÖÐÍ¾ Finish
			// TODO : ...
		}
	}
}
