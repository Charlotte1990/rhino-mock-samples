using Libraries.Interfaces;

namespace Libraries.Components
{
    public class Dependency : IDependency
    {
        public void ReturnVoidWithoutParameter() { }
        public int ReturnIntWithoutParameter()
        {
            return 42;
        }
        public string ReturnStringWithOneParameter(string text)
        {
            return text.ToUpper();
        }
        public string ReturnStringWithTwoParameters(string first, string second)
        {
            return string.Format("{0}{1}", first, second);
        }
        public void ReturnVoidWithRefParameter(ref string text)
        {
            text = "hello";
        }
        public T ReturnGenericWithoutParameter<T>(T input)
        {
            return input;
        }
    }
}
