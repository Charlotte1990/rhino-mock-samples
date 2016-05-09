using System;
using Libraries.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace Libraries.Tests
{
    [TestClass]
    public class MockExamples
    {
        private IDependency mockService;

        [TestInitialize()]
        public void Startup()
        {
            mockService = MockRepository.GenerateStub<IDependency>();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            mockService = null;
        }


        [TestMethod()]
        public void HowTo_MockResponse_ForSpecificParameter()
        {
            var specificParameter = "any string";
            var shouldReturn = "should return";

            // how to:
            mockService.Stub(service => service.ReturnStringWithOneParameter(specificParameter))
                .Return(shouldReturn);

            // should return expected value for specific parameter
            var actual = mockService.ReturnStringWithOneParameter(specificParameter);
            Assert.AreEqual(shouldReturn, actual);

            // should return default value for any other parameter
            var anyOtherParameter = "any other string";
            actual = mockService.ReturnStringWithOneParameter(anyOtherParameter);
            Assert.AreEqual(default(string), actual);
        }

        [TestMethod()]
        public void HowTo_MockResponse_ForAnyParameter()
        {
            var shouldReturn = "should return";

            // how to:
            mockService.Stub(service => service.ReturnStringWithTwoParameters(Arg<string>.Is.Anything, Arg<string>.Is.Anything))
                .Return(shouldReturn);

            // should return expected value for any parameters
            var actual = mockService.ReturnStringWithTwoParameters(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            Assert.AreEqual(shouldReturn, actual);

        }

        [TestMethod()]
        public void HowTo_MockResponse_BasedOnParameter()
        {

            // how to:
            mockService.Stub(service => service.ReturnStringWithOneParameter(Arg<string>.Is.Anything))
                .Return(null)
                .WhenCalled(method =>
                {
                    var param = method.Arguments[0] as string;
                    method.ReturnValue = param.ToUpper();
                });

            // should return expected value based on input
            var input = "star wars";
            var expected = input.ToUpper();
            var actual = mockService.ReturnStringWithOneParameter(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        // place an attribute here
        [ExpectedException(typeof(NotImplementedException))]
        public void HowTo_MockException()
        {

            // how to:
            mockService.Stub(service => service.ReturnVoidWithoutParameter())
                .Throw(new NotImplementedException());

            mockService.ReturnVoidWithoutParameter();
        }

        [TestMethod()]
        public void HowTo_Mock_GenericMethod()
        {
            var shouldReturn = "should return";

            mockService.Stub(service => service.ReturnGenericWithoutParameter<string>(Arg<string>.Is.Anything))
                .Return(shouldReturn);

            var actual = mockService.ReturnGenericWithoutParameter<string>(Guid.NewGuid().ToString());
            Assert.AreEqual(shouldReturn, actual);
        }

        [TestMethod()]
        public void HowTo_Mock_ForRefParameter()
        {
            var shouldReturn = "should return";
            mockService.Stub(service => service.ReturnVoidWithRefParameter(ref Arg<string>.Ref(Is.Anything(), shouldReturn).Dummy));
            var input = "any string";
            mockService.ReturnVoidWithRefParameter(ref input);
            Assert.AreEqual(shouldReturn, input);
        }

        [TestMethod()]
        public void HowTo_Assert_MethodWasCalled()
        {
            mockService.ReturnIntWithoutParameter();

            mockService.AssertWasCalled(service => service.ReturnIntWithoutParameter());
            mockService.AssertWasNotCalled(service => service.ReturnVoidWithoutParameter());
        }

        [TestMethod()]
        public void HowTo_Assert_MethodWasCalled_CertainTimes()
        {

            mockService.ReturnIntWithoutParameter();
            mockService.ReturnIntWithoutParameter();

            mockService.AssertWasCalled(service => service.ReturnIntWithoutParameter(), o => o.Repeat.Times(2));
            mockService.AssertWasCalled(service => service.ReturnIntWithoutParameter(), o => o.Repeat.Twice());
            mockService.AssertWasCalled(service => service.ReturnIntWithoutParameter(), o => o.Repeat.AtLeastOnce());
        }
    }
}
