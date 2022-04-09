using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;
using static System.Net.WebRequestMethods;

namespace ExpenseTrackerApi.IntegrationTests
{
    public class GeneralntegrationTests
    {
        private readonly string mockApiBaseUrl = "http://localhost:5001/api/Expenses";

        [Fact]
        public async void GetExpenses_WithNormalState_ReturnsOk()
        {
            await using var application = new WebApplicationFactory<Startup>();
            using var client = application.CreateClient();

            var response = await client.GetAsync($"{mockApiBaseUrl}/expenseslist");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void GetExpensesByMonth_WithNormalState_ReturnsOk()
        {
            await using var application = new WebApplicationFactory<Startup>();
            using var client = application.CreateClient();

            var response = await client.GetAsync($"{mockApiBaseUrl}/expenseslistbymonth/2022-03");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void GetSingleExpense_WithNormalState_ReturnsOk()
        {
            await using var application = new WebApplicationFactory<Startup>();
            using var client = application.CreateClient();
            int singleExpenseId = 1;

            var response = await client.GetAsync($"{mockApiBaseUrl}/1");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}