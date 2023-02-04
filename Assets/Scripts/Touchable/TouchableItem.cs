using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TouchableItem : MonoBehaviour
{
	protected bool m_touchEventEnabled = true;
	public bool TouchEventEnabled => m_touchEventEnabled;

	public void SetTouchEventEnabled(bool enabled)
	{
		m_touchEventEnabled = enabled;
	}


	[SerializeField]
	protected bool allowDragInClick = false;
	public bool AllowDragInClick => allowDragInClick;


	public UnityEvent onPressStartEvent;
	public UnityEvent onPressHoldEvent;
	public UnityEvent onPressEndEvent;

	protected bool m_pressing = false;
	protected Vector2 m_pressStartPoint = Vector2.zero;
	protected Vector2 m_pressLastHoldPoint = Vector2.zero;
	protected Vector2 m_pressHoldPoint = Vector2.zero;

	// OnPressStart, OnClick, OnPress
	public void OnPressStart(Vector2 hitPos, int holdFrames)
	{
		m_pressing = true;
		m_pressStartPoint = hitPos;
		m_pressLastHoldPoint = hitPos;
		m_pressHoldPoint = hitPos;
		if (m_touchEventEnabled)
			onPressStartEvent.Invoke();
	}

	// OnPressHold, OnPressMove
	public void OnPressHold(Vector2 hitPos, int holdFrames)
	{
		if (m_pressing)
		{
			m_pressLastHoldPoint = m_pressHoldPoint;
			m_pressHoldPoint = hitPos;
			if (m_touchEventEnabled)
				onPressHoldEvent.Invoke();
		}
	}

	// OnPressEnd, OnRelease
	public void OnPressEnd(Vector2 hitPos, int holdFrames)
	{
		if (m_pressing)
		{
			if (m_touchEventEnabled)
				onPressEndEvent.Invoke();
			m_pressStartPoint = Vector2.zero;
			m_pressLastHoldPoint = Vector2.zero;
			m_pressHoldPoint = Vector2.zero;
		}
	}


	public void DebugEvent(string text)
	{
		Debug.Log(gameObject.name + " : " + text);
	}


	//  π”√ Trigger ∑∂Œß

	protected Collider2D m_touchCollider;

	protected void InitCollider()
	{
		m_touchCollider = GetComponent<Collider2D>();
	}

	/*public bool TestHit(Vector2 hitPos)
	{
		//m_touchCollider.
	}*/


	protected void UpdateTouch()
	{

	}



	/*protected void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("TouchableItem Enter : " + collision.gameObject.name);
	}

	protected void OnTriggerStay2D(Collider2D collision)
	{
		Debug.Log("TouchableItem Stay : " + collision.gameObject.name);
	}

	protected void OnTriggerExit2D(Collider2D collision)
	{
		Debug.Log("TouchableItem Exit : " + collision.gameObject.name);
	}*/
}
