﻿namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.Actions;
    using Builders.Contracts.Pipeline;
    using Builders.Contracts.Routing;
    using Builders.Pipeline;
    using Builders.Routing;
    using Internal.Results;
    using Internal.TestContexts;

    /// <summary>
    /// Contains extension methods for <see cref="IControllerRouteTestBuilder{TController}"/>.
    /// </summary>
    public static class ControllerRouteTestBuilderPipelineExtensions
    {
        /// <summary>
        /// Allows the route test to continue the assertion chain on the matched controller action.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <param name="builder">Instance of <see cref="IControllerRouteTestBuilder{TController}"/> type.</param>
        /// <returns>Test builder of <see cref="IWhichControllerInstanceBuilder{TController}"/> type.</returns>
        public static IWhichControllerInstanceBuilder<TController> Which<TController>(
            this IControllerRouteTestBuilder<TController> builder)
            where TController : class
            => (IWhichControllerInstanceBuilder<TController>)builder.Which(null);

        /// <summary>
        /// Allows the route test to continue the assertion chain on the matched controller action.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <param name="builder">Instance of <see cref="IControllerRouteTestBuilder{TController}"/> type.</param>
        /// <param name="controllerInstanceBuilder">Builder for creating the controller instance.</param>
        /// <returns>Test builder of <see cref="IActionResultTestBuilder{TActionResult}"/> type.</returns>
        public static IActionResultTestBuilder<MethodResult> Which<TController>(
            this IControllerRouteTestBuilder<TController> builder,
            Action<IWhichControllerInstanceBuilder<TController>> controllerInstanceBuilder)
            where TController : class
        {
            var actualBuilder = (ControllerRouteTestBuilder<TController>)builder;

            var actionCall = actualBuilder.ActionCallExpression;

            var whichControllerInstanceBuilder = new WhichControllerInstanceBuilder<TController>(new ControllerTestContext
            {
                ComponentConstructionDelegate = () => null,
                MethodCall = actionCall
            });

            controllerInstanceBuilder?.Invoke(whichControllerInstanceBuilder);

            return whichControllerInstanceBuilder;
        }
    }
}