using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;


namespace WebUI
{
    public static class ActionLinkHelper
    {
        public static MvcHtmlString ChildActionLink(this HtmlHelper html, string text, string iconClass, string spanClass, string activeMenu, string action, string controller)
        {
            var str = "";
            string bClass = "";
            var context = html.ViewContext;
            if (context.Controller.ControllerContext.IsChildAction)
                context = html.ViewContext.ParentActionViewContext;
            var routeValues = context.RouteData.Values;
            var currentAction = routeValues["action"].ToString();
            var currentController = routeValues["controller"].ToString();
            var link = html.ActionLink("{0}{1}", action, controller).ToString();

            var url = string.Format(link,
                String.IsNullOrWhiteSpace(iconClass) ? String.Empty : "<i class= \"" + iconClass + "\"></i>",
                String.IsNullOrWhiteSpace(spanClass) ? text : "<span class= \"" + spanClass + "\">" + text + "</span>",
                String.IsNullOrWhiteSpace(bClass) ? String.Empty : "<b class=\"" + bClass + "\"></b>");


            str = String.Format("<li {0}>{1}{2}</li>",
               currentAction.Equals(action, StringComparison.InvariantCulture) &&
               currentController.Equals(controller, StringComparison.InvariantCulture) ?
               " class=\"active\"" :
               String.Empty, html.Raw(url).ToHtmlString(),
               "<b class=\"arrow\"></b>"
           );


            if (activeMenu.Equals("dynamic") == false && activeMenu != "")
            {



                str = String.Format("<li {0}>{1}{2}</li>",
                 activeMenu.Equals(action, StringComparison.InvariantCulture) &&
                 currentController.Equals("Report", StringComparison.InvariantCulture) ?
                 " class=\"active\"" :
                 String.Empty, html.Raw(url).ToHtmlString(),
                 "<b class=\"arrow\"></b>"
             );



            }

            return new MvcHtmlString(str);
        }
        /*
                public static string ActiveTab(this HtmlHelper helper, string ControllerPrefix)
                {

                    string currentController = helper.ViewContext.Controller.
                            ValueProvider.GetValue("controller").RawValue.ToString().ToLower();

                    string cssClassToUse = currentController.Contains(ControllerPrefix.ToLower()) ? "open" : string.Empty;

                    return cssClassToUse;
                }
                */




        //public static string ActiveTab(this HtmlHelper helper, string ParentDescription, String activeMenu)
        //{
        //    string cssClassToUse;
        //    Db db = new Db();
        //    //Select(o => new SelectListItem { o. })
        //  //  var obj = db.sec_object_structure.SelectMany(o => new SelectListItem { o.description }).ToList().Where(o => o.parent_structure.description == ParentDescription).ToString();

        //    string currentController = helper.ViewContext.Controller.
        //           ValueProvider.GetValue("controller").RawValue.ToString().ToLower();

        //    var obj = (from sec in db.sec_object_structure
        //                 .Where(o => o.viewname == currentController)
        //               select new { sec.parent_structure.description ,sec.parent_structure}
        //                  ).ToList();

        //    string expectedParentDescription=string.Empty;
        //    if (activeMenu.Equals("dynamic"))
        //    {
        //        foreach (var item in obj)
        //        {
        //            expectedParentDescription = item.description;

        //        }


        //        if (expectedParentDescription.Equals(ParentDescription, StringComparison.OrdinalIgnoreCase))
        //        { cssClassToUse = "active open"; }
        //        else { cssClassToUse = null; }
        //    }
        //    else
        //    {
        //        var obj1 = (from sec in db.sec_object_structure
        //               .Where(o => o.viewname == "Report" && o.viewpage == activeMenu)
        //                   select new { sec.parent_structure.description, sec.parent_structure }
        //                ).ToList();

        //        foreach (var item in obj1)
        //        {
        //            expectedParentDescription = item.description;

        //        }

        //        if (expectedParentDescription.Equals(ParentDescription, StringComparison.OrdinalIgnoreCase))
        //        { cssClassToUse = "active open"; }
        //        else { cssClassToUse = null; }
        //    }





        //    return cssClassToUse;
        //}



        public static MvcHtmlString MenuItem(
            this HtmlHelper htmlHelper,
            string text,
            string title,
            string action,
            string controller,
            object li_htmlAttributes = null,
            object routeValues = null,
            object a_htmlAttributes = null,
            bool isparent = false
        )
        {
            var li = new TagBuilder("li");


            var context = htmlHelper.ViewContext;
            if (context.Controller.ControllerContext.IsChildAction)
                context = htmlHelper.ViewContext.ParentActionViewContext;
            var routeData = context.RouteData;
            string currentController;
            if (context.TempData.ContainsKey("MenuTag"))
                currentController = context.TempData["MenuTag"].ToString();
            else
                //var currentAction = routeData.GetRequiredString("action");
                currentController = routeData.GetRequiredString("controller");

            li.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(li_htmlAttributes)));
            //if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
            //    string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            if (string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active");
            }
            //generate a unique id for the holder and convert it to string
            string holder = Guid.NewGuid().ToString();
            string anchor = htmlHelper.NoEncodeActionLink(holder, title, action, controller, routeValues, a_htmlAttributes, isparent).ToHtmlString();
            //replace the holder string with the html
            li.InnerHtml = anchor.Replace(holder, text);
            return MvcHtmlString.Create(li.ToString());
        }



        public static string MenuOpen(this HtmlHelper htmlHelper, string module)
        {
            Boolean returnActive = false;
            var context = htmlHelper.ViewContext;
            if (context.TempData.ContainsKey("Module") )
                returnActive = module.Equals(context.TempData["Module"].ToString(), StringComparison.OrdinalIgnoreCase);

            return returnActive ? "active open" : "";
        }


        // As the text the: "<span class='glyphicon glyphicon-plus'></span>" can be entered
        public static MvcHtmlString NoEncodeActionLink(this HtmlHelper htmlHelper,
                                             string text, string title, string action,
                                             string controller,
                                             object routeValues = null,
                                             object htmlAttributes = null,
                                             bool isparent = false)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = text;
            builder.Attributes["title"] = title;
            builder.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            if (!isparent) { builder.Attributes["data-load"] = ""; }
            builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));

            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString Button(this HtmlHelper helper,
                                     string innerHtml,
                                     object htmlAttributes,
                                     string message = null)
        {
            var builder = new TagBuilder("button");
            builder.InnerHtml = innerHtml;
            if (message != null && !String.IsNullOrWhiteSpace(message))
            {
                string[] msgarray = message.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (msgarray.Length > 1)
                {
                    builder.Attributes["data-plugin"] = "sweetalert";
                    builder.Attributes["data-type"] = msgarray[0];
                    builder.Attributes["data-text"] = msgarray[1];
                }
            }
            builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));
            return MvcHtmlString.Create(builder.ToString());

        }

        public static MvcHtmlString Button(this HtmlHelper helper,
                                           string innerHtml,
                                           IDictionary<string, object> htmlAttributes
                                           )
        {
            var builder = new TagBuilder("button");
            builder.InnerHtml = innerHtml;
            builder.MergeAttributes(htmlAttributes);
            return MvcHtmlString.Create(builder.ToString());
        }




    }
}