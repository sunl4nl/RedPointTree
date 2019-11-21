using System;
using System.Collections.Generic;

public class RedPointSystem
{   
    public delegate void OnRedPointNumChange(RedPointNode node);
        
    //根节点
    public RedPointNode rootNode;

    // 配置红点列表
    public List<string> listConfig = new List<string>()
    {
        RedPointEnum.Main,
        RedPointEnum.Task,
        RedPointEnum.Mail,
        RedPointEnum.MailTeam,
        RedPointEnum.MailSystem,
    };

    public void Init()
    {
        rootNode = new RedPointNode();
        rootNode.name = RedPointEnum.Main;

        for (var i = 0; i < listConfig.Count; i++)
        {
            var node = rootNode;
            var arraySplit = listConfig[i].Split('.');
            if (arraySplit.Length > 1)
            {
                for (int j = 0; j < arraySplit.Length; j++)
                {
                    if (!node.childNodes.ContainsKey(arraySplit[j]))
                    {
                        node.childNodes.Add(arraySplit[j], new RedPointNode());
                    }
                    node.childNodes[arraySplit[j]].name = arraySplit[j];
                    node.childNodes[arraySplit[j]].parent = node;
                    node = node.childNodes[arraySplit[j]];
                }
            }
        }
    }

    public void RegisterEvent(string nodeName, OnRedPointNumChange onRedPointNumChange)
    {
        var nodeNameArr = nodeName.Split('.');
        if (nodeNameArr.Length == 0)
        {   
            return;
        }
        nodeName = nodeNameArr[nodeNameArr.Length - 1];

        RedPointNode node = PeekNode(rootNode, nodeName);
        //RedPointNode node = null;
        //PeekNode(rootNode, nodeName, out node);
        if (node != null)
        {
            node.OnRedPointNumChange = onRedPointNumChange;
        }
        else
        {
            UnityEngine.Debug.LogError("[RegisterEvent] not found node => " + nodeName);
        }
    }

    public void SetRedPoint(string nodeName, int num)
    {
        var nodeNameArr = nodeName.Split('.');
        if (nodeNameArr.Length == 0)
        {
            return;
        }
        nodeName = nodeNameArr[nodeNameArr.Length - 1];
        RedPointNode node = PeekNode(rootNode, nodeName);
        //RedPointNode node = null;
        //PeekNode(rootNode, nodeName, out node);
        if (node != null)
        {   
            node.num = num;
        }
        else
        {
            UnityEngine.Debug.LogError("[SetRedPoint] not found node => " + nodeName);
        }
    }

    void PeekNode(RedPointNode node, string nodeName, out RedPointNode ret)
    {
        if (node.name == nodeName)
        {
            ret = node;
            return;
        }

        if (node.childNodes.ContainsKey(nodeName))
        {
            ret = node.childNodes[nodeName];
            return;
        }

        foreach (var kv in node.childNodes)
        {
            PeekNode(kv.Value, nodeName, out ret);
            if (ret != null)
            {
                return;
            }
        }

        ret = null;
    }


    RedPointNode PeekNode(RedPointNode node, string nodeName)
    {
        if (node.name == nodeName)
        {
            return node;
        }

        if (node.childNodes.ContainsKey(nodeName))
        {
            return node.childNodes[nodeName];
        }

        foreach (var kv in node.childNodes)
        {
            var result = PeekNode(kv.Value, nodeName);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }
}
