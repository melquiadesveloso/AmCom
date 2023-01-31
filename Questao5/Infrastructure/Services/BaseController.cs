using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Questao5.Infrastructure.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        #region Property
        private IMediator _mediator;
        #endregion
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
