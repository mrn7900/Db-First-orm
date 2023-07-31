using WebApplication1.Properties;

namespace WebApplication1.Services
{
    public class MethodResult : IMethodResult
    {
        public  string Errors { get; set; }
        public object Result { get; set; }
    }
}
