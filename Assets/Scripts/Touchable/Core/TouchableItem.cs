using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Touchable
{
	[RequireComponent(typeof(Collider2D))]
	public abstract class TouchableItem : MonoBehaviour
	{
		protected bool m_touchEnabled = true;
		public bool TouchEnabled => m_touchEnabled;

		public void SetTouchEnabled(bool enabled)
		{
			m_touchEnabled = enabled;
		}

		protected bool m_touchEventEnabled = true;
		public bool TouchEventEnabled => m_touchEventEnabled;

		public void SetTouchEventEnabled(bool enabled)
		{
			m_touchEventEnabled = enabled;
		}


		[SerializeField]
		protected int touchPriority = 0;
		public int TouchPriority => touchPriority;


		[SerializeField]
		protected bool allowDragInClick = false;

		[SerializeField]
		protected bool allowDragOutPress = false;

		[SerializeField]
		protected bool allowPressEndAfterExit = true;
		// TODO


		[SerializeField]
		protected bool singlePressItem = false;

		protected static int numberPressCount = 0;
		public static int NumberPressCount => numberPressCount;
		public static bool HasNumberPressed => numberPressCount > 0;
		// TODO : key

		protected static void OnNumberPressStart()
		{
			++numberPressCount;
		}

		protected static void OnNumberPressEnd()
		{
			--numberPressCount;
		}


		protected bool m_pressing = false;
		protected Vector2 m_pressStartPoint = Vector2.zero;
		protected Vector2 m_pressLastHoldPoint = Vector2.zero;
		protected Vector2 m_pressHoldPoint = Vector2.zero;

		// OnPressStart, OnPressEnter, OnClick, OnPress
		public bool OnPressStart(Vector2 hitPos, int holdFrames)
		{
			if (CheckCanPressStart(holdFrames))
			{
				OnNumberPressStart();
				m_pressing = true;
				m_pressStartPoint = hitPos;
				m_pressLastHoldPoint = hitPos;
				m_pressHoldPoint = hitPos;
				if (m_touchEventEnabled)
					//onPressStartEvent.Invoke();
					CallOnPressStartEvent();
				return true;
			}
			return false;
		}

		protected bool CheckCanPressStart(int holdFrames)
		{
			//return (!singlePressItem || !HasNumberPressed) && (allowDragInClick || holdFrames <= 0);
			return TouchEnabled && (!singlePressItem || !HasNumberPressed) && (allowDragInClick || holdFrames <= 0);
		}

		// OnPressHold, OnPressMove
		public void OnPressHold(Vector2 hitPos, int holdFrames)
		{
			if (m_pressing)
			{
				m_pressLastHoldPoint = m_pressHoldPoint;
				m_pressHoldPoint = hitPos;
				if (m_touchEventEnabled)
					//	onPressHoldEvent.Invoke();
					CallOnPressHoldEvent();
			}
		}

		// OnPressExit
		public bool OnPressExit(Vector2 hitPos, int holdFrames)
		{
			if (m_pressing)
			{
				CallOnPressExitEvent();
				if (allowDragOutPress)
				{
					//m_pressLastHoldPoint = m_pressHoldPoint;
					//m_pressHoldPoint = hitPos;
					return false;
				}
				else
				{
					//OnPressEnd(hitPos, holdFrames);
					return true;
				}
			}
			return true;
		}

		// OnPressEnd, OnRelease
		public void OnPressEnd(Vector2 hitPos, int holdFrames)
		{
			if (m_pressing)
			{
				OnNumberPressEnd();
				if (m_touchEventEnabled)
					//	onPressEndEvent.Invoke();
					CallOnPressEndEvent();
				m_pressStartPoint = Vector2.zero;
				m_pressLastHoldPoint = Vector2.zero;
				m_pressHoldPoint = Vector2.zero;
			}
		}


		//public UnityEvent onPressStartEvent;
		//public UnityEvent onPressHoldEvent;
		//public UnityEvent onPressEndEvent;

		public abstract void CallOnPressStartEvent();
		public abstract void CallOnPressHoldEvent();
		public abstract void CallOnPressExitEvent();
		public abstract void CallOnPressEndEvent();


		public Vector2 LastFrameDragOffset => m_pressHoldPoint - m_pressLastHoldPoint;
		public Vector2 PressingDragOffset => m_pressHoldPoint - m_pressStartPoint;


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


		//protected void UpdateTouch()
		//{
		//
		//}


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
}
