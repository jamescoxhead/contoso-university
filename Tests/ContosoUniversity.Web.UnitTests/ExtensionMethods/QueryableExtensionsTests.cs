using AutoFixture.NUnit3;
using ContosoUniversity.Web.ExtensionMethods;
using FluentAssertions.Execution;
using MockQueryable.NSubstitute;

namespace ContosoUniversity.Web.UnitTests.ExtensionMethods;

public class QueryableExtensionsTests
{
    public class TestDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }

    [Test]
    [AutoData]
    public async Task ToPaginatedListAsyncShouldCreatePaginatedList(Generator<TestDto> inputGenerator)
    {
        var pageSize = 5;
        var pageIndex = 0;
        var mockQueryable = inputGenerator.Take(20).BuildMock();
        var expectedTotalPages = (int)Math.Ceiling(mockQueryable.Count() / (double)pageSize);

        var resultList = await mockQueryable.ToPaginatedListAsync(pageIndex, pageSize);

        using (new AssertionScope())
        {
            resultList.Should().NotBeNullOrEmpty();
            resultList.Count.Should().Be(5);
            resultList.PageIndex.Should().Be(pageIndex);
            resultList.TotalPages.Should().Be(expectedTotalPages);
        }
    }
}
