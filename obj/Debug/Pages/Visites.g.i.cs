﻿#pragma checksum "..\..\..\Pages\Visites.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D6864051DC4782434F6584577E849E5D480F6FE6854B86C817640E39C9392327"
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
    /// Visite
    /// </summary>
    public partial class Visite : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 48 "..\..\..\Pages\Visites.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid_AssetView;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\Pages\Visites.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border loadingBox;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\Pages\Visites.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border nothingBox;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\Pages\Visites.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel infoBox;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\Pages\Visites.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel autoDate;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\Pages\Visites.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button returnBtn;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\Pages\Visites.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox lastDate;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\Pages\Visites.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel manuallyDate;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\Pages\Visites.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker filterStartDate;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\Pages\Visites.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker filterEndDate;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\..\Pages\Visites.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel groupInfo;
        
        #line default
        #line hidden
        
        
        #line 136 "..\..\..\Pages\Visites.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteManyBtn;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\..\Pages\Visites.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid visitesDataGrid;
        
        #line default
        #line hidden
        
        
        #line 169 "..\..\..\Pages\Visites.xaml"
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
            System.Uri resourceLocater = new System.Uri("/MenuWithSubMenu;component/pages/visites.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Visites.xaml"
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
            this.nothingBox = ((System.Windows.Controls.Border)(target));
            return;
            case 4:
            this.infoBox = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.autoDate = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 6:
            this.returnBtn = ((System.Windows.Controls.Button)(target));
            
            #line 94 "..\..\..\Pages\Visites.xaml"
            this.returnBtn.Click += new System.Windows.RoutedEventHandler(this.ReturnBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.lastDate = ((System.Windows.Controls.ComboBox)(target));
            
            #line 99 "..\..\..\Pages\Visites.xaml"
            this.lastDate.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.selectLastDate);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 107 "..\..\..\Pages\Visites.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.resetFilter);
            
            #line default
            #line hidden
            return;
            case 9:
            this.manuallyDate = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 10:
            this.filterStartDate = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 11:
            this.filterEndDate = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 12:
            
            #line 123 "..\..\..\Pages\Visites.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.filterByDate);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 126 "..\..\..\Pages\Visites.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.deleteManualDate);
            
            #line default
            #line hidden
            return;
            case 14:
            this.groupInfo = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 15:
            this.deleteManyBtn = ((System.Windows.Controls.Button)(target));
            
            #line 136 "..\..\..\Pages\Visites.xaml"
            this.deleteManyBtn.Click += new System.Windows.RoutedEventHandler(this.deleteMany);
            
            #line default
            #line hidden
            return;
            case 16:
            this.visitesDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 20:
            this.pagination = ((HandyControl.Controls.Pagination)(target));
            
            #line 169 "..\..\..\Pages\Visites.xaml"
            this.pagination.PageUpdated += new System.EventHandler<HandyControl.Data.FunctionEventArgs<int>>(this.pagination_PageUpdated);
            
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
            case 17:
            
            #line 143 "..\..\..\Pages\Visites.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.unCheckCmd);
            
            #line default
            #line hidden
            
            #line 143 "..\..\..\Pages\Visites.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.checkCmd);
            
            #line default
            #line hidden
            break;
            case 18:
            
            #line 155 "..\..\..\Pages\Visites.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.voirVisite);
            
            #line default
            #line hidden
            break;
            case 19:
            
            #line 158 "..\..\..\Pages\Visites.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.voirClient);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

