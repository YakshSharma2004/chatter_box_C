﻿#pragma checksum "..\..\..\friendSearch.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D2ADD8F3FD19E7F5132663A645BB6ABC2138EEBF"
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
using System.Windows.Controls.Ribbon;
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
using sql_practice;


namespace sql_practice {
    
    
    /// <summary>
    /// friendSearch
    /// </summary>
    public partial class friendSearch : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 47 "..\..\..\friendSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox search_txt;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\friendSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button search_btn;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\friendSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button back_btn;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\friendSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button reqs;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\friendSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox user_lst;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/sql_practice;component/friendsearch.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\friendSearch.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 2 "..\..\..\friendSearch.xaml"
            ((sql_practice.friendSearch)(target)).Loaded += new System.Windows.RoutedEventHandler(this.friendSearch_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.search_txt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.search_btn = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\..\friendSearch.xaml"
            this.search_btn.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.back_btn = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\..\friendSearch.xaml"
            this.back_btn.Click += new System.Windows.RoutedEventHandler(this.back_btn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.reqs = ((System.Windows.Controls.Button)(target));
            
            #line 88 "..\..\..\friendSearch.xaml"
            this.reqs.Click += new System.Windows.RoutedEventHandler(this.reqs_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.user_lst = ((System.Windows.Controls.ListBox)(target));
            
            #line 104 "..\..\..\friendSearch.xaml"
            this.user_lst.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.user_lst_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

