using NSubstitute.Core;

namespace NSubstitute.Routes.Handlers
{
    public class DoActionsCallHandler :ICallHandler
    {
        private readonly ICallActions _callActions;
        private readonly ICallInfoFactory _callInfoFactory;

        public DoActionsCallHandler(ICallActions callActions, ICallInfoFactory callInfoFactory)
        {
            _callActions = callActions;
            _callInfoFactory = callInfoFactory;
        }

        public object Handle(ICall call)
        {
            var actions = _callActions.MatchingActions(call);
            var callInfo = _callInfoFactory.Create(call);
            foreach (var action in actions)
            {
                action(callInfo);
            }
            return null;
        }
    }
}