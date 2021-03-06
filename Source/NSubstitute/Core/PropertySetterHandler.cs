using System.Linq;

namespace NSubstitute.Core
{
    public class PropertySetterHandler : ICallHandler
    {
        private readonly IPropertyHelper _propertyHelper;
        readonly IResultSetter _resultSetter;

        public PropertySetterHandler(IPropertyHelper propertyHelper, IResultSetter resultSetter)
        {
            _propertyHelper = propertyHelper;
            _resultSetter = resultSetter;
        }

        public object Handle(ICall call)
        {
            if (_propertyHelper.IsCallToSetAReadWriteProperty(call))
            {
                var callToPropertyGetter = _propertyHelper.CreateCallToPropertyGetterFromSetterCall(call);
                var valueBeingSetOnProperty = call.GetArguments().First();
                _resultSetter.SetResultForCall(callToPropertyGetter, valueBeingSetOnProperty);
            }
            return null;
        }
    }
}