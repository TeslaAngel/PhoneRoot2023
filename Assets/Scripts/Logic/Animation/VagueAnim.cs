using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
	public class VagueAnim : MonoBehaviour
	{
		protected SpriteRenderer m_renderer;

		protected void Awake()
		{
			m_renderer = GetComponent<SpriteRenderer>();
			m_renderer.sprite = null;
		}


		[SerializeField]
		protected List<Sprite> animFrameSprites;

		[SerializeField]
		protected float duration = 0.1f;

		public float AnimTotalDuration()
		{
			return duration * animFrameSprites.Count;
		}


		public void StartVague()
		{
			m_renderer.sprite = animFrameSprites[0];
			StartCoroutine(StartVague_Coroutine());
		}

		public IEnumerator StartVague_Coroutine()
		{
			foreach (var sprite in animFrameSprites)
			{
				m_renderer.sprite = sprite;
				yield return new WaitForSeconds(duration);
			}
			m_renderer.sprite = null;
		}
	}
}
