using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using System;
using CleanArchitecture.Application.Sales.Commands.GenerateSalesReport;
using CleanArchitecture.Application.Sales.Queries.GetHighestSellingItemCategoryQuery;

namespace CleanArchitecture.Application.IntegrationTests.Sales
{
    using static Testing;
    public class GetHighestSellingItemCategoryTest : TestBase
    {
        [Test]
        public async Task ShouldGetHighestSellingItemCategory()
        {
            var sales = await SendAsync(new GetHighestSellingItemCategoryQuery
            {
                TimeRange = new DateTime()
            });
            sales.Should().NotBeNull();
        }
    }
}
