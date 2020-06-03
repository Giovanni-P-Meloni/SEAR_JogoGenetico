using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//TODO: nao necessario, apenas pra embelezar o editor....
//Basicamente melhorar a interface do Inspector da Unity
//Fazer apenas para aprender a usar o editor de Inspector da Unity
public class LvlChoice : EditorWindow
{
    public string[] whichLevel = new string[] {"Level 1", "Level 2"};
    public int index = 0;
}
