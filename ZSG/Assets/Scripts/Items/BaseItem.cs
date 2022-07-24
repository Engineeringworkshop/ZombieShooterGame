using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BaseItem : MonoBehaviour
{
    protected float itemWeight;
    protected float itemPrice;

	[SerializeField] string id;
	public string ID { get { return id; } }
	public string ItemName;
	public Sprite Icon;
	[Range(1, 999)]
	public int MaximumStacks = 1;

	protected static readonly StringBuilder sb = new StringBuilder();

#if UNITY_EDITOR
	protected virtual void OnValidate()
	{
		string path = AssetDatabase.GetAssetPath(this);
		id = AssetDatabase.AssetPathToGUID(path);
	}
#endif

	public virtual BaseItem GetCopy()
	{
		return this;
	}

	public virtual void Destroy()
	{

	}

	public virtual string GetItemType()
	{
		return "";
	}

	public virtual string GetDescription()
	{
		return "";
	}

	#region Update methods

	protected void UpdateItemWeight()
    {

    }

    protected void updateItemPrice()
    {

    }

    #endregion

    #region Get methods

    public float GetItemWeight()
    {
        return itemWeight;
    }

    public float GetItemPrice()
    {
        return itemPrice;
    }

    #endregion
}
