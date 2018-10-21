//-------------------------------------------------------------------------------------------------
// <copyright file="AjaxHelper.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Helpers methods used to generate standard object, AjaxModel, that can be consumed in Angular
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Helpers.Web
{
    using System;
    using System.Threading.Tasks;
    using Nootus.Fabric.Web.Core.Context;
    using Nootus.Fabric.Web.Core.Exception;
    using Nootus.Fabric.Web.Core.Models.Web;

    public static class AjaxHelper
    {
        public static async Task<AjaxModel<T>> GetAsync<T>(Func<string, Task<T>> action, string message = "")
            where T : class
        {
            AjaxModel<T> ajax;

            try
            {
                T model = await action(null);
                ajax = new AjaxModel<T>() { Result = AjaxResult.Success, Model = model, Message = message };
            }
            catch (NTException exp)
            {
                ajax = new AjaxModel<T>() { Result = AjaxResult.ValidationException, Model = null, Errors = exp.Errors, Message = exp.GetBaseException().Message };
            }
            catch (System.Exception exp)
            {
                ajax = new AjaxModel<T>() { Result = AjaxResult.Exception, Model = null, Message = exp.GetBaseException().Message };
            }

            return ajax;
        }

        public static async Task<AjaxModel<NTModel>> SaveAsync(Func<string, Task> action, string message)
        {
            AjaxModel<NTModel> ajax;

            try
            {
                await action(null);
                ajax = new AjaxModel<NTModel>() { Result = AjaxResult.Success, Model = null, Message = message };
            }
            catch (NTException exp)
            {
                ajax = new AjaxModel<NTModel>() { Result = AjaxResult.ValidationException, Model = null, Errors = exp.Errors, Message = exp.GetBaseException().Message };
            }
            catch (System.Exception exp)
            {
                ajax = new AjaxModel<NTModel>() { Result = AjaxResult.Exception, Model = null, Message = exp.GetBaseException().Message };
            }

            return ajax;
        }

        public static async Task<AjaxModel<T>> SaveGetAsync<T>(Func<string, Task<T>> action, string message)
            where T : class
        {
            return await GetAsync(action, message);
        }

/*
        public static async Task<AjaxModel<object>> GetDashboardAsync(BaseDashboardDomain dashboardDomain, IWidgetDomain widgetDomain)
        {
            var ajaxModel = AjaxHelper.BlankModel<object>();
            ajaxModel.Dashboard = await AjaxHelper.DashboardGet(dashboardDomain.GetWidgetData, widgetDomain);
            return ajaxModel;
        }

        public static async Task<AjaxModel<T>> GetDashboardAsync<T>(Func<string, Task<T>> action, BaseDashboardDomain dashboardDomain, IWidgetDomain widgetDomain)
            where T : class
        {
            var ajaxModel = await AjaxHelper.GetAsync(action);
            ajaxModel.Dashboard = await AjaxHelper.DashboardGet(dashboardDomain.GetWidgetData, widgetDomain);
            return ajaxModel;
        }

        public static async Task<DashboardModel> DashboardGet(Func<int, WidgetOptions, Task<ChartModel<string, int>>> widgetData, IWidgetDomain widgetDomain)
        {
            int? dashboardPageId = NTContext.Context.DashboardPageId;
            if (dashboardPageId != null)
            {
                DashboardModel dashboard = await widgetDomain.DashboardGet(dashboardPageId.Value);
                foreach (WidgetModel widget in dashboard.AllWidgets)
                {
                    WidgetOptions options = new WidgetOptions()
                    {
                        XAxisLabel = widget.XAxisLabel,
                        YAxisLabel = widget.YAxisLabel,
                    };
                    widget.Chart.Model = await widgetData(widget.WidgetId, options);
                }

                return dashboard;
            }
            else
            {
                return null;
            }
        }
*/
        public static AjaxModel<T> BlankModel<T>()
            where T : class
        {
            return new AjaxModel<T>() { Result = AjaxResult.Success, Model = null, Message = string.Empty };
        }
    }
}
