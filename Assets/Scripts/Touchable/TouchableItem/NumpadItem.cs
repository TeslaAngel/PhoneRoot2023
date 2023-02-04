using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


	protected void Awake()
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
}
