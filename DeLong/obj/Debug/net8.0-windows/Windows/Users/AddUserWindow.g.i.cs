// Updated by XamlIntelliSenseFileGenerator 26.11.2024 10:46:54
#pragma checksum "..\..\..\..\..\Windows\Users\AddUserWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "39DE1851D6C8FA3D7E920439FC7401B202A327C6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DeLong.Windows.Kirims;
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


namespace DeLong.Windows.Users
{


    /// <summary>
    /// AddKirimWindow
    /// </summary>
    public partial class AddUserWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DeLong;component/windows/users/adduserwindow.xaml", System.UriKind.Relative);

#line 1 "..\..\..\..\..\Windows\Users\AddUserWindow.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.OmborNomiComboBox = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 2:
                    this.YetkazuvchiTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 3:
                    this.SanaDatePicker = ((System.Windows.Controls.DatePicker)(target));
                    return;
                case 4:
                    this.JamiSoniTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 5:
                    this.JamiNarxiTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 6:
                    this.dgInform = ((System.Windows.Controls.DataGrid)(target));
                    return;
                case 7:

#line 43 "..\..\..\..\..\Windows\Users\AddUserWindow.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddRowToDataGrid);

#line default
#line hidden
                    return;
                case 8:

#line 46 "..\..\..\..\..\Windows\Users\AddUserWindow.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddKirimPageButton_Click);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.TextBox txtFIO;
        internal System.Windows.Controls.TextBox txtTelefon;
        internal System.Windows.Controls.TextBox txtAdres;
        internal System.Windows.Controls.TextBox txtTelegramRaqam;
        internal System.Windows.Controls.TextBox txtINN;
        internal System.Windows.Controls.TextBox txtOKONX;
        internal System.Windows.Controls.TextBox txtXisobRaqam;
        internal System.Windows.Controls.TextBox txtJSHSHIR;
        internal System.Windows.Controls.TextBox txtBank;
        internal System.Windows.Controls.TextBox txtFirmaAdres;
    }
}

