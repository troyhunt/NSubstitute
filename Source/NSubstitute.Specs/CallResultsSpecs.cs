using System;
using NSubstitute.Core;
using NSubstitute.Specs.Infrastructure;
using NUnit.Framework;

namespace NSubstitute.Specs
{
    public class CallResultsSpecs
    {
        public abstract class Concern : ConcernFor<CallResults>
        {
            public override CallResults CreateSubjectUnderTest()
            {
                return new CallResults();
            }
        }

        public class When_getting_a_result_that_has_been_set : Concern
        {
            object _result;
            readonly object _expectedResult = new object();
            ICallSpecification _callSpecification;
            ICall _call;

            [Test]
            public void Should_get_the_result_that_was_set()
            {
                Assert.That(_result, Is.SameAs(_expectedResult));
            }

            public override void Because()
            {
                sut.SetResult(_callSpecification, _expectedResult);
                _result = sut.GetResult(_call);
            }

            public override void Context()
            {
                base.Context();
                _callSpecification = mock<ICallSpecification>();
                _call = mock<ICall>();
                _callSpecification.stub(x => x.IsSatisfiedBy(_call)).Return(true);
            }
        }

        public class When_getting_a_result_that_has_been_set_multiple_times : Concern
        {
            object _result;
            readonly object _originalResult = new object();
            readonly object _overriddenResult = new object();
            ICallSpecification _callSpecification;
            ICall _call;

            [Test]
            public void Should_get_the_last_result_that_was_set()
            {
                Assert.That(_result, Is.SameAs(_overriddenResult));
            }

            public override void Because()
            {
                sut.SetResult(_callSpecification, _originalResult);
                sut.SetResult(_callSpecification, _overriddenResult);
                _result = sut.GetResult(_call);
            }

            public override void Context()
            {
                base.Context();
                _callSpecification = mock<ICallSpecification>();
                _call = mock<ICall>();
                _callSpecification.stub(x => x.IsSatisfiedBy(_call)).Return(true);
            }
            
        }

        public class When_getting_a_reference_type_result_that_has_not_been_set : Concern
        {
            object _result;
            ICall _call;

            [Test]
            public void Should_use_the_default_value_for_the_result_type()
            {
                Assert.That(_result, Is.Null);
            }

            public override void Because()
            {
                _result = sut.GetResult(_call);
            }

            public override void Context()
            {
                base.Context();
                _call = mock<ICall>();
                _call.stub(x => x.GetReturnType()).Return(typeof(List));
            }
        }
        
        public class When_getting_a_value_type_result_that_has_not_been_set : Concern
        {
            object _result;
            ICall _call;

            [Test]
            public void Should_use_the_default_value_for_the_result_type()
            {
                Assert.That(_result, Is.EqualTo(default(int)));
            }

            public override void Because()
            {
                _result = sut.GetResult(_call);
            }

            public override void Context()
            {
                base.Context();
                _call = mock<ICall>();
                _call.stub(x => x.GetReturnType()).Return(typeof(int));
            }
        }

        public class When_getting_a_void_type_result : Concern
        {
            object _result;
            ICall _call;

            [Test]
            public void Should_return_null_because_there_is_no_void_instance()
            {
                Assert.That(_result, Is.Null);
            }

            public override void Because()
            {
                _result = sut.GetResult(_call);
            }

            public override void Context()
            {
                base.Context();
                _call = mock<ICall>();
                _call.stub(x => x.GetReturnType()).Return(typeof (void));
            }
        }

        public class When_getting_default_results : Concern
        {
            [Test]
            public void Should_return_null_for_reference_types()
            {
                var callThatReturnsReferenceType = CreateCallWithReturnType(typeof(string));
                var result = sut.GetDefaultResultFor(callThatReturnsReferenceType);
                Assert.That(result, Is.Null);
            }

            [Test]
            public void Should_return_default_for_value_types()
            {
                var callThatReturnsValueType = CreateCallWithReturnType(typeof(int));
                var result = sut.GetDefaultResultFor(callThatReturnsValueType);
                Assert.That(result, Is.EqualTo(default(int)));
            }

            [Test]
            public void Should_return_null_for_void_type()
            {
                var callThatReturnsVoidType = CreateCallWithReturnType(typeof (void));
                var result = sut.GetDefaultResultFor(callThatReturnsVoidType);
                Assert.That(result, Is.Null);
            }

            ICall CreateCallWithReturnType(Type type)
            {
                var call = mock<ICall>();
                call.stub(x => x.GetReturnType()).Return(type);
                return call;
            }
        }
    }
}