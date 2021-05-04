using Microsoft.AspNetCore.Mvc;
using Project_9.Models;
using Project_9.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_9.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService) {
            this.searchService = searchService;
        }

        /// <summary>
        /// Run search query and return result.
        /// </summary>
        /// <param name="query">
        /// Search query string from uri query part.<br />
        /// ex: uri?query="+or_filter -not_filter and_filter"
        /// </param>
        /// <returns>Status code 200 If query has result.<br/> Status code 404 If exaption occured or query has no result.</returns>
        [HttpGet]
        public IActionResult Search([FromQuery] string query) {
            try {
                var result = searchService.Search(query);
                if (result.Count() == 0)
                    throw new Exception();
                return Ok(result);
            }
            catch (Exception e) {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Add a document to index.
        /// </summary>
        /// <param name="document"> 
        /// Single document from http body. <br/>
        /// ex: {"id":"id string","text":"text string"}
        /// </param>
        /// <returns>Status code 200 If no exeption occure, otherwise Status code 500.</returns>
        [HttpPost]
        public IActionResult AddToIndex([FromBody] Document document) {
            try {
                searchService.AddToIndex(document);
                return Ok();
            }
            catch (Exception e) {
                return Problem(detail: "Somting went wrong!\n" + e.Message, statusCode: 500);
            }
        }

        /// <summary>
        /// Add list of documents to index.
        /// </summary>
        /// <param name="documents"> 
        /// List of documents from http body. <br/>
        /// ex: [{"id":"id string","text":"text string"}]
        /// </param>
        /// <returns>Status code 200 If no exeption occure, otherwise Status code 500.</returns>
        [HttpPost("bulk")]
        public IActionResult AddToIndex([FromBody] IEnumerable<Document> documents) {
            try {
                searchService.AddToIndex(documents);
                return Ok();
            }
            catch (Exception e) {
                return Problem(detail: "Somting went wrong!\n" + e.Message, statusCode: 500);
            }
        }
    }
}
