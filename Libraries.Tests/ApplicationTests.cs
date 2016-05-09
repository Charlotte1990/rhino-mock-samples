using Libraries.Components;
using Libraries.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Libraries.Tests
{
    [TestClass]
    public class ApplicationTests
    {
        private IDependency mockService;
        private Application application;

        [TestInitialize()]
        public void Startup()
        {
            mockService = MockRepository.GenerateStub<IDependency>();
            application = new Application(mockService);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            mockService = null;
            application = null;
        }


        [TestMethod()]
        public void Should_CallDependencyWithParameters()
        {

            var expectedFirstParameter = "any string";
            var expectedSecondParameter = "any other string";


            application.ReturnStringWithTwoParameters(expectedFirstParameter, expectedSecondParameter);
            

            var parameters = mockService.GetArgumentsForCallsMadeOn(service =>
                service.ReturnStringWithTwoParameters(Arg<string>.Is.Anything, Arg<string>.Is.Anything));

            // the method was called once
            var expectedCallsCountMade = 1;
            Assert.AreEqual(parameters.Count, expectedCallsCountMade);

            Assert.AreEqual(parameters[0][0], expectedFirstParameter);
            Assert.AreEqual(parameters[0][1], expectedSecondParameter);
        }

    }
}
