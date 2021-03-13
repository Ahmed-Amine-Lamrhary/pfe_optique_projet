﻿#pragma checksum "..\..\..\Pages\EspaceClient.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B491683425864515A12A5231DBEFD0940E7B1210"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
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
        
        
        #line 13 "..\..\..\Pages\EspaceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid_AssetView;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Pages\EspaceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border loadingBox;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Pages\EspaceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HandyControl.Controls.SearchBar searchBar;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Pages\EspaceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancelFocus;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Pages\EspaceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid clientsDataGrid;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\Pages\EspaceClient.xaml"
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
            this.loadingBox = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.searchBar = ((HandyControl.Controls.SearchBar)(target));
            
            #line 38 "..\..\..\Pages\EspaceClient.xaml"
            this.searchBar.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SearchBar_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cancelFocus = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\Pages\EspaceClient.xaml"
            this.cancelFocus.Click += new System.Windows.RoutedEventHandler(this.CancelFocus_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 40 "..\..\..\Pages\EspaceClient.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.addClient);
            
            #line default
            #line hidden
            return;
            case 6:
            this.clientsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 43 "..\..\..\Pages\EspaceClient.xaml"
            this.clientsDataGrid.AutoGeneratedColumns += new System.EventHandler(this.DataGrid_AutoGeneratedColumns);
            
            #line default
            #line hidden
            return;
            case 12:
            this.pagination = ((HandyControl.Controls.Pagination)(target));
            
            #line 69 "..\..\..\Pages\EspaceClient.xaml"
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
            case 7:
            
            #line 49 "..\..\..\Pages\EspaceClient.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddPresetButton_Click);
            
            #line default
            #line hidden
            break;
            case 8:
            
            #line 56 "..\..\..\Pages\EspaceClient.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ajouterVisite);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 57 "..\..\..\Pages\EspaceClient.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.voirClient);
            
            #line default
            #line hidden
            break;
            case 10:
            
            #line 58 "..\..\..\Pages\EspaceClient.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.updateClient);
            
            #line default
            #line hidden
            break;
            case 11:
            
            #line 59 "..\..\..\Pages\EspaceClient.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.deleteClient);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

