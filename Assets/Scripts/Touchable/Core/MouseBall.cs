using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Touchable
{
	[RequireComponent(typeof(Collider2D))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class MouseBall : MonoBehaviour
	{
		protected Collider2D m_collider;
		protected Rigidbody2D m_rigidbody;
		protected SpriteRenderer m_renderer;

		protected void Awake()
		{
			m_collider = GetComponent<Collider2D>();
			m_rigidbody = GetComponent<Rigidbody2D>();
			m_renderer = GetComponent<SpriteRenderer>();

			m_collider.isTrigger = true;
			m_rigidbody.bodyType = RigidbodyType2D.Kinematic;
			if (m_renderer)
				m_renderer.enabled = false;
		}

		protected int updateCount = 0;

		protected void Update()
		{
			++updateCount;
			if (Input.GetMouseButton(0))
			{
				m_collider.enabled = true;
				if (m_renderer)
					m_renderer.enabled = true;
				transform.position = MousePointInWorld(transform.position.z);
			}
			else
			{
				m_collider.enabled = false;
				if (m_renderer)
					m_renderer.enabled = false;
			}
		}


		protected Vector2 MousePointInWorld()
		{
			return Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		protected Vector3 MousePointInWorld(float z)
		{
			Vector3 pos = MousePointInWorld();
			pos.z = z;
			return pos;
		}


		protected int holdFrames = 0;

		public static readonly string touchItemTag = "Touchable";
		public static readonly string phoneScreenTag = "PhoneScreen"; // todo

		protected HashSet<TouchableItem> m_pressingItems = new HashSet<TouchableItem>();
		protected HashSet<TouchableItem> m_tempItems = new HashSet<TouchableItem>();

		protected List<TouchableItem> m_tempItemsFilter = new List<TouchableItem>();

		protected int CompareItemPriority(TouchableItem a, TouchableItem b)
		{
			if (b.TouchPriority != a.TouchPriority)
			{
				return b.TouchPriority - a.TouchPriority;
			}
			else if (b.transform.position.z != a.transform.position.z)
			{
				return Mathf.FloorToInt(b.transform.position.z - a.transform.position.z + 0.0001f);
			}
			else
			{
				return 0;
			}
		}

		protected void FixedUpdate()
		{
			var mousePos = MousePointInWorld();
			if (Input.GetMouseButton(0))
			{
				var hits = Physics2D.RaycastAll(mousePos, Vector2.right, 0.0001f);
				var itemsFilter = m_tempItemsFilter;
				itemsFilter.Clear();
				//TouchableItem bestHitItem = null;
				//int bestPriority = 0;
				foreach (var each in hits)
				{
					if (each.collider.CompareTag(touchItemTag))
					{
						var item = each.collider.GetComponent<TouchableItem>();
						if (item)
						{
							itemsFilter.Add(item);
							//if (bestHitItem == null || bestPriority < item.TouchPriority)
							//{
							//	bestHitItem = item;
							//	bestPriority = item.TouchPriority;
							//}
						}
					}
				}

				itemsFilter.Sort(CompareItemPriority);


				var pressingSet = m_pressingItems;
				var tempSet = m_tempItems;
				tempSet.Clear();

				foreach (var item in itemsFilter)
				{
					// press enter
					if (pressingSet.Contains(item))
					{
						item.OnPressHold(mousePos, holdFrames);
						tempSet.Add(item);
						break; // 只取第一个
					}
				}
				if (tempSet.Count <= 0)
				{
					foreach (var item in itemsFilter)
					{
						if (item.OnPressStart(mousePos, holdFrames))
						{
							item.OnPressHold(mousePos, holdFrames);
							tempSet.Add(item);
							break; // 只取第一个
						}
					}
				}

				foreach (var item in pressingSet)
				{
					if (!tempSet.Contains(item))
					{
						// press exit
						if (item.OnPressExit(mousePos, holdFrames))
						{
							item.OnPressEnd(mousePos, holdFrames);
						}
						else
						{
							item.OnPressHold(mousePos, holdFrames);
							tempSet.Add(item);
						}
					}
				}

				m_pressingItems = tempSet;
				m_tempItems = pressingSet;
				m_tempItems.Clear();
				m_tempItemsFilter.Clear();

				++holdFrames;
			}
			else
			{
				var mainSet = m_pressingItems;
				foreach (var item in mainSet)
					item.OnPressEnd(mousePos, holdFrames);
				mainSet.Clear();

				holdFrames = 0;
			}
		}
	}
}
