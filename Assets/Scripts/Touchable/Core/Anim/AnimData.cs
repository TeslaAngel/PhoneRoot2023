using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Touchable
{
	public enum TargetType
	{
		PosX,
		PosY,
		ScaleX,
		ScaleY,
		Alpha,
	}

	public class AnimData
	{
		protected bool playing = false;

		protected float startVal = 0f;
		protected float endVal = 0f;
		protected float totalTime = 0;
		protected float duration = 0;
		protected AnimationCurve curve = null;

		protected bool curValCalced = false;
		protected float curVal = 0f;

		public bool Playing => playing;
		public bool Finished => !playing;

		public float CurrentVal {
			get {
				if (!curValCalced)
					curVal = Calculate();
				return curVal;
			}
		}

		public void Start(float startVal, float endVal, float totalTime, AnimationCurve curve = null)
		{
			Stop();

			playing = true;

			this.startVal = startVal;
			this.endVal = endVal;
			this.totalTime = totalTime;
			duration = totalTime;
			this.curve = curve;

			curValCalced = true;
			curVal = startVal;

			if (duration <= 0)
			{
				Finish();
			}
		}

		public void FinishAndStart(float startVal, float endVal, float totalTime, AnimationCurve curve = null)
		{
			Finish();
			Start(startVal, endVal, totalTime, curve);
		}

		public bool Update(float delta)
		{
			if (playing)
			{
				if (duration > 0)
					duration -= delta;
				curValCalced = false;
				if (duration <= 0)
					Finish();
				return true;
			}
			return false;
		}

		public void Finish()
		{
			Stop();

			duration = 0;
			//curve = null;

			curValCalced = true;
			curVal = endVal;
		}

		public void Stop()
		{
			if (playing)
			{
				playing = false;
			}
		}

		public float Calculate()
		{
			if (totalTime <= 0)
			{
				return endVal;
			}
			else if (curve != null)
			{
				return startVal + (endVal - startVal) * curve.Evaluate(1 - duration / totalTime);
			}
			else
			{
				return startVal + (endVal - startVal) * (1 - duration / totalTime);
			}
		}


		public delegate void AnimSetValueDelegate(AnimData animData);

		protected AnimSetValueDelegate m_animSetValMethod = null;

		public void SetAnimSetValMethod(AnimSetValueDelegate method)
		{
			m_animSetValMethod = method;
		}

		public void UpdateAndSetValue(float delta)
		{
			if (Update(delta))
				m_animSetValMethod?.Invoke(this);
		}
	}
	// ？指定速度

	// ？OnFinish
}
