using System;
using System.Collections.Generic;
using System.Linq;
using Advantage.API.Models;

namespace Advantage.API
{
    public class DataSeed
    {
        private readonly ApiContext ctx;
        public DataSeed(ApiContext ctx)
        {
            this.ctx = ctx;
        }

        public void SeedData(int nCustomers, int nOrders) {
            if(!this.ctx.Customers.Any())
            {
                SeedCustomers(nCustomers);
                this.ctx.SaveChanges();
            }

            if(!this.ctx.Orders.Any())
            {
                SeedOrders(nOrders);
                this.ctx.SaveChanges();
            }

            if(!this.ctx.Servers.Any())
            {
                SeedServer();
                this.ctx.SaveChanges();
            }            
        }

        private void SeedCustomers(int n)
        {
            List<Customer> customers = BuildCustomerList(n);
            foreach (var customer in customers)
            {
                this.ctx.Customers.Add(customer);
            }
        }

        private void SeedOrders(int n)
        {
            List<Order> Orders = BuildOrderList(n);
            foreach (var order in Orders)
            {
                this.ctx.Orders.Add(order);
            }
        }

        private void SeedServer()
        {
            List<Server> servers = BuildServerList();
            foreach (var server in servers)
            {
                this.ctx.Servers.Add(server);
            }
        }

        private List<Customer> BuildCustomerList(int nCustomers)
        {
            var customer = new List<Customer>();
            var names = new List<string>();

            for (int i = 1; i < nCustomers; i++)
            {
                var name = Helpers.MakeUniqueCustomerName(names);
                names.Add(name);

                customer.Add(new Customer {
                    Id=i,
                    Name = name,
                    Email = Helpers.MakeCustomerEmail(name),
                    State = Helpers.GetRandomState()
                });
            }

            return customer;
        }

        private List<Order> BuildOrderList(int nOrders){
            var orders = new List<Order>();
            var rand = new Random();

            for (int i = 1; i < nOrders; i++)
            {
                var randCustomerId = rand.Next(1, this.ctx.Customers.Count());
                var placed = Helpers.GetRandomOrderPlaced();
                var completed = Helpers.GetRandomOrderCompleted(placed);
                var customers = this.ctx.Customers.ToList();

                orders.Add(new Order{
                    Id=i,
                    Customer = customers.First(c => c.Id == randCustomerId),
                    Total = Helpers.GetRandomOrderTotal(),
                    Placed = placed,
                    Completed = completed
                });
            }

            return orders;
        }

        private List<Server> BuildServerList()
        {
            return new List<Server>()
            {
                new Server{
                    Id=1,
                    Name="Dev-Web",
                    IsOnline=true
                },
                new Server{
                    Id=2,
                    Name="Dev-Mail",
                    IsOnline=false
                },
                new Server{
                    Id=3,
                    Name="Dev-Services",
                    IsOnline=true
                },
                new Server{
                    Id=4,
                    Name="QA-Web",
                    IsOnline=false
                },
                new Server{
                    Id=5,
                    Name="QA-Mail",
                    IsOnline=true
                },
                new Server{
                    Id=6,
                    Name="QA-Services",
                    IsOnline=true
                },
                new Server{
                    Id=7,
                    Name="Prod-Web",
                    IsOnline=true
                },
                new Server{
                    Id=8,
                    Name="Prod-Mail",
                    IsOnline=false
                },
                new Server{
                    Id=9,
                    Name="Prod-Services",
                    IsOnline=true
                }
                
            };
        }        
    }
}