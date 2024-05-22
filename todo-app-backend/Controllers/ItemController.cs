using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todo_app_backend.Data;

namespace todo_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public ItemController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


    }
}
