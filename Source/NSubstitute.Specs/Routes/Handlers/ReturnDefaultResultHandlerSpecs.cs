using NSubstitute.Core;
using NSubstitute.Routes.Handlers;
using NSubstitute.Specs.Infrastructure;
using NUnit.Framework;

namespace NSubstitute.Specs.Routes.Handlers
{
    public class ReturnDefaultResultHandlerSpecs
    {
        public class When_handling_a_call : ConcernFor<ReturnDefaultResultHandler>
        {
            readonly object _defaultReturnForCall = new object();
            ICallResults _callResults;
            ICall _call;
            object _result;

            [Test]
            public void Should_return_default_for_call()
            {
                Assert.That(_result, Is.SameAs(_defaultReturnForCall));
            }

            public override void Because()
            {
                _result = sut.Handle(_call);
            }

            public override void Context()
            {
                _call = mock<ICall>();
                _callResults = mock<ICallResults>();
                _callResults.stub(x => x.GetDefaultResultFor(_call)).Return(_defaultReturnForCall);
            }

            public override ReturnDefaultResultHandler CreateSubjectUnderTest()
            {
                return new ReturnDefaultResultHandler(_callResults);
            }
        }
    }
}