using AuctionPortal.BusinessLayer.Facades;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.WebApi.Models.Auctions;
using RouteAttribute = System.Web.Http.RouteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace AuctionPortal.WebApi.Controllers
{
    public class AuctionsController : ApiController
    {
		public AuctionFacade AuctionFacade { get; set; }

		/// <summary>
        /// Query auctions according to given query parameters
        /// </summary>
        /// <param name="sort">Name of the auction attribute (e.g. "name", "price", ...) to sort according to</param>
        /// <param name="asc">Sort product collection in ascending manner</param>
        /// <param name="name">Product name (can be only partial: "Galaxy", "Lumia", ...)</param>
        /// <param name="minimalPrice">Minimal product price</param>
        /// <param name="maximalPrice">Maximal product price</param>
        /// <param name="category">Product category names, currently supported are: "android", "windows 10" and "ios"</param>
        /// <returns>Collection of products, satisfying given query parameters.</returns>       
        [HttpGet, Route("api/Auctions/Query")]
        public async Task<IEnumerable<AuctionDTO>> Query(string sort = null, bool asc = true, [FromUri] string[] category = null)
        {
	        var filter = new AuctionFilterDto
	        {
		        SortCriteria = sort,
		        SortAscending = asc,
		        CategoryNames = category,
	        };
	        var auctions = (await AuctionFacade.GetAllAuctionsAsync(filter));
	        foreach (var auction in auctions)
	        {
		        auction.Id = Guid.Empty;
	        }
	        return auctions;
        }

		/// <summary>
		/// Gets auction info for auction with given name
		/// </summary>
		/// <param name="name">Auction name</param>
		/// <returns>Complete auction info.</returns>
		public async Task<AuctionDTO> Get(string name)
        {
	        if (string.IsNullOrWhiteSpace(name))
	        {
		        throw new HttpResponseException(HttpStatusCode.BadRequest);
	        }
	        var auction = await AuctionFacade.GetAuctionAsync(name);
	        if (auction == null)
	        {
		        throw new HttpResponseException(HttpStatusCode.NotFound);
	        }
	        auction.Id = Guid.Empty;
	        return auction;
        }

		/// <summary>
		/// Creates auction, example URL call can be found in test folder.
		/// </summary>
		/// <param name="model">Created auction details.</param>
		/// <returns>Message describing the action result.</returns>
		public async Task<string> Post([FromBody]AuctionCreateModel model)
        {
	        if (!ModelState.IsValid)
	        {
		        throw new HttpResponseException(HttpStatusCode.BadRequest);
	        }
	        var auctionId = await AuctionFacade.CreateAuctionWithCategoryNameAsync(model.Auction, model.CategoryName);
	        if (auctionId.Equals(Guid.Empty))
	        {
		        throw new HttpResponseException(HttpStatusCode.BadRequest);
	        }
	        return $"Created auction with id: {auctionId}, within {model.CategoryName} category.";
        }

		/// <summary>
		/// Updates auction with given id.
		/// </summary>
		/// <param name="id">Id of the auction to update.</param>
		/// <param name="product">Auction to update</param>
		/// <returns>Message describing the action result.</returns>
		public async Task<string> Put(Guid id, [FromBody]AuctionDTO auction)
        {
	        if (!ModelState.IsValid)
	        {
		        throw new HttpResponseException(HttpStatusCode.BadRequest);
	        }
	        var success = await AuctionFacade.EditAuctionAsync(auction);
	        if (!success)
	        {
		        throw new HttpResponseException(HttpStatusCode.NotFound);
	        }
	        return $"Updated product with id: {id}";
        }

		/// <summary>
		/// Deletes auction with given id ("aa05dc64-5c07-40fe-a916-175165b9b90f", "aa06dc64-5c07-40fe-a916-175165b9b90f", ...)
		/// </summary>
		/// <param name="id">Id of the auction to delete.</param>
		/// <returns>Message describing the action result.</returns>
		public async Task<string> Delete(Guid id)
        {
	        var success = await AuctionFacade.DeleteAuctionAsync(id);
	        if (!success)
	        {
		        throw new HttpResponseException(HttpStatusCode.NotFound);
	        }
	        return $"Deleted product with id: {id}";
        }
	}
}