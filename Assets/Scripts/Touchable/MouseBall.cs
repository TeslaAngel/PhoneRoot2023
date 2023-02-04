using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			//if (!holding)
			//{
			//	justNowHold = true;
			//	holding = true;
			//}

			m_collider.enabled = true;
			if (m_renderer)
				m_renderer.enabled = true;
			var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.z = transform.position.z;
			transform.position = pos;
		}
		else
		{
			//holdTime = 0f;
			//holdFrames = 0;
			m_collider.enabled = false;
			if (m_renderer)
				m_renderer.enabled = false;
		}
	}

	//protected float holdTime = 0f;
	protected int holdFrames = 0;
	//protected bool justNowHold = false;
	//protected bool holding = false;

	public static readonly string touchItemTag = "Touchable";

	protected HashSet<TouchableItem> m_stayItems = new HashSet<TouchableItem>();
	protected HashSet<TouchableItem> m_tempItems = new HashSet<TouchableItem>();

	//protected Dictionary<int, TouchableItem> m_touchableItemCache = new(64);
	protected List<TouchableItem> m_tempItemsFilter = new List<TouchableItem>();

	protected void FixedUpdate()
	{
		var mousePos = MousePointInWorld();
		if (Input.GetMouseButton(0))
		{
			var hits = Physics2D.RaycastAll(mousePos, Vector2.right, 0.0001f);
			var itemsFilter = m_tempItemsFilter;
			itemsFilter.Clear();
			foreach (var each in hits)
			{
				if (each.collider.CompareTag(touchItemTag))
				{
					//var iid = each.collider.GetInstanceID();
					//if (!m_touchableItemCache.ContainsKey(iid))
					//	m_touchableItemCache.Add(iid, each.collider.GetComponent<TouchableItem>());
					//var item = m_touchableItemCache[iid];
					var item = each.collider.GetComponent<TouchableItem>();
					if (item)
					//if (item && (item.AllowDragInClick || holdFrames <= 0))
					{
						itemsFilter.Add(item);
					}
				}
			}

			var mainSet = m_stayItems;
			var tempSet = m_tempItems;
			tempSet.Clear();

			foreach (var item in itemsFilter)
			{
				if (!mainSet.Contains(item))
				{
					if (item.AllowDragInClick || holdFrames <= 0)
					{
						item.OnPressStart(mousePos, holdFrames);
						item.OnPressHold(mousePos, holdFrames);
						tempSet.Add(item);
					}
				}
				else
				{
					item.OnPressHold(mousePos, holdFrames);
					tempSet.Add(item);
				}
			}
			foreach (var item in mainSet)
			{
				if (!tempSet.Contains(item))
					item.OnPressEnd(mousePos, holdFrames);
			}

			m_stayItems = tempSet;
			m_tempItems = mainSet;
			m_tempItems.Clear();
			itemsFilter.Clear();

			++holdFrames;
		}
		else
		{
			var mainSet = m_stayItems;
			foreach (var item in mainSet)
				item.OnPressEnd(mousePos, holdFrames);
			mainSet.Clear();

			holdFrames = 0;
		}
		//Debug.Log("FixedUpdate  " + holdFrames + " | " + holdTime);
	}


	protected Vector2 MousePointInWorld()
	{
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}


	/*protected void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("OnTriggerEnter2D  " + holdFrames + " | " + holdTime);
		//Debug.Log("MouseBall Enter : " + collision.gameObject.name + " " + updateCount);
		if (collision.CompareTag("Touchable"))
		{
			collision.GetComponent<TouchableItem>()?.OnPressStart(MousePointInWorld());
		}
	}

	protected void OnTriggerStay2D(Collider2D collision)
	{
		//Debug.Log("MouseBall Stay : " + collision.gameObject.name + " " + updateCount);
		if (collision.CompareTag("Touchable"))
		{
			collision.GetComponent<TouchableItem>()?.OnPressHold(MousePointInWorld());
		}
	}

	protected void OnTriggerExit2D(Collider2D collision)
	{
		//Debug.Log("MouseBall Exit : " + collision.gameObject.name + " " + updateCount);
		if (collision.CompareTag("Touchable"))
		{
			collision.GetComponent<TouchableItem>()?.OnPressEnd(MousePointInWorld());
		}
	}*/
}
