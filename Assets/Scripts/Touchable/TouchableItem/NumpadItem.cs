using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Touchable
{
	public class NumpadItem : TouchableItem
	{
		[SerializeField, Range(0, 9)]
		protected int number = 0;

		public int Number => number;


		[SerializeField]
		protected List<Sprite> numberSprites;

		[SerializeField]
		protected Transform basePosTf = null;

		[SerializeField]
		protected Vector2 space = new Vector2(1.5f, 1.5f);

		protected void InitNumpadSprite()
		{
			var numberTf = transform.Find("Number");
			if (numberTf)
			{
				var renderer = numberTf.GetComponent<SpriteRenderer>();
				if (renderer)
					renderer.sprite = numberSprites[number];
			}

			var basePos = basePosTf ? basePosTf.position : transform.position;
			int bx = number == 0 ? 0 : ((number - 1) % 3 + 1 - 2);
			//int by = number == 0 ? 0 : ((number - 1) / 3 + 1);
			int by = number == 0 ? 0 : (4 - ((number - 1) / 3 + 1));
			var pos = new Vector3(basePos.x + bx * space.x, basePos.y + by * space.y, transform.position.z);
			transform.position = pos;
		}


		protected bool m_allowDrag = false;

		public void SetAllowDrag(bool allow)
		{
			m_allowDrag = allow;
		}

		public void UpdateHoldDrag()
		{
			if (m_allowDrag)
			{
				// m_pressStartPoint
				// m_pressLastHoldPoint
				// m_pressHoldPoint
				// ...
			}
		}


		[SerializeField]
		protected bool btnClickEnabled = true;

		protected bool pressingWithEnabled = false;

		public UnityEngine.Events.UnityEvent onPressOkEvent;
		public UnityEngine.Events.UnityEvent onPressFailEvent;
		public UnityEngine.Events.UnityEvent onPressEndEvent;

		public override void CallOnPressStartEvent()
		{
			if (btnClickEnabled)
			{
				pressingWithEnabled = true;
				onPressOkEvent.Invoke();
			}
			else
			{
				pressingWithEnabled = false;
				onPressFailEvent.Invoke();
			}
		}
		public override void CallOnPressHoldEvent()
		{
			UpdateHoldDrag();
		}
		public override void CallOnPressExitEvent()
		{
		}
		public override void CallOnPressEndEvent()
		{
			if (pressingWithEnabled)
			{
				onPressEndEvent.Invoke();
			}
			else
			{
				pressingWithEnabled = false;
			}
		}
	}
}
