//-------------------------------------------------------------------------------------------------
// <copyright file="ChartFactory.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  This is factory class which is used to create and format data for displaying charts
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Models.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Nootus.Fabric.Web.Core.SqlServer.Repositories;

    public static class ChartFactory
    {
        public static async Task<ChartModel<TX, TY>> Create<TX, TY, TContext>(WidgetOptions options, BaseDbContext<TContext> dbContext, string sql, params object[] parameters)
            where TContext : DbContext
        {
            List<ChartEntity<TX, TY>> data = await dbContext.FromSql<ChartEntity<TX, TY>>(sql, parameters)
                                    .Select(m => m).ToListAsync();

            return CreateChartModel<TX, TY>(data, options.XAxisLabel, options.YAxisLabel);
        }

        private static ChartModel<TX, TY> CreateChartModel<TX, TY>(List<ChartEntity<TX, TY>> data, string xAxisLabel, string yAxisLabel)
        {
            List<ChartDataModel<TX, TY>> dataModel = MapChartData<TX, TY>(data);

            ChartModel<TX, TY> model = (ChartModel<TX, TY>)CreateChartModel(new ChartModel<TX, TY>(), dataModel, xAxisLabel, yAxisLabel);

            // setting up the x data axis labels
            model.XAxisDataLabels = SetXAsisDataLables(model.Data);

            return model;
        }

        private static ChartModel<TX, TY> CreateChartModel<TX, TY>(ChartModel<TX, TY> model, List<ChartDataModel<TX, TY>> data, string xAxisLabel, string yAxisLabel)
        {
            // ensuring that all X exist in the data
            IEnumerable<ChartPointModel<TX, TY>> xList = data.SelectMany(m => m.Values).Select(v => new ChartPointModel<TX, TY>() { X = v.X, Y = default(TY), Order = v.Order }).Distinct(new ChartPointModelComparer<TX, TY>());

            foreach (var item in data)
            {
                IEnumerable<ChartPointModel<TX, TY>> missing = xList.Except(item.Values, new ChartPointModelComparer<TX, TY>());

                // adding the missing
                foreach (var missValue in missing)
                {
                    item.Values.Add(missValue);
                }

                item.Values = item.Values.OrderBy(o => o.Order).ThenBy(o => o.X).ToList();
            }

            model.Data = data;
            model.XAxisLabel = xAxisLabel;
            model.YAxisLabel = yAxisLabel;

            return model;
        }

        private static List<ChartDataModel<TX, TY>> MapChartData<TX, TY>(List<ChartEntity<TX, TY>> data)
        {
            Dictionary<string, ChartDataModel<TX, TY>> dict = new Dictionary<string, ChartDataModel<TX, TY>>();

            foreach (var entity in data)
            {
                string key = entity.Key;

                if (!dict.ContainsKey(key))
                {
                    dict.Add(key, new ChartDataModel<TX, TY>() { Key = key, Order = entity.KeyOrder, Values = new List<ChartPointModel<TX, TY>>() });
                }

                dict[key].Values.Add(
                        new ChartPointModel<TX, TY>()
                        {
                            X = entity.X,
                            Y = entity.Y,
                            Order = entity.XOrder,
                        });
            }

            return dict.Values.ToList();
        }

        private static List<TX> SetXAsisDataLables<TX, TY>(List<ChartDataModel<TX, TY>> data)
        {
            List<TX> dataLabels = new List<TX>();
            int index = -1;

            foreach (var item in data)
            {
                foreach (var value in item.Values)
                {
                    TX x = value.X;
                    index = dataLabels.IndexOf(x);
                    if (index == -1)
                    {
                        dataLabels.Add(x);
                        index = dataLabels.Count - 1;
                    }

                    value.X = (TX)Convert.ChangeType(index.ToString(), typeof(TX));
                }
            }

            return dataLabels;
        }
    }
}
