namespace NSubstitute.Core
{
    public interface IResultSetter
    {
        void SetResultForLastCall<T>(T valueToReturn);
        void SetResultForCall<T>(ICall call, T valueToReturn);
    }
}