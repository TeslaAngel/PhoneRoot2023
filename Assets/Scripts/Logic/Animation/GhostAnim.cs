using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
	using Touchable;

	[RequireComponent(typeof(SpriteRenderer))]
	public class GhostAnim : MonoBehaviour
	{
		protected void Awake()
		{
			m_renderer = GetComponent<SpriteRenderer>();

			m_currentFrame = 0;
			m_animCurWait = 0;
			RefreshSprite();
		}

		protected void Update()
		{
			UpdateAnim();
		}


		protected SpriteRenderer m_renderer;

		[SerializeField]
		protected List<Sprite> animFrames = new List<Sprite>();


		protected bool animEnabled = false;

		public void SetAnimEnabled(bool enabled)
		{
			animEnabled = enabled;
			if (!animEnabled)
			{
				m_animCurWait = 0;
			}
		}


		[SerializeField, Min(0)]
		protected float frameAnimSpeed = 0.5f;

		protected int m_currentFrame = 0;
		protected float m_animCurWait = 0;

		public void SetCurFrame(int frame)
		{
			if (0 <= frame && frame < animFrames.Count)
				m_currentFrame = frame;
			else
				m_currentFrame = 0;
			RefreshSprite();
		}

		protected void UpdateAnim()
		{
			if (animEnabled)
			{
				m_animCurWait += Time.deltaTime;
				if (m_animCurWait >= frameAnimSpeed)
				{
					SetCurFrame((m_currentFrame + 1) % animFrames.Count);
					m_animCurWait = 0;
				}
			}
		}

		protected void RefreshSprite()
		{
			m_renderer.sprite = animFrames[m_currentFrame];
		}


		[SerializeField]
		protected SpriteItemAnimData ghostAnimData;

		public void StartMoveXAnim(float dist, float time)
		{
			ghostAnimData
				.FinishAllAnims()
				.StartValueAnim(TargetType.PosX, ghostAnimData.CurrentPosX + dist, time);
		}
	}
}