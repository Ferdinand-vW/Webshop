using System.Web.Mvc;

namespace Webshop.Areas.AdminControlPanel
{
    public class AdminControlPanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdminControlPanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminControlPanel_default",
                "AdminControlPanel/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Webshop.Areas.AdminControlPanel.Controllers" }
            );
        }
    }
}