using LogHelperClass;
using PortalSite.Interface.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace PortalSite.Interface.Filter
{
    public class RequestInfoFilter: ActionFilterAttribute
    {
        private const string Key = "action";
        private bool _IsDebugLog = true;
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (_IsDebugLog)
            {
                Stopwatch stopWatch = new Stopwatch();

                actionContext.Request.Properties[Key] = stopWatch;

                string actionName = actionContext.ActionDescriptor.ActionName;
                //Debug.Print(Newtonsoft.Json.JsonConvert.SerializeObject(actionContext.ActionArguments));

                //LogHelper.WriteInfo(string.Format(@"请求的url:{0},参数为：{1}", actionContext.Request.RequestUri.AbsoluteUri, actionContext.ActionArguments));
                stopWatch.Start();
            }

        }
        private string FormatArguments(Dictionary<string, object> args)
        {
            string argstring = "";
            if (args == null)
            {
                return "";
            }
            foreach (var item in args)
            {
                if (item.Value is QuerySearchModel)
                {
                    QuerySearchModel model = (QuerySearchModel)item.Value;
                    argstring += string.Format("{0}@{1}@{2}@{3}@{4}", model.queryExp, model.restype, model.rows, model.sortExp, model.groupName);
                }
                else
                {
                    argstring += string.Format("{0}----{1}&", item.Key, item.Value == null ? "" : item.Value.ToString());
                }
            }
            return argstring;
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (_IsDebugLog)
            {
                Stopwatch stopWatch = actionExecutedContext.Request.Properties[Key] as Stopwatch;

                if (stopWatch != null)
                {
                    try
                    {
                        stopWatch.Stop();
                        string actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
                        string controllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                        LogHelper.WriteInfo(string.Format(@"[{0} 用时 {1}ms] {2},请求的参数为:{3},{4}请求的结果为：{5}{6}",
                                                          actionExecutedContext.Request.RequestUri.AbsoluteUri,
                                                          stopWatch.Elapsed.TotalMilliseconds,
                                                          Environment.NewLine,
                                                          FormatArguments(actionExecutedContext.ActionContext.ActionArguments),
                                                          Environment.NewLine,
                                                          actionExecutedContext.Response.Content.ReadAsStringAsync().Result,
                                                          Environment.NewLine));
                    }
                    catch (Exception e)
                    {
                        LogHelper.WriteError("请求出错了!!!");
                    }
                }
            }
        }
    }
}