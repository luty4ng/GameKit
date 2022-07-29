using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;
using GameKit.DataStructure;
using GameKit;

public static class DialogPhaser
{
    public static List<string> semantics = new List<string>()
    {
        "name",
        "sbranch",
        "cbranch",
        "ebranch",
        "linkfrom",
        "linkto",
        "mood",
        "cdivider",
        "ccomplete"
    };
    public static List<string> prioritizedSemantics = new List<string>() // 该语法集合不会自动上链
    {
        "enter",
        "ebranch",
        "linkfrom"
    };

    public static List<string> declareSemantics = new List<string>() // 该语法集合声明节点名称
    {
        "name",
        "sbranch",
        "ebranch",
        "cbranch"
    };

    static Regex smallBracketRegex = new Regex(@"\(\S+\)", RegexOptions.IgnoreCase);
    public static void PhaseNode(Node<Dialog> node, string text)
    {
        if (text.Correction() == "\n" || text.Correction() == "")
        {
            Debug.LogWarning("[Phaser] Skip invalid node syntax.");
            return;
        }

        if (text.Substring(0, 2).Correction() == "//")
        {
            Debug.Log($"[Phaser] Detect comments.");
            return;
        }

        DialogTree tree = (node.Tree as DialogTree);
        string nodeInfo = Regex.Match(text, @"(?i)(?<=\[)(.*)(?=\])").Value.Trim();
        string dialogInfo = text.Split(']').LastOrDefault();

        if (dialogInfo.Split('：').Length == 2) // 普通节点
        {
            string speaker = dialogInfo.Split('：').FirstOrDefault().Trim();
            string contents = dialogInfo.Split('：').LastOrDefault().Trim();
            node.nodeEntity.speaker = speaker;
            node.nodeEntity.contents = contents;
        }
        else if (dialogInfo.Split('：').Length == 1)    // 对话选项节点
        {
            node.nodeEntity.IsFunctional = true;
            node.nodeEntity.contents = dialogInfo;
        }
        else // 功能性节点
        {
            node.nodeEntity.IsFunctional = true;
        }

        bool customLinking = true;

        if (nodeInfo != "")
        {
            string[] parameters = nodeInfo.Split(',');

            for (int i = 0; i < parameters.Length; i++)
            {
                string semantic = parameters[i].Split('-').FirstOrDefault().Correction();
                string value = parameters[i].Split('-').LastOrDefault().Correction();

                if (prioritizedSemantics.Contains(semantic))
                    customLinking = false;

                if (!semantics.Contains(semantic))
                    Debug.LogWarning(string.Format("Invalid semantic {0} is used.", semantic));

                if (semantic == "pos")
                {
                    node.nodeEntity.pos = value == "left" ? SpritePos.Left : SpritePos.Right;
                }

                if (semantic == "mood")
                {
                    node.nodeEntity.moodName = value;
                }

                if (semantic == "sbranch")
                {
                    node.IsSBranch = true;
                    tree.RecordBranch(node);
                    node.Id = value;
                    tree.RecordDeclaredNode(node);
                }
                else if (semantic == "ebranch")
                {
                    node.Id = value;
                    tree.RecordDeclaredNode(node);
                }
                else if (semantic == "name")
                {
                    node.Id = value;
                    tree.RecordDeclaredNode(node);
                }

                if (semantic == "linkfrom")
                {
                    tree.CachedLinkFromDeclared(node, value);
                }
                else if (semantic == "linkto")
                {
                    tree.CachedLinkToDeclared(node, value);
                }

                if (semantic == "ccomplete")
                {
                    node.nodeEntity.IsCompleter = true;
                    // Debug.Log(smallBracketRegex.Match(value));
                    node.nodeEntity.completeConditons = value.Trim().RemoveBracket().Split('&').ToList();
                    foreach (var condition in node.nodeEntity.completeConditons)
                    {
                        if (!tree.LocalConditions.ContainsKey(condition))
                            tree.LocalConditions.Add(condition, false);
                    }
                }

                if (semantic == "cdivider")
                {
                    node.nodeEntity.IsDivider = true;
                    node.nodeEntity.IsFunctional = true;
                    string[] cparams = GetParams(value);
                    if (cparams.Length < 3)
                    {
                        Debug.LogError($"[Phaser] cdivider command require at least 3 parameters");
                        return;
                    }
                    
                    node.nodeEntity.dividerConditions = cparams[0].Trim().RemoveBracket().Split('&').ToList();
                    // Debug.Log(smallBracketRegex.Match(cparams[0]));
                    tree.CachedLinkToDeclared(node, cparams[1]);
                    tree.CachedLinkToDeclared(node, cparams[2]);
                }
            }

            if (customLinking)
                tree.AddFromLast(node);
        }
        else
        {
            tree.AddFromLast(node);
        }
        tree.currentNode = node;
    }

    private static string[] GetParams(string value)
    {
        return value.Split(' ');
    }
}
