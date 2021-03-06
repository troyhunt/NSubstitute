﻿using NSubstitute.Core;
using NSubstitute.Routes.Handlers;

namespace NSubstitute.Routes
{
    public class RecordReplayRoute : IRoute
    {
        private readonly IRouteParts _routeParts;

        public RecordReplayRoute(IRouteParts routeParts)
        {
            _routeParts = routeParts;
        }

        public object Handle(ICall call)
        {
            _routeParts.GetPart<EventSubscriptionHandler>().Handle(call);
            _routeParts.GetPart<PropertySetterHandler>().Handle(call);
            _routeParts.GetPart<DoActionsCallHandler>().Handle(call);
            _routeParts.GetPart<RecordCallHandler>().Handle(call);
            return _routeParts.GetPart<ReturnConfiguredResultHandler>().Handle(call);
        }
    }
}