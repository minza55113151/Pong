using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] private Menu[] menus;

    private void Awake()
    {
        instance = this;
    }
    public void OpenMenu(Menu menu)
    {
        OpenMenu(menu.menuName);
    }
    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                menus[i].Open();
            }
            else
            {
                menus[i].Close();
            }
        }
    }
}
