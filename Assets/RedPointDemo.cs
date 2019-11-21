using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPointDemo : MonoBehaviour
{
    private RedPointSystem redPointSystem;
    IEnumerator Start()
    {
        redPointSystem = new RedPointSystem();
        redPointSystem.Init();


        // init
        //redPointSystem.RegisterEvent(RedPointEnum.Main, (RedPointNode node) =>
        //{
        //    Debug.Log("node.name=" + node.name + " node.num=" + node.num);
        //});

        redPointSystem.RegisterEvent(RedPointEnum.Task, (RedPointNode node) =>
        {
            Debug.Log("node.name=" + node.name + " node.num=" + node.num);
        });

        redPointSystem.RegisterEvent(RedPointEnum.Mail, (RedPointNode node) =>
        {
            Debug.Log("node.name=" + node.name + " node.num=" + node.num);
        });

        redPointSystem.RegisterEvent(RedPointEnum.MailTeam, (RedPointNode node) =>
        {
            Debug.Log("node.name=" + node.name + " node.num=" + node.num);
        });

        redPointSystem.RegisterEvent(RedPointEnum.MailSystem, (RedPointNode node) =>
        {
            Debug.Log("node.name=" + node.name + " node.num=" + node.num);
        });

        //change
        Debug.Log("################### change1 ");
        redPointSystem.SetRedPoint(RedPointEnum.Task, 1);
        redPointSystem.SetRedPoint(RedPointEnum.MailTeam, 2);
        redPointSystem.SetRedPoint(RedPointEnum.MailSystem, 3);

        yield return new WaitForSeconds(2.0f);
        Debug.Log("################### change2 ");
        redPointSystem.SetRedPoint(RedPointEnum.MailTeam, 0);
    }
}
