using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Touchable
{
	public class CommonItem : TouchableItem
	{
		public UnityEvent onPressStartEvent;
		public UnityEvent onPressHoldEvent;
		public UnityEvent onPressExitEvent;
		public UnityEvent onPressEndEvent;

		public override void CallOnPressStartEvent()
		{
			onPressStartEvent.Invoke();
		}
		public override void CallOnPressHoldEvent()
		{
			onPressHoldEvent.Invoke();
		}
		public override void CallOnPressExitEvent()
		{
			onPressExitEvent.Invoke();
		}
		public override void CallOnPressEndEvent()
		{
			onPressEndEvent.Invoke();
		}


		public void ProcessPressHoldDrag()
		{
			var offset = m_pressHoldPoint - m_pressLastHoldPoint;
			var pos = transform.position;
			pos += (Vector3)offset;
			transform.position = pos;
		}
	}
}
