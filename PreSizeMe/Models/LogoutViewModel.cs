using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PreSizeMe.Models
{
    public class LogoutViewModel
    {
        [BindNever]
        public string RequestId { get; set; }
    }
}