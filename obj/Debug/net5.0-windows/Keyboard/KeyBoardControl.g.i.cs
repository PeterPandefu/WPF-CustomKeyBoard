﻿#pragma checksum "..\..\..\..\Keyboard\KeyBoardControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "25E03C4F1EB0D0C3A7A12BCBAF348DA0ED5FBC9D"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using CustomKeyboard;
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


namespace CustomKeyboard {
    
    
    /// <summary>
    /// KeyBoardControl
    /// </summary>
    public partial class KeyBoardControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\..\Keyboard\KeyBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid boardGrid;
        
        #line default
        #line hidden
        
        
        #line 287 "..\..\..\..\Keyboard\KeyBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CustomKeyboard.ToggonKeyboardKey btn_CapsLock;
        
        #line default
        #line hidden
        
        
        #line 405 "..\..\..\..\Keyboard\KeyBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CustomKeyboard.ToggonKeyboardKey leftShift;
        
        #line default
        #line hidden
        
        
        #line 493 "..\..\..\..\Keyboard\KeyBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CustomKeyboard.ToggonKeyboardKey rightShift;
        
        #line default
        #line hidden
        
        
        #line 531 "..\..\..\..\Keyboard\KeyBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CustomKeyboard.ToggonKeyboardKey leftCtrl;
        
        #line default
        #line hidden
        
        
        #line 547 "..\..\..\..\Keyboard\KeyBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CustomKeyboard.ToggonKeyboardKey leftAlt;
        
        #line default
        #line hidden
        
        
        #line 563 "..\..\..\..\Keyboard\KeyBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CustomKeyboard.ToggonKeyboardKey rightAlt;
        
        #line default
        #line hidden
        
        
        #line 572 "..\..\..\..\Keyboard\KeyBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CustomKeyboard.ToggonKeyboardKey rightCtrl;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CustomKeyboard;component/keyboard/keyboardcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Keyboard\KeyBoardControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.boardGrid = ((System.Windows.Controls.Grid)(target));
            
            #line 25 "..\..\..\..\Keyboard\KeyBoardControl.xaml"
            this.boardGrid.AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new System.Windows.RoutedEventHandler(this.Grid_Click));
            
            #line default
            #line hidden
            return;
            case 2:
            this.btn_CapsLock = ((CustomKeyboard.ToggonKeyboardKey)(target));
            return;
            case 3:
            this.leftShift = ((CustomKeyboard.ToggonKeyboardKey)(target));
            return;
            case 4:
            this.rightShift = ((CustomKeyboard.ToggonKeyboardKey)(target));
            return;
            case 5:
            this.leftCtrl = ((CustomKeyboard.ToggonKeyboardKey)(target));
            return;
            case 6:
            this.leftAlt = ((CustomKeyboard.ToggonKeyboardKey)(target));
            return;
            case 7:
            this.rightAlt = ((CustomKeyboard.ToggonKeyboardKey)(target));
            return;
            case 8:
            this.rightCtrl = ((CustomKeyboard.ToggonKeyboardKey)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
