﻿#pragma checksum "..\..\..\mainLanding.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "490ACBB4D817EDC96B1CB422BB51E47D803F3132"
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
    /// mainLanding
    /// </summary>
    public partial class mainLanding : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\mainLanding.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox channel_list;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\mainLanding.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button logOut;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\mainLanding.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button friendbtn;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\mainLanding.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button join_new_channel_btn;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\mainLanding.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button new_chnl;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\mainLanding.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox chat_list;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\mainLanding.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox chat_input;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\mainLanding.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button send_button;
        
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
            System.Uri resourceLocater = new System.Uri("/sql_practice;V1.0.0.0;component/mainlanding.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\mainLanding.xaml"
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
            
            #line 2 "..\..\..\mainLanding.xaml"
            ((sql_practice.mainLanding)(target)).Loaded += new System.Windows.RoutedEventHandler(this.mainLanding_loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.channel_list = ((System.Windows.Controls.ListBox)(target));
            
            #line 26 "..\..\..\mainLanding.xaml"
            this.channel_list.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.logOut = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\mainLanding.xaml"
            this.logOut.Click += new System.Windows.RoutedEventHandler(this.logOut_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.friendbtn = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\mainLanding.xaml"
            this.friendbtn.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.join_new_channel_btn = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\mainLanding.xaml"
            this.join_new_channel_btn.Click += new System.Windows.RoutedEventHandler(this.join_new_channel_btn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.new_chnl = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\mainLanding.xaml"
            this.new_chnl.Click += new System.Windows.RoutedEventHandler(this.newchl);
            
            #line default
            #line hidden
            return;
            case 7:
            this.chat_list = ((System.Windows.Controls.ListBox)(target));
            return;
            case 8:
            this.chat_input = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.send_button = ((System.Windows.Controls.Button)(target));
            
            #line 82 "..\..\..\mainLanding.xaml"
            this.send_button.Click += new System.Windows.RoutedEventHandler(this.send_button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

