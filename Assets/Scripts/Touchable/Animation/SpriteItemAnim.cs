using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(SpriteRenderer))]
public class SpriteItemAnim : MonoBehaviour
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

	public SpriteItemAnim StopAllAnims()
	{
		m_animPosX.Stop();
		m_animPosY.Stop();
		m_animScaleX.Stop();
		m_animScaleY.Stop();
		m_animAlpha.Stop();
		return this;
	}

	public SpriteItemAnim FinishAllAnims()
	{
		m_animPosX.Finish();
		m_animPosY.Finish();
		m_animScaleX.Finish();
		m_animScaleY.Finish();
		m_animAlpha.Finish();
		return this;
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

	protected float CurrentPosX {
		get => transform.position.x;
		set {
			var pos = transform.position;
			pos.x = value;
			transform.position = pos;
		}
	}
	protected float CurrentPosY {
		get => transform.position.y;
		set {
			var pos = transform.position;
			pos.y = value;
			transform.position = pos;
		}
	}
	protected float CurrentScaleX {

		get => transform.localScale.x;
		set {
			var pos = transform.localScale;
			pos.x = value;
			transform.localScale = pos;
		}
	}
	protected float CurrentScaleY {
		get => transform.localScale.y;
		set {
			var pos = transform.localScale;
			pos.y = value;
			transform.localScale = pos;
		}
	}
	protected float CurrentAlpha {
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


	public enum TargetType
	{
		PosX,
		PosY,
		ScaleX,
		ScaleY,
		Alpha,
	}

	public SpriteItemAnim StartValueAnim(TargetType tType, float endVal, float time, AnimationCurve curve = null)
	{
		if (tType == TargetType.PosX)
			m_animPosX.Start(CurrentPosX, endVal, time, curve);
		else if (tType == TargetType.PosY)
			m_animPosY.Start(CurrentPosY, endVal, time, curve);
		else if(tType == TargetType.ScaleX)
			m_animScaleX.Start(CurrentScaleX, endVal, time, curve);
		else if (tType == TargetType.ScaleY)
			m_animScaleY.Start(CurrentScaleY, endVal, time, curve);
		else if (tType == TargetType.Alpha)
			m_animAlpha.Start(CurrentAlpha, endVal, time, curve);
		return this;
	}

	public SpriteItemAnim SetValueImmedi(TargetType tType, float value)
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
