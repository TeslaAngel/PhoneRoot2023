using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TargetType = SpriteItemAnim.TargetType;

[RequireComponent(typeof(SpriteItemAnim))]
public class NumpadAnim : MonoBehaviour
{
	protected SpriteItemAnim m_itemAnim;

	protected SpriteItemAnim TouchItemAnim()
	{
		if (!m_itemAnim)
			m_itemAnim = GetComponent<SpriteItemAnim>();
		return m_itemAnim;
	}


	public Vector2 startScale = new Vector2(.5f, .5f);
	public Vector2 stayScale = new Vector2(1, 1);
	public Vector2 endScale = new Vector2(2, 2);
	[Min(0)]
	public float fadeInTime = .05f;
	[Min(0)]
	public float fadeOutTime = .3f;

	public void AnimShow()
	{
		TouchItemAnim()
			.FinishAllAnims()
			.SetValueImmedi(TargetType.ScaleX, startScale.x)
			.StartValueAnim(TargetType.ScaleX, stayScale.x, fadeInTime)
			.SetValueImmedi(TargetType.ScaleY, startScale.y)
			.StartValueAnim(TargetType.ScaleY, stayScale.y, fadeInTime)
			.SetValueImmedi(TargetType.Alpha, 1);
	}

	public void AnimHide()
	{
		TouchItemAnim()
			.FinishAllAnims()
			.SetValueImmedi(TargetType.ScaleX, stayScale.x)
			.StartValueAnim(TargetType.ScaleX, endScale.x, fadeOutTime)
			.SetValueImmedi(TargetType.ScaleY, stayScale.y)
			.StartValueAnim(TargetType.ScaleY, endScale.y, fadeOutTime)
			.SetValueImmedi(TargetType.Alpha, 1)
			.StartValueAnim(TargetType.Alpha, 0, fadeOutTime);
	}
}
