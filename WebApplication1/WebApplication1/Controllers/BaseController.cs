using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rbac.Application;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController<TService, TEntity, TDTO> : ControllerBase
        where TService : IBaseService<TEntity,TDTO>
        where TEntity : class,new ()
        where TDTO : class,new ()

    {
        private readonly TService service;
        public BaseController(TService service)
        {
            this.service = service;
        }
        [HttpPost]
        public ActionResult Add(TDTO dTo)
        {
            return Ok(service.Add(dTo));
        }

        [HttpPost]
        public ActionResult UpdateAll(TDTO dto)
        {
            return Ok(service.Update(dto));
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return Ok(service.Delete(id));
        }
        [HttpGet]
        public ActionResult FindOne(int id)
        {
            return new JsonResult(service.FindOne(id));
        }
        [HttpGet]
        public ActionResult GetList()
        {
            return new JsonResult(service.GetList());
        }
    }
}
