﻿#pragma checksum "..\..\GameUI.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "231C799929E1C994A74536F0423B874340C673441C4023986B3F8050881FE1D9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using WorldBattle;


namespace WorldBattle {
    
    
    /// <summary>
    /// GameUI
    /// </summary>
    public partial class GameUI : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition titlurow;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition tablerow;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition butoanerow;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition photocol;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition mytablecol;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition yourtablecol;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas fundalopponent;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas opponenttable;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid pozegrid;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image poza1;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image poza2;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Infolabel;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas fundal;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas mytable;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock subtitle;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button yourTurnButton;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button readyButton;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\GameUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ComboLabel;
        
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
            System.Uri resourceLocater = new System.Uri("/WorldBattle;component/gameui.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\GameUI.xaml"
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
            this.titlurow = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 2:
            this.tablerow = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 3:
            this.butoanerow = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 4:
            this.photocol = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 5:
            this.mytablecol = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 6:
            this.yourtablecol = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 7:
            this.fundalopponent = ((System.Windows.Controls.Canvas)(target));
            return;
            case 8:
            this.opponenttable = ((System.Windows.Controls.Canvas)(target));
            return;
            case 9:
            this.pozegrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 10:
            this.poza1 = ((System.Windows.Controls.Image)(target));
            return;
            case 11:
            this.poza2 = ((System.Windows.Controls.Image)(target));
            return;
            case 12:
            this.Infolabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 13:
            this.fundal = ((System.Windows.Controls.Canvas)(target));
            return;
            case 14:
            this.mytable = ((System.Windows.Controls.Canvas)(target));
            return;
            case 15:
            this.subtitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 16:
            this.yourTurnButton = ((System.Windows.Controls.Button)(target));
            
            #line 64 "..\..\GameUI.xaml"
            this.yourTurnButton.Click += new System.Windows.RoutedEventHandler(this.yourTurnButton_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            this.readyButton = ((System.Windows.Controls.Button)(target));
            
            #line 65 "..\..\GameUI.xaml"
            this.readyButton.Click += new System.Windows.RoutedEventHandler(this.readyButton_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            this.ComboLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

