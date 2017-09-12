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
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Repositories;

    public static class ChartFactory
    {
        public static async Task<ChartModel<Tx, Ty>> Create<Tx, Ty, TContext>(WidgetOptions options, BaseDbContext<TContext> dbContext, string sql, params object[] parameters)
            where TContext : DbContext
        {
            List<ChartEntity<Tx, Ty>> data = await dbContext.FromSql<ChartEntity<Tx, Ty>>(sql, parameters)
                                    .Select(m => m).ToListAsync();

            return CreateChartModel<Tx, Ty>(data, options.XAxisLabel, options.YAxisLabel);
        }

        private static ChartModel<Tx, Ty> CreateChartModel<Tx, Ty>(List<ChartEntity<Tx, Ty>> data, string xAxisLabel, string yAxisLabel)
        {
            List<ChartDataModel<Tx, Ty>> dataModel = MapChartData<Tx, Ty>(data);

            ChartModel<Tx, Ty> model = (ChartModel<Tx, Ty>)CreateChartModel(new ChartModel<Tx, Ty>(), dataModel, xAxisLabel, yAxisLabel);

            // setting up the x data axis labels
            model.XAxisDataLabels = SetXAsisDataLables(model.Data);

            return model;
        }

        private static ChartModel<Tx, Ty> CreateChartModel<Tx, Ty>(ChartModel<Tx, Ty> model, List<ChartDataModel<Tx, Ty>> data, string xAxisLabel, string yAxisLabel)
        {
            // ensuring that all X exist in the data
            IEnumerable<ChartPointModel<Tx, Ty>> xList = data.SelectMany(m => m.Values).Select(v => new ChartPointModel<Tx, Ty>() { X = v.X, Y = default(Ty), Order = v.Order }).Distinct(new ChartPointModelComparer<Tx, Ty>());

            foreach (var item in data)
            {
                IEnumerable<ChartPointModel<Tx, Ty>> missing = xList.Except(item.Values, new ChartPointModelComparer<Tx, Ty>());

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

        private static List<ChartDataModel<Tx, Ty>> MapChartData<Tx, Ty>(List<ChartEntity<Tx, Ty>> data)
        {
            Dictionary<string, ChartDataModel<Tx, Ty>> dict = new Dictionary<string, ChartDataModel<Tx, Ty>>();

            foreach (var entity in data)
            {
                string key = entity.Key;

                if (!dict.ContainsKey(key))
                {
                    dict.Add(key, new ChartDataModel<Tx, Ty>() { Key = key, Order = entity.KeyOrder, Values = new List<ChartPointModel<Tx, Ty>>() });
                }

                dict[key].Values.Add(
                        new ChartPointModel<Tx, Ty>()
                        {
                            X = entity.X,
                            Y = entity.Y,
                            Order = entity.XOrder,
                        });
            }

            return dict.Values.ToList();
        }

        private static List<Tx> SetXAsisDataLables<Tx, Ty>(List<ChartDataModel<Tx, Ty>> data)
        {
            List<Tx> dataLabels = new List<Tx>();
            int index = -1;

            foreach (var item in data)
            {
                foreach (var value in item.Values)
                {
                    Tx x = value.X;
                    index = dataLabels.IndexOf(x);
                    if (index == -1)
                    {
                        dataLabels.Add(x);
                        index = dataLabels.Count - 1;
                    }

                    value.X = (Tx)Convert.ChangeType(index.ToString(), typeof(Tx));
                }
            }

            return dataLabels;
        }
    }
}
