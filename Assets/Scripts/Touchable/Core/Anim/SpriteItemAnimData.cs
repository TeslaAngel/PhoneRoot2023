using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Touchable
{
	//[RequireComponent(typeof(SpriteRenderer))]
	public class SpriteItemAnimData : MonoBehaviour
	{
		protected SpriteRenderer m_renderer;

		protected void Awake()
		{
			InitAllAnims();
			m_renderer = GetComponent<SpriteRenderer>();
		}

		protected void Update()
		{
			UpdateAllAnims(Time.deltaTime);
		}


		protected AnimData m_animPosX = new AnimData();
		protected AnimData m_animPosY = new AnimData();
		protected AnimData m_animScaleX = new AnimData();
		protected AnimData m_animScaleY = new AnimData();
		protected AnimData m_animAlpha = new AnimData();

		protected void InitAllAnims()
		{
			m_animPosX.SetAnimSetValMethod(AnimSetPosX);
			m_animPosY.SetAnimSetValMethod(AnimSetPosY);
			m_animScaleX.SetAnimSetValMethod(AnimSetPosScaleX);
			m_animScaleY.SetAnimSetValMethod(AnimSetPosScaleY);
			m_animAlpha.SetAnimSetValMethod(AnimSetPosAlpha);
		}

		protected void UpdateAllAnims(float delta)
		{
			m_animPosX.UpdateAndSetValue(delta);
			m_animPosY.UpdateAndSetValue(delta);
			m_animScaleX.UpdateAndSetValue(delta);
			m_animScaleY.UpdateAndSetValue(delta);
			m_animAlpha.UpdateAndSetValue(delta);
		}

		public SpriteItemAnimData StopAllAnims()
		{
			m_animPosX.Stop();
			m_animPosY.Stop();
			m_animScaleX.Stop();
			m_animScaleY.Stop();
			m_animAlpha.Stop();
			return this;
		}

		public SpriteItemAnimData FinishAllAnims()
		{
			m_animPosX.Finish();
			m_animPosY.Finish();
			m_animScaleX.Finish();
			m_animScaleY.Finish();
			m_animAlpha.Finish();
			return this;
		}

		public bool HasAnimPlaying()
		{
			return m_animPosX.Playing
				|| m_animPosY.Playing
				|| m_animScaleX.Playing
				|| m_animScaleY.Playing
				|| m_animAlpha.Playing;
		}


		protected void AnimSetPosX(AnimData animData)
		{
			CurrentPosX = animData.CurrentVal;
		}
		protected void AnimSetPosY(AnimData animData)
		{
			CurrentPosY = animData.CurrentVal;
		}
		protected void AnimSetPosScaleX(AnimData animData)
		{
			CurrentScaleX = animData.CurrentVal;
		}
		protected void AnimSetPosScaleY(AnimData animData)
		{
			CurrentScaleY = animData.CurrentVal;
		}
		protected void AnimSetPosAlpha(AnimData animData)
		{
			CurrentAlpha = animData.CurrentVal;
		}

		public float CurrentPosX {
			get => transform.position.x;
			set {
				var pos = transform.position;
				pos.x = value;
				transform.position = pos;
				// £¿localPosition
			}
		}
		public float CurrentPosY {
			get => transform.position.y;
			set {
				var pos = transform.position;
				pos.y = value;
				transform.position = pos;
				// £¿localPosition
			}
		}
		public float CurrentScaleX {

			get => transform.localScale.x;
			set {
				var pos = transform.localScale;
				pos.x = value;
				transform.localScale = pos;
			}
		}
		public float CurrentScaleY {
			get => transform.localScale.y;
			set {
				var pos = transform.localScale;
				pos.y = value;
				transform.localScale = pos;
			}
		}
		public float CurrentAlpha {
			get => m_renderer ? m_renderer.color.a : 0;
			set {
				if (m_renderer)
				{
					var color = m_renderer.color;
					color.a = value;
					m_renderer.color = color;
				}
			}
		}


		public SpriteItemAnimData StartValueAnim(TargetType tType, float endVal, float time, AnimationCurve curve = null)
		{
			if (tType == TargetType.PosX)
				m_animPosX.Start(CurrentPosX, endVal, time, curve);
			else if (tType == TargetType.PosY)
				m_animPosY.Start(CurrentPosY, endVal, time, curve);
			else if (tType == TargetType.ScaleX)
				m_animScaleX.Start(CurrentScaleX, endVal, time, curve);
			else if (tType == TargetType.ScaleY)
				m_animScaleY.Start(CurrentScaleY, endVal, time, curve);
			else if (tType == TargetType.Alpha)
				m_animAlpha.Start(CurrentAlpha, endVal, time, curve);
			return this;
		}

		public SpriteItemAnimData SetValueImmedi(TargetType tType, float value)
		{
			if (tType == TargetType.PosX)
				CurrentPosX = value;
			else if (tType == TargetType.PosY)
				CurrentPosY = value;
			else if (tType == TargetType.ScaleX)
				CurrentScaleX = value;
			else if (tType == TargetType.ScaleY)
				CurrentScaleY = value;
			else if (tType == TargetType.Alpha)
				CurrentAlpha = value;
			return this;
		}
	}
}
