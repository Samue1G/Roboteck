﻿#pragma checksum "..\..\RegistrationPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DACDA11158EDECAD9D55742CB19B1EF69BBB95F2"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Roboteck;
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


namespace Roboteck {
    
    
    /// <summary>
    /// RegistrationPage
    /// </summary>
    public partial class RegistrationPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid AccountWindow;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image GearCenterLeft;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid AutoriztionGroupBox;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SignIn;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SignUp;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image GearCenterRight;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image GearTopRight;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image GearTopLeft;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image GearBotLeft;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image GearBotRight;
        
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
            System.Uri resourceLocater = new System.Uri("/Roboteck;component/registrationpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RegistrationPage.xaml"
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
            this.AccountWindow = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.GearCenterLeft = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.AutoriztionGroupBox = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.SignIn = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\RegistrationPage.xaml"
            this.SignIn.Click += new System.Windows.RoutedEventHandler(this.SignIn_OnClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SignUp = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\RegistrationPage.xaml"
            this.SignUp.Click += new System.Windows.RoutedEventHandler(this.SignUp_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.GearCenterRight = ((System.Windows.Controls.Image)(target));
            return;
            case 7:
            this.GearTopRight = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            this.GearTopLeft = ((System.Windows.Controls.Image)(target));
            return;
            case 9:
            this.GearBotLeft = ((System.Windows.Controls.Image)(target));
            return;
            case 10:
            this.GearBotRight = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

