using System;
using System.Collections;
using System.Collections.Generic;

// 红点节点
public class RedPointNode
{
    public string name;
    
    public RedPointNode parent;

    public RedPointSystem.OnRedPointNumChange OnRedPointNumChange;
        
    //子节点列表
    public Dictionary<string, RedPointNode> childNodes = new Dictionary<string, RedPointNode>();

    public bool isLeaf
    {
        get { return childNodes.Count == 0; }
    }

    private int _num;
    public int num
    {
        get { return _num;}
        set
        {
            if (_num != value)
            {
                _num = value;
                NotifyRedPointChange();
                if (parent != null)
                {   
                    parent.ChangeParentRedPoint();
                }
            }
        }
    }

    public void NotifyRedPointChange()
    {
        if (OnRedPointNumChange != null)
        {
            OnRedPointNumChange(this);
        }
    }

    public void ChangeParentRedPoint()
    {
        var totalNum = 0;
        foreach (var node in childNodes)
        {
            totalNum += node.Value.num;
        }

        if (num != totalNum)
        {
            num = totalNum;
        }
    }
}
