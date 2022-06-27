using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rbac.Application;
using Rbac.Application.VFCode;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VFCodeController : ControllerBase
    {
        private readonly IServiceCode code;

        public VFCodeController(IServiceCode code)
        {
            this.code = code;
        }
        //验证码
        [HttpGet]
        public async Task<FileContentResult> CaptchaAsync()
        {
            var code = await this.code.GenerateRandomCaptchaAsync();
            var result = await this.code.GenerateCaptchaImageAsync(code);
            //HttpContext.Session.SetString("CodeKey", code);
            //SessionHelp session = new SessionHelp(accessor.HttpContext);
            //session.SetSession("CodeKey",code);
            //HttpContext.Session.GetString("CodeKey");
            return File(result.CaptchaMemoryStream.ToArray(), "image/png");
        }

    }
}
