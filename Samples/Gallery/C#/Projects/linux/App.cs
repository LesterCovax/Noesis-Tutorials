﻿using NoesisApp;
using System.Runtime.InteropServices;

namespace Gallery
{
    partial class App : Application
    {
        protected override Display CreateDisplay()
        {
            return new XDisplay();
        }

        protected override RenderContext CreateRenderContext()
        {
            if (RuntimeInformation.OSArchitecture == Architecture.Arm ||
                RuntimeInformation.OSArchitecture == Architecture.Arm64)
            {
                return new RenderContextEGL();
            }
            else
            {
                return new RenderContextGLX();
            }
        }

        static void Main(string[] args)
        {
            App app = new App();
            app.Uri = "/Gallery;component/App.xaml";
            app.Run();
        }
    }
}
