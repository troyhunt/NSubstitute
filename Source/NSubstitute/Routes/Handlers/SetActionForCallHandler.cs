using System;
using NSubstitute.Core;

namespace NSubstitute.Routes.Handlers
{
    public class SetActionForCallHandler :ICallHandler
    {
        private readonly ICallSpecificationFactory _callSpecificationFactory;
        private readonly ICallActions _callActions;
        private readonly Action<CallInfo> _action;

        public SetActionForCallHandler(ICallSpecificationFactory callSpecificationFactory, ICallActions callActions, Action<CallInfo> action)
        {
            _callSpecificationFactory = callSpecificationFactory;
            _callActions = callActions;
            _action = action;
        }

        public object Handle(ICall call)
        {
            var callSpec = _callSpecificationFactory.CreateFrom(call);
            _callActions.Add(callSpec, _action);
            return null;
        }
    }
}