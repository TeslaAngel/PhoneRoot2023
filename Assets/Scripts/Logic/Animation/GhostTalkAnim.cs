using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
	using TMPro;
	using Touchable;

	public class GhostTalkAnim : MonoBehaviour
	{
		[SerializeField]
		protected TextMeshPro textMeshPro;

		public void ClearText()
		{
			textMeshPro.text = "";
		}

		public void SetText(string text)
		{
			textMeshPro.text = text;
		}


		[SerializeField]
		protected int printCharSpeed = 2;

		protected bool m_printTextMode = false;
		protected string m_targetText = "";
		protected int m_curPrintIndex = 0;
		protected int m_waitPrintCount = 0;

		public void SetTextWithPrint(string text)
		{
			ClearText();

			m_printTextMode = true;
			m_targetText = text;
			m_curPrintIndex = 0;
			m_waitPrintCount = 0;

			ResetTextAlpha();
		}

		public void StopPrintText(bool finish = false)
		{
			if (finish)
				SetText(m_targetText);

			m_printTextMode = false;
			m_targetText = "";
			m_curPrintIndex = 0;
			m_waitPrintCount = 0;
		}


		public float PrintTextTotalDuration(string text)
		{
			return text.Length * printCharSpeed * Time.fixedDeltaTime;
		}


		protected void FixedUpdate()
		{
			UpdatePrintText();
		}

		protected void UpdatePrintText()
		{
			if (m_printTextMode)
			{
				if (m_waitPrintCount <= 0)
				{
					m_waitPrintCount = printCharSpeed;
					++m_curPrintIndex;
					SetText(m_targetText.Substring(0, m_curPrintIndex));

					if (m_curPrintIndex >= m_targetText.Length)
						StopPrintText(true);
				}
				else
				{
					--m_waitPrintCount;
				}
			}
		}


		protected AnimData fadeAnimData = new AnimData();

		protected void Awake()
		{
			fadeAnimData.SetAnimSetValMethod(AnimSetTextAlpha);
		}

		protected void AnimSetTextAlpha(AnimData animData)
		{
			var color = textMeshPro.color;
			color.a = animData.CurrentVal;
			textMeshPro.color = color;
		}

		protected void ResetTextAlpha()
		{
			var color = textMeshPro.color;
			color.a = 1f;
			textMeshPro.color = color;
		}


		[SerializeField]
		protected float fadeOutTime = 1f;

		public void StartFadeOutText()
		{
			fadeAnimData.Start(1, 0, fadeOutTime);
		}


		protected void Update()
		{
			UpdateFadeAnim();
		}

		protected void UpdateFadeAnim()
		{
			if (fadeAnimData.Playing)
				fadeAnimData.UpdateAndSetValue(Time.deltaTime);
		}
	}
}
