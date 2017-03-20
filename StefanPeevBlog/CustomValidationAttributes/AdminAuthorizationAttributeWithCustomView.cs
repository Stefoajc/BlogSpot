using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StefanPeevBlog.CustomAuthorization
{
    public class AdminAuthorizationAttributeWithCustomView : AuthorizeAttribute
    {
        public string ViewParameter { get; set; }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // The user is not authenticated
                base.HandleUnauthorizedRequest(filterContext);
            }
            else if (!this.Roles.Split(',').Any(filterContext.HttpContext.User.IsInRole))
            {

                // The user is not in any of the listed roles => 
                // show the unauthorized view
                filterContext.Result = new ViewResult
                {
                    ViewName = ViewParameter//"~/Views/Shared/Unauthorized.cshtml"
                };
            }else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}