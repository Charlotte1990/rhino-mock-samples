
namespace Libraries.Interfaces
{
    public interface IDependency
    {
        void ReturnVoidWithoutParameter();
        int ReturnIntWithoutParameter();
        string ReturnStringWithOneParameter(string text);
        string ReturnStringWithTwoParameters(string first, string second);
        void ReturnVoidWithRefParameter(ref string text);
        T ReturnGenericWithoutParameter<T>(T input);
    }
}
