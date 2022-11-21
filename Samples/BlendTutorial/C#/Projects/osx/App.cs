﻿using NoesisApp;

namespace BlendTutorial
{
    partial class App : Application
    {
        protected override Display CreateDisplay()
        {
            return new AppKitDisplay();
        }

        protected override RenderContext CreateRenderContext()
        {
            return new RenderContextMTL();
        }

        static void Main(string[] args)
        {
            App app = new App();
            app.Uri = "/BlendTutorial;component/App.xaml";
            app.Run();
        }
    }
}
