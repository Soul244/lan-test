﻿#pragma checksum "..\..\..\..\..\Views\UserControllers\AnasayfaPages\UcBaslangic.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "99F3437926F8F4743625A4B15AC990D268887CAB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LTest;
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


namespace LTest.Views.UserControllers.AnasayfaPages {
    
    
    /// <summary>
    /// UcBaslangic
    /// </summary>
    public partial class UcBaslangic : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\..\..\Views\UserControllers\AnasayfaPages\UcBaslangic.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\..\Views\UserControllers\AnasayfaPages\UcBaslangic.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel dockPanel;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\..\Views\UserControllers\AnasayfaPages\UcBaslangic.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OdaBaslat;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\..\Views\UserControllers\AnasayfaPages\UcBaslangic.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl Icerik;
        
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
            System.Uri resourceLocater = new System.Uri("/LTest;component/views/usercontrollers/anasayfapages/ucbaslangic.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\UserControllers\AnasayfaPages\UcBaslangic.xaml"
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
            
            #line 9 "..\..\..\..\..\Views\UserControllers\AnasayfaPages\UcBaslangic.xaml"
            ((LTest.Views.UserControllers.AnasayfaPages.UcBaslangic)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.dockPanel = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 4:
            this.OdaBaslat = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\..\..\Views\UserControllers\AnasayfaPages\UcBaslangic.xaml"
            this.OdaBaslat.Click += new System.Windows.RoutedEventHandler(this.UcOdaBaslat);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Icerik = ((System.Windows.Controls.ContentControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
