﻿#pragma checksum "..\..\..\Pages\EspaceClient.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "41E92EA9501BCA1940CE6BC2A6E05039942913F77346B2DEA072D0711A9A483C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Expression.Media;
using HandyControl.Expression.Shapes;
using HandyControl.Interactivity;
using HandyControl.Media.Animation;
using HandyControl.Media.Effects;
using HandyControl.Properties.Langs;
using HandyControl.Themes;
using HandyControl.Tools;
using HandyControl.Tools.Converter;
using HandyControl.Tools.Extension;
using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using MenuWithSubMenu.Pages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MenuWithSubMenu.Pages {
    
    
    /// <summary>
    /// EspaceClient
    /// </summary>
    public partial class EspaceClient : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 40 "..\..\..\Pages\EspaceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid_AssetView;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Pages\EspaceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HandyControl.Controls.SearchBar searchBar;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Pages\EspaceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancelFocus;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\Pages\EspaceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border loadingBox;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\Pages\EspaceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border nothingBox;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\Pages\EspaceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel groupInfo;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\Pages\EspaceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteManyBtn;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\Pages\EspaceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid clientsDataGrid;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\..\Pages\EspaceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HandyControl.Controls.Pagination pagination;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MenuWithSubMenu;component/pages/espaceclient.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\EspaceClient.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.grid_AssetView = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.searchBar = ((HandyControl.Controls.SearchBar)(target));
            
            #line 50 "..\..\..\Pages\EspaceClient.xaml"
            this.searchBar.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SearchBar_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.cancelFocus = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\Pages\EspaceClient.xaml"
            this.cancelFocus.Click += new System.Windows.RoutedEventHandler(this.CancelFocus_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 54 "..\..\..\Pages\EspaceClient.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.addClient);
            
            #line default
            #line hidden
            return;
            case 5:
            this.loadingBox = ((System.Windows.Controls.Border)(target));
            return;
            case 6:
            this.nothingBox = ((System.Windows.Controls.Border)(target));
            return;
            case 7:
            this.groupInfo = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 8:
            this.deleteManyBtn = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\..\Pages\EspaceClient.xaml"
            this.deleteManyBtn.Click += new System.Windows.RoutedEventHandler(this.deleteMany);
            
            #line default
            #line hidden
            return;
            case 9:
            this.clientsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 15:
            this.pagination = ((HandyControl.Controls.Pagination)(target));
            
            #line 138 "..\..\..\Pages\EspaceClient.xaml"
            this.pagination.PageUpdated += new System.EventHandler<HandyControl.Data.FunctionEventArgs<int>>(this.page_PageUpdated);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 10:
            
            #line 102 "..\..\..\Pages\EspaceClient.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.unCheckCmd);
            
            #line default
            #line hidden
            
            #line 102 "..\..\..\Pages\EspaceClient.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.checkCmd);
            
            #line default
            #line hidden
            break;
            case 11:
            
            #line 117 "..\..\..\Pages\EspaceClient.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ajouterVisite);
            
            #line default
            #line hidden
            break;
            case 12:
            
            #line 120 "..\..\..\Pages\EspaceClient.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.voirClient);
            
            #line default
            #line hidden
            break;
            case 13:
            
            #line 123 "..\..\..\Pages\EspaceClient.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.updateClient);
            
            #line default
            #line hidden
            break;
            case 14:
            
            #line 126 "..\..\..\Pages\EspaceClient.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.deleteClient);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

