﻿namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.AcceptedTests
{
    using Xunit;
    using Setups;
    using Setups.Controllers;
    using Exceptions;
    using Utilities;

    public class AcceptedTestBuilderTests
    {
        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AcceptedAction())
                .ShouldReturn()
                .Accepted(accepted => accepted
                    .WithStatusCode(202));
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AcceptedAction())
                .ShouldReturn()
                .Accepted(accepted => accepted
                    .WithStatusCode(HttpStatusCode.Accepted));
        }

        [Fact]
        public void WithStatusCodeShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<AcceptedResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AcceptedAction())
                        .ShouldReturn()
                        .Accepted(accepted => accepted
                            .WithStatusCode(415));
                },
                "When calling AcceptedAction action in MvcController expected accepted result to have 415 (UnsupportedMediaType) status code, but instead received 202 (Accepted).");
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<AcceptedResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AcceptedAction())
                        .ShouldReturn()
                        .Accepted(accepted => accepted
                            .WithStatusCode(HttpStatusCode.UnsupportedMediaType));
                },
                "When calling AcceptedAction action in MvcController expected accepted result to have 415 (UnsupportedMediaType) status code, but instead received 202 (Accepted).");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullAcceptedAction())
                .ShouldReturn()
                .Accepted(accepted => accepted
                    .WithStatusCode(HttpStatusCode.Accepted)
                    .AndAlso()
                    .ContainingContentType(ContentType.ApplicationJson));
        }
    }
}
