﻿using System;
using NoesisApp;

namespace NineSlice
{
    partial class App : Application
    {
        protected override Display CreateDisplay()
        {
            return new Win32Display();
        }

        protected override RenderContext CreateRenderContext()
        {
            return new RenderContextD3D11();
        }

        [STAThread]
        static void Main()
        {
            App app = new App();
            app.Uri = "/NineSlice;component/App.xaml";
            app.Run();
        }
    }
}
