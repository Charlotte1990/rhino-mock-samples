using Libraries.Interfaces;

namespace Libraries.Components
{
    public class Application
    {
        private IDependency service;
        public Application(IDependency service) {
            this.service = service;
        }

        public void ReturnVoidWithoutParameter() {
            service.ReturnVoidWithoutParameter();
        }
        public int ReturnIntWithoutParameter() {
            return service.ReturnIntWithoutParameter();
        }
        public string ReturnStringWithOneParameter(string text) {
            return service.ReturnStringWithOneParameter(text);
        }
        public string ReturnStringWithParameter(string first, string second) {
            return service.ReturnStringWithTwoParameters(first, second);
        }
        public void ReturnVoidWithRefParameter(ref string text) {
            service.ReturnVoidWithRefParameter(ref text);
        }
        public T ReturnGenericWithoutParameter<T>(T input) {
            return service.ReturnGenericWithoutParameter<T>(input);
        }
    }
}
