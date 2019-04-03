﻿using BowieD.Unturned.NPCMaker.BetterControls;
using BowieD.Unturned.NPCMaker.BetterForms;
using BowieD.Unturned.NPCMaker.Logging;
using BowieD.Unturned.NPCMaker.NPC;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BowieD.Unturned.NPCMaker.Editors
{
    public class VendorEditor : IEditor<NPCVendor>
    {
        public VendorEditor()
        {
            MainWindow.Instance.vendorAddItemButton.Click += (object sender, RoutedEventArgs e) =>
            {
                Universal_VendorItemEditor uvie = new Universal_VendorItemEditor();
                if (uvie.ShowDialog() == true)
                {
                    VendorItem resultedVendorItem = uvie.Result;
                    if (resultedVendorItem.isBuy)
                    {
                        AddItemBuy(resultedVendorItem);
                        Logger.Log($"Added item {resultedVendorItem.id} in buy list");
                    }
                    else
                    {
                        AddItemSell(resultedVendorItem);
                        Logger.Log($"Added item {resultedVendorItem.id} in sell list");
                    }
                }
            };
            MainWindow.Instance.vendorSaveButton.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) => {
                Save();
            });
            MainWindow.Instance.vendorOpenButton.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) =>
            {
                Open();
            });
            MainWindow.Instance.vendorResetButton.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) =>
            {
                Reset();
            });
        }

        public NPCVendor Current
        {
            get
            {
                List<VendorItem> items = new List<VendorItem>();
                foreach (UIElement ui in MainWindow.Instance.vendorListBuyItems.Children)
                {
                    items.Add((ui as Universal_ItemList).Value as VendorItem);
                }
                foreach (UIElement ui in MainWindow.Instance.vendorListSellItems.Children)
                {
                    items.Add((ui as Universal_ItemList).Value as VendorItem);
                }
                Logger.Log($"Built vendor {MainWindow.Instance.vendorIdTxtBox.Value}");
                return new NPCVendor()
                {
                    items = items,
                    id = (ushort)(MainWindow.Instance.vendorIdTxtBox.Value ?? 0),
                    vendorDescription = MainWindow.Instance.vendorDescTxtBox.Text,
                    vendorTitle = MainWindow.Instance.vendorTitleTxtBox.Text,
                    comment = MainWindow.Instance.vendor_commentbox.Text
                };
            }
            set
            {
                Reset();
                foreach (VendorItem item in value.BuyItems)
                {
                    AddItemBuy(item);
                }
                foreach (VendorItem item in value.SellItems)
                {
                    AddItemSell(item);
                }
                MainWindow.Instance.vendorIdTxtBox.Value = value.id;
                MainWindow.Instance.vendorTitleTxtBox.Text = value.vendorTitle;
                MainWindow.Instance.vendorDescTxtBox.Text = value.vendorDescription;
                MainWindow.Instance.vendor_commentbox.Text = value.comment;
                Logger.Log($"Set vendor {value.id}");
            }
        }

        public void Open()
        {
            Universal_ListView ulv = new Universal_ListView(MainWindow.CurrentNPC.vendors.OrderBy(d => d.id).Select(d => new Universal_ItemList(d, Universal_ItemList.ReturnType.Vendor, false)).ToList(), Universal_ItemList.ReturnType.Vendor);
            if (ulv.ShowDialog() == true)
            {
                Save();
                Current = ulv.SelectedValue as NPCVendor;
                Logger.Log($"Opened vendor {MainWindow.Instance.vendorIdTxtBox.Value}");
            }
            MainWindow.CurrentNPC.vendors = ulv.Values.Cast<NPCVendor>().ToList();
        }
        public void Reset()
        {
            MainWindow.Instance.vendorListBuyItems.Children.Clear();
            MainWindow.Instance.vendorListSellItems.Children.Clear();
            MainWindow.Instance.vendorDescTxtBox.Text = "";
            MainWindow.Instance.vendorTitleTxtBox.Text = "";
            Logger.Log($"Vendor {MainWindow.Instance.vendorIdTxtBox.Value} cleared!");
        }
        public void Save()
        {
            NPCVendor cur = Current;
            if (cur.id == 0)
                return;
            if (MainWindow.CurrentNPC.vendors.Where(d => d.id == cur.id).Count() > 0)
            {
                MainWindow.CurrentNPC.vendors.Remove(MainWindow.CurrentNPC.vendors.Where(d => d.id == cur.id).ElementAt(0));
            }
            MainWindow.CurrentNPC.vendors.Add(cur);
            MainWindow.NotificationManager.Notify(MainWindow.Localize("notify_Vendor_Saved"));
            MainWindow.isSaved = false;
            Logger.Log($"Vendor {cur.id} saved!");
        }

        public void AddItemBuy(VendorItem item)
        {
            Universal_ItemList uil = new Universal_ItemList(item, Universal_ItemList.ReturnType.VendorItem, false)
            {
                Width = 240
            };
            uil.deleteButton.Click += (object sender, RoutedEventArgs e) =>
            {
                RemoveItemBuy(Util.FindParent<Universal_ItemList>(sender as Button));
            };
            MainWindow.Instance.vendorListBuyItems.Children.Add(uil);
        }
        public void AddItemSell(VendorItem item)
        {
            Universal_ItemList uil = new Universal_ItemList(item, Universal_ItemList.ReturnType.VendorItem, false)
            {
                Width = 240
            };
            uil.deleteButton.Click += (object sender, RoutedEventArgs e) =>
            {
                RemoveItemSell(Util.FindParent<Universal_ItemList>(sender as Button));
            };
            MainWindow.Instance.vendorListSellItems.Children.Add(uil);
        }
        public void RemoveItemSell(UIElement item)
        {
            MainWindow.Instance.vendorListSellItems.Children.Remove(item);
        }
        public void RemoveItemBuy(UIElement item)
        {
            MainWindow.Instance.vendorListBuyItems.Children.Remove(item);
        }
    }
}