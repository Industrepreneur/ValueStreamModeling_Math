using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for EquipResultPage
/// </summary>
public abstract class EquipResultPage: TableGraph
{
	protected Menu productMenu;

    protected int value;

    public EquipResultPage() {

    }

    protected override void OnInit(EventArgs e) {
        base.OnInit(e);
        createMenu();
    }

    protected void createMenu() {
        productMenu = new Menu();
        productMenu.ID = "productMenu";
        productMenu.Orientation = Orientation.Horizontal;
        productMenu.MenuItemClick += productMenu_MenuItemClick;
        productMenu.StaticMenuItemStyle.CssClass = "productMenuItem";
        productMenu.StaticSelectedStyle.CssClass = "productMenuItemActive";

        MenuItem item = new MenuItem("Equipment Table and Utilization Graph", "0");

        productMenu.Items.Add(item);
        item = new MenuItem("Equipment WIP Graph", "1");
        productMenu.Items.Add(item);

        item = new MenuItem("Equip Oper Details 1", "2");
        productMenu.Items.Add(item);

        item = new MenuItem("Equip Oper Details 2", "3");
        productMenu.Items.Add(item);

        productMenu.Items[value].Selected = true; // select the correct tab according to the page

        Control menuContainer = GetMenuContainer();
        menuContainer.Controls.Add(productMenu);
    }

    protected abstract Control GetMenuContainer();

    protected abstract Control GetTabsDiv();

    protected void productMenu_MenuItemClick(object sender, MenuEventArgs e) {
        int itemNum = int.Parse(e.Item.Value);

        switch (itemNum) {
            case 0:
                Response.Redirect("results_equip.aspx");
                break;
            case 1:
                Response.Redirect("results_equip_graph2.aspx");
                break;
            case 2:
                Response.Redirect("results_equip_oper1.aspx");
                break;
            case 3:
                Response.Redirect("results_equip_oper2.aspx");
                break;
            default:
                break;
        }
    }
}