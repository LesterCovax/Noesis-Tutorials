﻿using NoesisApp;

namespace RssReader
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
            app.Uri = "/ApplicationTutorial;component/App.xaml";
            app.Run();
        }
    }
}
