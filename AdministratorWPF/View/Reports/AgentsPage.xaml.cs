﻿using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFAdministratorLib.View.Reports
{
    /// <summary>
    /// Логика взаимодействия для AgentsPage.xaml
    /// </summary>
    public partial class AgentsPage : Page
    {
        public AgentsPage()
        {
            InitializeComponent();

            DgAgents.ItemsSource = Calculate();
        }

        private PopularAgent[] Calculate()
        {
            List<Contract> contracts = DbConnect.Db.Contracts.Where(c => c.StatusId == (int)StatusKey.Active)
                .Include(c => c.Worker).ToList();

            List<PopularAgent> popularAgents = new();
            foreach (var contract in contracts)
            {
                PopularAgent? findAgent = popularAgents.FirstOrDefault(pa => pa.Worker.Id == contract.WorkerId);
                if (findAgent == null)
                {
                    PopularAgent popularAgent = new PopularAgent(contract.Worker!, 1);
                    popularAgents.Add(popularAgent);
                }
                else findAgent.Count++;
            }

            popularAgents = popularAgents.OrderByDescending(pa => pa.Count).ToList();

            int countInTop = 10;
            if (popularAgents.Count > countInTop)
                popularAgents.RemoveRange(countInTop, popularAgents.Count - countInTop);

            return popularAgents.ToArray();
        }

        public class PopularAgent
        {
            public Worker Worker { get; set; } = null!;
            public int Count { get; set; }

            public PopularAgent(Worker worker, int count)
            {
                Worker = worker;
                Count = count;
            }
        }
    }
}
