using Microsoft.Owin;
using Owin;
using InvoicingTool.Models;
using System.Globalization;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(InvoicingTool.Startup))]
namespace InvoicingTool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());

        }
    }
}
