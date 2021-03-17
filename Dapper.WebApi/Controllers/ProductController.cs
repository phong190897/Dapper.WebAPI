using Dapper.Application;
using Dapper.Application.IRepositories;
using Dapper.Application.IServices;
using Dapper.Core.CustomEntities;
using Dapper.Core.Entities;
using Dapper.Core.QueryFilters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Dapper.WebApi.Controllers
{
    //[Authorize]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1")]
    [ApiVersion("2")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUriService _uriService;
        public ProductController(IUnitOfWork unitOfWork, IUriService uriService)
        {
            this._unitOfWork = unitOfWork;
            this._uriService = uriService;
        }

        [ApiVersion("1")]
        [MapToApiVersion("1")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.Products.GetAllAsync();
            return Ok(data);
        }

        [ApiVersion("2")]
        [MapToApiVersion("2")]
        [HttpGet(Name = nameof(GetAllWithPaging))]
        public IActionResult GetAllWithPaging([FromBody] ProductQueryFilter filter)
        {
            var products = _unitOfWork.Products.GetAllProductsWithPaging(filter);
            var metadata = new Metadata
            {
                TotalCount = products.TotalCount,
                PageSize = products.PageSize,
                CurrentPage = products.CurrentPage,
                TotalPages = products.TotalPages,
                HasNextPage = products.HasNextPage,
                HasPreviousPage = products.HasPreviousPage,
                NextPageUrl = _uriService.GetProductPaginationUri(filter, Url.RouteUrl(nameof(GetAllWithPaging))).ToString(),
                PreviousPageUrl = _uriService.GetProductPaginationUri(filter, Url.RouteUrl(nameof(GetAllWithPaging))).ToString()
            };

            var response = new ApiResponse<IEnumerable<Product>>(products)
            {
                Meta = metadata,
                Status = (int)HttpStatusCode.OK,
                Message = "Success",
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _unitOfWork.Products.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            var data = await _unitOfWork.Products.AddAsync(product);
            return Ok(data);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.Products.DeleteAsync(id);
            return Ok(data);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            var data = await _unitOfWork.Products.UpdateAsync(product);
            return Ok(data);
        }
    }
}
