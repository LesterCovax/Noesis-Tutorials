﻿using System;
using NoesisApp;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace Gallery
{
    partial class App : Application
    {
        protected override Display CreateDisplay()
        {
            return new WinRTDisplay();
        }

        protected override RenderContext CreateRenderContext()
        {
            return new RenderContextD3D11();
        }

        static void Main()
        {
            CoreApplication.Run(new FrameworkViewSource());
        }

        public class FrameworkViewSource : IFrameworkViewSource
        {
            public IFrameworkView CreateView()
            {
                return new FrameworkView();
            }
        }

        public class FrameworkView : IFrameworkView
        {
            public void SetWindow(CoreWindow window) { }
            public void Load(string entryPoint) { }
            public void Uninitialize() { }
            public void Initialize(CoreApplicationView applicationView) { }

            public void Run()
            {
                App app = new App();
                app.Uri = "/Gallery;component/App.xaml";
                app.Run();
            }
        }
    }
}
